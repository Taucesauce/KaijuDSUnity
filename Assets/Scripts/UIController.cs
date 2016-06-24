using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIController : MonoBehaviour {
    private Sprite currentEmote;
     
    //Methods to toggle emote menu display.
    public void hideGameObject(GameObject objectToHide) {
        objectToHide.SetActive(false);
    }
    public void showGameObject(GameObject objectToShow) {
        objectToShow.SetActive(true);
    }

    public void setGobzillaSprite(Sprite sprite) {
        GameObject gobzilla = GameObject.Find("Gobzilla");
        SpriteRenderer rend = gobzilla.GetComponent<SpriteRenderer>();
        rend.sprite = sprite;
    }

    public void setBiltonSprite(Sprite sprite) {
        GameObject bilton = GameObject.Find("Bilton");
        SpriteRenderer rend = bilton.GetComponent<SpriteRenderer>();
        rend.sprite = sprite;
    }
    public void setButtonSprite(Sprite sprite) {
        currentEmote = sprite;
    }
    public void setP1Sprite() {
        GameObject p1Button = GameObject.Find("P1EmoteMenuButton");
        Button button = p1Button.GetComponent<Button>();
        button.image.sprite = currentEmote;
    }

    public void setP2Sprite() {
        GameObject p2Button = GameObject.Find("P2EmoteMenuButton");
        Button button = p2Button.GetComponent<Button>();
        button.image.sprite = currentEmote;
    }
}
