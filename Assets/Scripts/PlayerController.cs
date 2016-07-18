using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public Gobzilla gobzilla;
    public Bilton bilton;

    // Use this for initialization
    void Start () {
        gobzilla.Type = PlayerType.unassigned;
        bilton.Type = PlayerType.unassigned;
    }

    public void resetPlayers() {
        gobzilla.Type = PlayerType.unassigned;
        bilton.Type = PlayerType.unassigned;
    }

    //Matches a player's type to its corresponding reaction value.
    public Emote emoteResponse(Emote emote) {
        int responseValue = 0;
        Emote responseEmote = Emote.neutral;
        bool isP1Turn = GameController.isP1Turn;

        PlayerType playerType;

        if(isP1Turn) {
            playerType = bilton.Type;
            gobzilla.CurrentEmote = emote;
            gobzilla.SetSprite(emote);
        } else {
            playerType = gobzilla.Type;
            bilton.CurrentEmote = emote;
            bilton.SetSprite(emote);
        }

        //Find corresponding value of this given reaction based on type.
        switch(playerType) {
            case PlayerType.childish:
                responseEmote = DataStructures.childishResponses[(int)emote];
                responseValue = calculateValue(responseEmote);
                updateEmote(responseEmote, isP1Turn);
                break;
            case PlayerType.loving:
                responseEmote = DataStructures.lovingResponses[(int)emote];
                responseValue = calculateValue(responseEmote);
                updateEmote(responseEmote, isP1Turn);
                break;
            case PlayerType.tsundere:
                responseEmote = DataStructures.tsundereResponses[(int)emote];
                responseValue = calculateValue(responseEmote);
                updateEmote(responseEmote, isP1Turn);
                break;
            default:
                break;
        }

        //Update the current score.
        GameController.updateScore(responseValue);
        return responseEmote;
    }

    private void updateEmote(Emote response, bool isP1Turn) {
        if (isP1Turn) {
            bilton.SetSprite(response);
        }
        else {
            gobzilla.SetSprite(response);
        }
    }

    public Emote GobzillaEmote() {
        return gobzilla.CurrentEmote;
    }

    public Emote BiltonEmote() {
        return bilton.CurrentEmote;
    }

    public PlayerType GobzillaType() {
        return gobzilla.Type;
    }

    public PlayerType BiltonType() {
        return bilton.Type;
    }

    //Calculates the point value of a given emote.
    private int calculateValue(Emote emote) {
        switch(emote) {
            case Emote.happy:
                return 1;
            case Emote.sad:
                return -1;
            case Emote.wow:
                return 0;
            case Emote.angry:
                return -2;
            case Emote.heart:
                return 2;
            default:
                return 0;
        }
    }
}
