using UnityEngine;
using System.Collections;

public class Bilton : MonoBehaviour {

    public Sprite[] biltonSprites;
    private SpriteRenderer rend;

    private Emote currentEmote = Emote.neutral;
    public Emote CurrentEmote { get { return currentEmote; } set { currentEmote = value; } }
    private PlayerType type = PlayerType.unassigned;
    public PlayerType Type { get { return type; } set { type = value; } }

    // Use this for initialization
    void Start() {
        currentEmote = Emote.neutral;
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update() {

    }

    public void SetSprite(Emote emote) {
        GetComponent<SpriteRenderer>().sprite = biltonSprites[(int)emote];
        currentEmote = emote;
    }

    public void EmotePreview(int emote) {
        rend.sprite = biltonSprites[emote];
    }

    public void SetPlayerType(int newType) {
        type = (PlayerType)newType;
    }
}