using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public static bool isP1Turn;
    public static int currentRelationshipScore;
    public static int turnsRemaining = 6;

	// Use this for initialization
	void Start () {
        isP1Turn = true;
	}
	
	// Update is called once per frame
	void Update () {
	
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
    }
}
