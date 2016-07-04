using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public enum PlayerType { childish, loving, tsundere };
    public enum Emote { neutral, happy, sad, wow, angry, heart };

    private static GameObject gobzilla;
    private static GameObject bilton;
    private static PlayerType gobzillaType = PlayerType.childish;
    private static PlayerType biltonType = PlayerType.tsundere;
    public static Emote gobzillaEmote;
    public static Emote biltonEmote;
    private static Sprite[] gobzillaSprites = new Sprite[6];
    private static Sprite[] biltonSprites = new Sprite[6];

    private static int[] childishResponses = new int[] { 0, 1, 1, -1, 0, -1 };
    private static int[] lovingResponses = new int[] { 0, -1, 1, 0, 1, -1 };
    private static int[] tsundereResponses = new int[] { 0, 0, 1, -1, 1, -1 };

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
    public static void setGobzillaSprite(int emote) {
        SpriteRenderer rend = gobzilla.GetComponent<SpriteRenderer>();
        rend.sprite = gobzillaSprites[emote];
    }

    public static void setBiltonSprite(int emote) {
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
    public static void emoteResponse(int emote) {
        int response = 0;
        bool isP1Turn = GameController.isP1Turn;

        PlayerType playerType;

        if(isP1Turn) {
            playerType = biltonType;
        } else {
            playerType = gobzillaType;
        }

        //Find corresponding value of this given reaction based on type.
        switch(playerType) {
            case PlayerType.childish:
                response = childishResponses[(int)emote];
                updateSprites(response, isP1Turn);
                break;
            case PlayerType.loving:
                response = lovingResponses[(int)emote];
                updateSprites(response, isP1Turn);
                break;
            case PlayerType.tsundere:
                response = tsundereResponses[(int)emote];
                updateSprites(response, isP1Turn);
                break;
            default:
                break;
        }

        //Update the current score.
        GameController.updateScore(response);
    }

    private static void updateSprites(int response, bool isP1Turn) {
        if (isP1Turn) {
            setBiltonSprite(response);
            biltonEmote = (Emote)response;
        }
        else {
            setGobzillaSprite(response);
            gobzillaEmote = (Emote)response;
        }
    }
}
