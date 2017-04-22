using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditScroll : MonoBehaviour {
    public float fadeRate;
    public float scrollRate;
    public float delay;
    private RectTransform myRect;
    private Vector3 startPos;
    private Text myText;
    private Color startColor;
    public float bounds;
    public bool theEnd;
	// Use this for initialization
	void Start () {
        myText = GetComponent<Text>();
        myText.color = new Color(1, 1, 1, 0);
        startColor = myText.color;
        myRect = GetComponent<RectTransform>();
        startPos = myRect.position;
        InvokeRepeating("Scroll", delay, 0.02f);
	}
    void Scroll()
    {
        Debug.Log("yolo");
        startPos.y = startPos.y + scrollRate * Time.fixedDeltaTime;
        myRect.position = startPos;
        if (myRect.anchoredPosition.y < 0)
        {
            startColor.a = startColor.a + fadeRate * Time.fixedDeltaTime;
        }
        else
        {
            startColor.a = startColor.a - fadeRate * Time.fixedDeltaTime;
        }
        myText.color = startColor;
        if (myRect.anchoredPosition.y > bounds)
        {
            if (theEnd)
            {
                FindObjectOfType<MenuOptions>().QuitToMain();
            }
            Destroy(this);
        }
    }
	
}
