using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public enum PlayerType { childish, loving, tsundere };
    public enum Emote { neutral, happy, sad, wow, angry, heart };

    private GameObject gobzilla;
    private GameObject bilton;
    private static PlayerType gobzillaType = PlayerType.childish;
    private static PlayerType biltonType = PlayerType.tsundere;

    // Use this for initialization
    void Start () {
        gobzilla = GameObject.Find("Gobzilla");
        bilton = GameObject.Find("Bilton");
    }

    //Methods for setting player sprite reactions.
    public void setGobzillaSprite(Sprite sprite) {
        SpriteRenderer rend = gobzilla.GetComponent<SpriteRenderer>();
        rend.sprite = sprite;
    }

    public void setBiltonSprite(Sprite sprite) {
        SpriteRenderer rend = bilton.GetComponent<SpriteRenderer>();
        rend.sprite = sprite;
    }

    public void setGobzillaType(PlayerType type) {
        gobzillaType = type;
    }

    public void setBiltonType(PlayerType type) {
        biltonType = type;
    }

    //Matches a player's type to its corresponding reaction value.
    public static void emoteResponse(Emote emote) {
        int response = 1;
        PlayerType playerType;

        if(GameController.isP1Turn) {
            playerType = biltonType;
        } else {
            playerType = gobzillaType;
        }

        //TODO: Incorporate player type key/value arrays to assign response value.
        //Find corresponding value of this given reaction based on type.
        switch(playerType) {
            case PlayerType.childish:
                response = 1;
                break;
            case PlayerType.loving:
                response = 1;
                break;
            case PlayerType.tsundere:
                response = 1;
                break;
            default:
                break;
        }

        //Update the current score.
        GameController.updateScore(response);
    }
}
