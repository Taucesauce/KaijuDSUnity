using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class UIController : MonoBehaviour {
    //Class variables
    private Sprite currentEmote;
    private GameObject p1Button;
    private GameObject p2Button;
    private Text turnText;
    private Text turnsRemainingText;
    private Text scoreText;
    private GameObject statusIcon;
    private GameObject statusBanner;
    private GameObject friendMeter;
    private GameObject enemyMeter;
    private GameObject soulMeter;
    private GameObject leftCursor;
    private GameObject rightCursor;

    void Start() {
        //Grab the objects from the game and assign them to class vars.
        p1Button = GameObject.Find("P1EmoteMenuButton");
        p2Button = GameObject.Find("P2EmoteMenuButton");
        GameObject currentTurn = GameObject.Find("CurrentTurn");
        turnText = currentTurn.GetComponent<Text>();
        GameObject turnsRemaining = GameObject.Find("TurnsRemaining");
        turnsRemainingText = turnsRemaining.GetComponent<Text>();
        statusIcon = GameObject.Find("Icon");
        statusBanner = GameObject.Find("Banner");
        friendMeter = GameObject.Find("FriendMeter");
        enemyMeter = GameObject.Find("EnemyMeter");
        soulMeter = GameObject.Find("SoulmateMeter");
        leftCursor = GameObject.Find("LeftCursor");
        rightCursor = GameObject.Find("RightCursor");
    }

    void Update() {

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
    public void setButtonSprite(Sprite sprite) {
        currentEmote = sprite;
    }

    //TODO: Separate concern.
    public void setP1Sprite() {
        //Make sure it's the correct player's turn.
        if(GameController.isP1Turn && currentEmote != null) {
            //Update UI image
            Button button = p1Button.GetComponent<Button>();
            button.image.sprite = currentEmote;
            //Update player-based variables.
            PlayerController.emoteResponse(PlayerController.Emote.happy);
            currentEmote = null;
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
        if (!GameController.isP1Turn && currentEmote != null) {
            //Update UI image
            Button button = p2Button.GetComponent<Button>();
            button.image.sprite = currentEmote;
            //Update player-based variables.
            PlayerController.emoteResponse(PlayerController.Emote.happy);
            currentEmote = null;
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
            enemyMeter.SetActive(true);
            friendMeter.SetActive(false);
            soulMeter.SetActive(false);
        } else if (GameController.currentRelationshipScore > 1) {
            enemyMeter.SetActive(false);
            friendMeter.SetActive(false);
            soulMeter.SetActive(true);
        } else {
            enemyMeter.SetActive(false);
            friendMeter.SetActive(true);
            soulMeter.SetActive(false);
        }
    }
}
