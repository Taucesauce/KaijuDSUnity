using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    //GameController depends on these other controllers:
    public PlayerController PlayerController;
    public UIController UIController;

    public static bool isP1Turn;
    public static Relationship currentRelationshipScore = Relationship.acquaintance;
    public static int turnsRemaining = 6;

	// Use this for initialization
	void Start () {
        isP1Turn = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void resetGame() {
        isP1Turn = true;
        currentRelationshipScore = 0;
        turnsRemaining = 6;
        PlayerController.resetPlayers();
        UIController.resetUI();

    }
    public static void nextPlayerTurn() {
        if(isP1Turn) {
            isP1Turn = false;
        } else {
            isP1Turn = true;
        }
        turnsRemaining--;
    }

    public static void updateScore(int incrementAmount) {
        currentRelationshipScore = currentRelationshipScore + incrementAmount;
        if((int)currentRelationshipScore <- 0) {
            currentRelationshipScore = Relationship.mortal;
        } else if((int)currentRelationshipScore >= 5) {
            currentRelationshipScore = Relationship.soulmate;
        }
    }

    public void switchSetting() {
        SceneManager.LoadScene("Game");
    }

    public void quitGame() {
        Application.Quit();
    }
}
