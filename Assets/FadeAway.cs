using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAway : MonoBehaviour {
    private Color normalColor;
    private SpriteRenderer myRend;
    private Coroutine fadeRoutine;
    void Start()
    {
        myRend = GetComponent<SpriteRenderer>();
        normalColor = myRend.color;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }
            fadeRoutine = StartCoroutine(FadeToColor(new Color(1, 1, 1, 0)));
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }
            fadeRoutine = StartCoroutine(FadeToColor(normalColor));
        }
    }
    IEnumerator FadeToColor(Color newColor)
    {
        while (myRend.color != newColor)
        {
            myRend.color = Color.Lerp(myRend.color, newColor, 1 * Time.fixedDeltaTime);
            yield return null;
        }
    }
}
