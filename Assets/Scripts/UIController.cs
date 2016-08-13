using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;


public class UIController : MonoBehaviour {
    //UIController depends on PlayerController.
    public PlayerController PlayerController;

    public GameObject gameWrapper;
    public GameObject endWrapper;
    private static bool gameDisplayed = false;

    public Image resultImage;
    //Button variables
    private Emote currentEmote;
    public GameObject p1Button;
    public GameObject p2Button;
    public GameObject p1Menu;
    public GameObject p2Menu;

    private Button[] p1MenuButtons;
    private Button[] p2MenuButtons;
    private int selectedButton;
    //StatusBar variables
    private SpriteRenderer iconRend;
    private SpriteRenderer bannerRend;
    private SpriteRenderer meterRend;

    private GameObject leftCursor;
    private GameObject rightCursor;

    public Sprite[] iconSprites;
    public Sprite[] selectSprites;
    public Sprite[] bannerSprites;
    public Sprite[] meterSprites;

    //Game Text UI Variables
    private Text turnText;
    private Text turnsRemainingText;

    //Sprites for menu wheel
    public Sprite[] menuSprites = new Sprite[6];

    void Start() {
        gameDisplayed = false;
        p1MenuButtons = p1Menu.GetComponentsInChildren<Button>();
        p2MenuButtons = p2Menu.GetComponentsInChildren<Button>();
        selectedButton = -1;
    }

    void Update() {
        if(PlayerController.GobzillaType() != PlayerType.unassigned && 
            PlayerController.BiltonType() != PlayerType.unassigned && !gameDisplayed) {
            displayGame();
            gameDisplayed = true;
        } else if(GameController.turnsRemaining == 0) {
            displayEnd();
        }
    }

    void displayEnd() {
        gameWrapper.SetActive(false);
        endWrapper.SetActive(true);
        resultImage.sprite = iconSprites[(int)GameController.currentRelationshipScore];
    }

    void displayGame() {
        gameWrapper.SetActive(true);
        setVariables();
    }

    public static void resetUI() {
        gameDisplayed = false;
    }

    void setVariables() {
        //Grab the objects from the game and assign them to class vars.
        GameObject currentTurn = GameObject.Find("CurrentTurn");
        turnText = currentTurn.GetComponent<Text>();
        GameObject turnsRemaining = GameObject.Find("TurnsRemaining");
        turnsRemainingText = turnsRemaining.GetComponent<Text>();
        GameObject iconObject = GameObject.Find("IconObject");
        iconRend = iconObject.GetComponent<SpriteRenderer>();
        GameObject bannerObject = GameObject.Find("BannerObject");
        bannerRend = bannerObject.GetComponent<SpriteRenderer>();
        GameObject meterObject = GameObject.Find("Meter");
        meterRend = meterObject.GetComponent<SpriteRenderer>();
        leftCursor = GameObject.Find("LeftCursor");
        rightCursor = GameObject.Find("RightCursor");
    }

    //Methods to toggle emote menu display.
    public void hideGameObject(GameObject objectToHide) {
        objectToHide.SetActive(false);
    }
    public void showGameObject(GameObject objectToShow) {
        objectToShow.SetActive(true);
    }

    //Specific methods for turn-related restrictions.
    public void showP1Menu(GameObject menuToShow) {
        if(GameController.isP1Turn) {
            menuToShow.SetActive(true);
            p1Button.SetActive(false);
        }    
    }

    public void showP2Menu(GameObject menuToShow) {
        if(!GameController.isP1Turn) {
            menuToShow.SetActive(true);
            p2Button.SetActive(false);
        }
    }

    //Methods to change the current active sprite
    public void setCurrentEmote(int emote) {
        currentEmote = (Emote)emote;
    }

    public void setSelectedButton(int emote) {
        if (GameController.isP1Turn) {
            if(selectedButton != -1) {
                p1MenuButtons[selectedButton - 1].image.sprite = menuSprites[selectedButton];
            }
            selectedButton = emote;
            p1MenuButtons[emote - 1].image.sprite = selectSprites[emote];
        } else {
            if (selectedButton != -1) {
                p2MenuButtons[selectedButton - 1].image.sprite = menuSprites[selectedButton];
            }
            selectedButton = emote;
            p2MenuButtons[emote - 1].image.sprite = selectSprites[emote];
        }
    }

    public void resetMenu() {
        if(GameController.isP1Turn) {
            p1MenuButtons[selectedButton - 1].image.sprite = menuSprites[selectedButton];
            selectedButton = -1;
        } else {
            p2MenuButtons[selectedButton - 1].image.sprite = menuSprites[selectedButton];
            selectedButton = -1;
        }
    }
    public void setP1Button(Emote emote) {
        Button button = p1Button.GetComponent<Button>();
        button.image.sprite = menuSprites[(int)(emote)];
    }

    public void setP2Button(Emote emote) {
        Button button = p2Button.GetComponent<Button>();
        button.image.sprite = menuSprites[(int)emote];
    }
    //TODO: Separate concern.
    public void setP1Sprite() {
        //Make sure it's the correct player's turn.
        if(GameController.isP1Turn && currentEmote != Emote.neutral) {
            //Update UI image
            setP1Button(currentEmote);
            //Update player-based variables.
            setP2Button(PlayerController.emoteResponse(currentEmote));
            currentEmote = Emote.neutral;
            //Update game logic dependent variables.
            //Can potentially move this from inside of UIController to in Editor OnClick().
            GameController.nextPlayerTurn();
            turnText.text = "Bilton's Turn";
            turnsRemainingText.text = "Turns Remaining: " + GameController.turnsRemaining;
            updateStatusBar(); 
        }
    }

    //TODO: Separate concern
    public void setP2Sprite() {
        //Make sure it's the correct player's turn
        if (!GameController.isP1Turn && currentEmote != Emote.neutral) {
            //Update UI image
            setP2Button(currentEmote);
            //Update player-based variables.
            setP1Button(PlayerController.emoteResponse(currentEmote));
            currentEmote = Emote.neutral;
            //Update game logic dependent variables.
            GameController.nextPlayerTurn();
            turnText.text = "Gobzilla's Turn";
            turnsRemainingText.text = "Turns Remaining: " + GameController.turnsRemaining;
            updateStatusBar();
        }
    }

    private void updateStatusBar() {
        int relationship = (int)GameController.currentRelationshipScore;
        iconRend.sprite = iconSprites[relationship];
        bannerRend.sprite = bannerSprites[relationship];
        meterRend.sprite = meterSprites[relationship];
        leftCursor.transform.position = new Vector2(meterRend.bounds.min.x, leftCursor.transform.position.y);
        rightCursor.transform.position = new Vector2(meterRend.bounds.max.x, leftCursor.transform.position.y);
    }
}
