using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArtifactScript : MonoBehaviour {
    [SerializeField] private int itemType; // 1 is hat, 2 is shirt
    public bool loadCredits;
    private Image fadeOut;
    private Color FadeToColor = new Color(0, 0, 0, 0);
    private SpriteRenderer mySprite;
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        if (mySprite == null)
        {
           mySprite = GetComponentInChildren<SpriteRenderer>();
        }
    }
    public void OnCollisionEnter2D(Collision2D col)
    {
        GhostMove GM = col.gameObject.GetComponent<GhostMove>();
        if (GM != null)
        {
            GetComponent<Rigidbody2D>().simulated = false;
            GetComponent<Collider2D>().enabled = false;
            transform.position = GM.GetObjectPos(itemType).position;
            transform.rotation = GM.GetObjectPos(itemType).rotation;
            mySprite.sortingOrder = 6;
            transform.parent = GM.GetObjectPos(itemType);

            Color transColor = mySprite.color;
            transColor.a = 0.5f;
            mySprite.color = transColor;
            if (!loadCredits)
            {
                Destroy(this);
            }
            else
            {
                fadeOut = GameObject.FindGameObjectWithTag("FadeOut").GetComponent<Image>();
                InvokeRepeating("FadeScreen", 0, 0.02f);
                Invoke("LoadCredits", 3.0f);
            }
        }
    }
    void LoadCredits()
    {
        Application.LoadLevel("Credits");
    }
    void FadeScreen()
    {
        FadeToColor.a = FadeToColor.a + 0.25f * Time.fixedDeltaTime;
        fadeOut.color = FadeToColor;
    }
}
