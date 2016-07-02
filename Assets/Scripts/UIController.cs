using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIController : MonoBehaviour {
    //Class variables:

    //Button variables
    private Emote currentEmote;
    private GameObject p1Button;
    private GameObject p2Button;

    //StatusBar variables
    private SpriteRenderer iconRend;
    private SpriteRenderer bannerRend;
    private SpriteRenderer meterRend;

    private Sprite enemyIcon;
    private Sprite enemyBanner;
    private Sprite enemyMeter;
    private Sprite friendIcon;
    private Sprite friendBanner;
    private Sprite friendMeter;
    private Sprite soulmateIcon;
    private Sprite soulmateBanner;
    private Sprite soulmateMeter;

    private GameObject leftCursor;
    private GameObject rightCursor;

    //Game Text UI Variables
    private Text turnText;
    private Text turnsRemainingText;

    //Sprites for menu wheel
    private Sprite[] menuSprites = new Sprite[6];

    void Start() {
        //Grab the objects from the game and assign them to class vars.
        p1Button = GameObject.Find("P1EmoteMenuButton");
        p2Button = GameObject.Find("P2EmoteMenuButton");
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

        int arrayIndex = 1;
        //Load corresponding sprites for menu buttons and sort into array.
        foreach (string name in Emote.GetNames(typeof(Emote))) {
            if(name != "neutral") {
                string iconName = "Emotes/" + name + "_emote";
                Sprite iconSprite = Resources.Load<Sprite>(iconName);
                menuSprites[arrayIndex] = iconSprite;
                arrayIndex++;
            }
        }

        //Load in StatusBar UI sprites
        enemyIcon = Resources.Load<Sprite>("StatusBar/Icons/enemyIcon");
        enemyBanner = Resources.Load<Sprite>("StatusBar/Banner/enemyBanner");
        enemyMeter = Resources.Load<Sprite>("StatusBar/Meter/enemyMeter");
        friendIcon = Resources.Load<Sprite>("StatusBar/Icons/friendIcon");
        friendMeter = Resources.Load<Sprite>("StatusBar/Meter/friendMeter");
        friendBanner = Resources.Load<Sprite>("StatusBar/Banner/friendBanner");
        soulmateIcon = Resources.Load<Sprite>("StatusBar/Icons/soulmateIcon");
        soulmateBanner = Resources.Load<Sprite>("StatusBar/Banner/soulmateBanner");
        soulmateMeter = Resources.Load<Sprite>("StatusBar/Meter/soulmateMeter");
    }

    void Update() {
        leftCursor.transform.position = new Vector2(meterRend.bounds.min.x, leftCursor.transform.position.y);
        rightCursor.transform.position = new Vector2(meterRend.bounds.max.x, leftCursor.transform.position.y);
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

    //TODO: Separate concern.
    public void setP1Sprite() {
        //Make sure it's the correct player's turn.
        if(GameController.isP1Turn && currentEmote != Emote.neutral) {
            //Update UI image
            Button button = p1Button.GetComponent<Button>();
            button.image.sprite = menuSprites[(int)currentEmote];
            //Update player-based variables.
            PlayerController.emoteResponse(PlayerController.Emote.happy);
            currentEmote = Emote.neutral;
            //Update game logic dependent variables.
            //Can potentially move this from inside of UIController to in Editor OnClick().
            GameController.nextPlayerTurn();
            turnText.text = "Player 2's Turn";
            turnsRemainingText.text = "Turns Remaining: " + GameController.turnsRemaining;
            updateStatusBar(); 
        }
    }

    //TODO: Separate concern
    public void setP2Sprite() {
        //Make sure it's the correct player's turn
        if (!GameController.isP1Turn && currentEmote != Emote.neutral) {
            //Update UI image
            Button button = p2Button.GetComponent<Button>();
            button.image.sprite = menuSprites[(int)currentEmote];
            //Update player-based variables.
            PlayerController.emoteResponse(PlayerController.Emote.happy);
            currentEmote = Emote.neutral;
            //Update game logic dependent variables.
            //Can potentially move this from inside of UIController to in Editor OnClick().
            GameController.nextPlayerTurn();
            turnText.text = "Player 1's Turn";
            turnsRemainingText.text = "Turns Remaining: " + GameController.turnsRemaining;
            updateStatusBar();
        }
    }

    private void updateStatusBar() {
        if(GameController.currentRelationshipScore < -1) {
            iconRend.sprite = enemyIcon;
            bannerRend.sprite = enemyBanner;
            meterRend.sprite = enemyMeter;
        } else if (GameController.currentRelationshipScore > 1) {
            iconRend.sprite = soulmateIcon;
            bannerRend.sprite = soulmateBanner;
            meterRend.sprite = soulmateMeter;
        } else {
            iconRend.sprite = friendIcon;
            bannerRend.sprite = friendBanner;
            meterRend.sprite = friendMeter;
        }
    }
}
