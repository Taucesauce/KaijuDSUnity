using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public enum PlayerType { childish, loving, tsundere };
    public enum Emote { neutral, happy, sad, wow, angry, heart };

    private GameObject gobzilla;
    private GameObject bilton;
    private static PlayerType gobzillaType = PlayerType.childish;
    private static PlayerType biltonType = PlayerType.tsundere;
    private Sprite[] gobzillaSprites = new Sprite[6];
    private Sprite[] biltonSprites = new Sprite[6];

    // Use this for initialization
    void Start () {
        gobzilla = GameObject.Find("Gobzilla");
        bilton = GameObject.Find("Bilton");

        int arrayIndex = 0;
        //Load corresponding sprites for Gobzilla and sort into array.
        foreach (string name in Emote.GetNames(typeof(Emote))) {
                string iconName = "GobzillaEmotes/Gobzilla" + name;
                Sprite iconSprite = Resources.Load<Sprite>(iconName);
                gobzillaSprites[arrayIndex] = iconSprite;
                arrayIndex++;
        }
        arrayIndex = 0;
        //Load corresponding sprites for Gobzilla and sort into array.
        foreach (string name in Emote.GetNames(typeof(Emote))) {
            string iconName = "BiltonEmotes/Bilton" + name;
            Sprite iconSprite = Resources.Load<Sprite>(iconName);
            biltonSprites[arrayIndex] = iconSprite;
            arrayIndex++;
        }
    }

    //Methods for setting player sprite reactions.
    public void setGobzillaSprite(int emote) {
        SpriteRenderer rend = gobzilla.GetComponent<SpriteRenderer>();
        rend.sprite = gobzillaSprites[emote];
    }

    public void setBiltonSprite(int emote) {
        SpriteRenderer rend = bilton.GetComponent<SpriteRenderer>();
        rend.sprite = biltonSprites[emote];
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
