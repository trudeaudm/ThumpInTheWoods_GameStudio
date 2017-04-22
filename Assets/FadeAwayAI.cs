using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAwayAI : MonoBehaviour {
    private bool triggered;
    public float fadeRate;
    private SpriteRenderer myRend;
    private Color newColor;
	// Use this for initialization
	void Start () {
        myRend = GetComponent<SpriteRenderer>();
        newColor = myRend.color;
        newColor.a = 0;
	}
	
	public void TriggerFade()
    {
        if (!triggered)
        {
            InvokeRepeating("Fade", 0, 0.05f);
            triggered = true;
        }
    }
    void Fade()
    {
        myRend.color = Color.Lerp(myRend.color, newColor, fadeRate * Time.fixedDeltaTime);
        if (myRend.color.a <= newColor.a + 0.1f)
        {
            CancelInvoke("Fade");
            Vanish();
        }
    }
    void Vanish()
    {
        gameObject.SetActive(false);
    }
}
