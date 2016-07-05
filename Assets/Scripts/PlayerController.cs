using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public enum PlayerType { childish, loving, tsundere, unassigned };
    public enum Emote { neutral, happy, sad, wow, angry, heart };

    private static GameObject gobzilla;
    private static GameObject bilton;
    private static SpriteRenderer gobzillaRend;
    private static SpriteRenderer biltonRend;
    public static PlayerType gobzillaType = PlayerType.unassigned;
    public static PlayerType biltonType = PlayerType.unassigned;
    public static Emote gobzillaEmote;
    public static Emote biltonEmote;
    private static Sprite[] gobzillaSprites = new Sprite[6];
    private static Sprite[] biltonSprites = new Sprite[6];

    private static int[] childishResponses = new int[] { 0, 1, 5, 2, 3, 4 };
    private static int[] lovingResponses = new int[] { 0, 4, 1, 3, 5, 2 };
    private static int[] tsundereResponses = new int[] { 0, 3, 1, 4, 1, 2 };

    // Use this for initialization
    void Start () {
        gobzilla = GameObject.Find("Gobzilla");
        bilton = GameObject.Find("Bilton");

        gobzillaRend = gobzilla.GetComponent<SpriteRenderer>();
        biltonRend = bilton.GetComponent<SpriteRenderer>();

        gobzillaType = PlayerType.unassigned;
        biltonType = PlayerType.unassigned;

        int arrayIndex = 0;
        //Load corresponding sprites for Gobzilla and sort into array.
        //Not great, forces naming conventions on assets to work properly.
        foreach (string name in Emote.GetNames(typeof(Emote))) {
                string iconName = "GobzillaEmotes/Gobzilla" + name;
                Sprite iconSprite = Resources.Load<Sprite>(iconName);
                gobzillaSprites[arrayIndex] = iconSprite;
                arrayIndex++;
        }

        arrayIndex = 0;
        //Load corresponding sprites for Gobzilla and sort into array.
        //Not great, same as above.
        foreach (string name in Emote.GetNames(typeof(Emote))) {
            string iconName = "BiltonEmotes/Bilton" + name;
            Sprite iconSprite = Resources.Load<Sprite>(iconName);
            biltonSprites[arrayIndex] = iconSprite;
            arrayIndex++;
        }
    }

    public static void resetPlayers() {
        gobzillaType = PlayerType.unassigned;
        biltonType = PlayerType.unassigned;
    }

    //Methods for setting player sprite reactions.
    public static void setGobzillaSprite(int emote) {
        gobzillaRend.sprite = gobzillaSprites[emote];
    }

    public static void setBiltonSprite(int emote) {
        biltonRend.sprite = biltonSprites[emote];
    }

    public void setGobzillaType(int type) {
        gobzillaType = (PlayerType)type;
    }

    public void setBiltonType(int type) {
        biltonType = (PlayerType)type;
    }

    //Matches a player's type to its corresponding reaction value.
    public static int emoteResponse(int emote) {
        int responseValue = 0;
        int responseEmote = 0;
        bool isP1Turn = GameController.isP1Turn;

        PlayerType playerType;

        if(isP1Turn) {
            playerType = biltonType;
            gobzillaEmote = (Emote)emote;
            setGobzillaSprite(emote);
        } else {
            playerType = gobzillaType;
            biltonEmote = (Emote)emote;
            setBiltonSprite(emote);
        }

        //Find corresponding value of this given reaction based on type.
        switch(playerType) {
            case PlayerType.childish:
                responseValue = calculateValue(emote);
                responseEmote = childishResponses[(int)emote];
                updateEmote(responseEmote, isP1Turn);
                break;
            case PlayerType.loving:
                responseValue = calculateValue(emote);
                responseEmote = lovingResponses[emote];
                updateEmote(responseEmote, isP1Turn);
                break;
            case PlayerType.tsundere:
                responseValue = calculateValue(emote);
                responseEmote = tsundereResponses[emote];
                updateEmote(responseEmote, isP1Turn);
                break;
            default:
                break;
        }

        //Update the current score.
        GameController.updateScore(responseValue);
        return responseEmote;
    }

    private static void updateEmote(int response, bool isP1Turn) {
        if (isP1Turn) {
            biltonEmote = (Emote)response;
            setBiltonSprite(response);
        }
        else {
            gobzillaEmote = (Emote)response;
            setGobzillaSprite(response);
        }
    }

    private static int calculateValue(int emote) {
        switch(emote) {
            case 1:
                return 1;
            case 2:
                return -1;
            case 3:
                return 1;
            case 4:
                return -2;
            case 5:
                return 2;
            default:
                return 0;
        }
    }
}
