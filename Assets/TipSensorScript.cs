using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipSensorScript : MonoBehaviour {

    public float timer;
    public bool inTrigger;
    public GameObject bubble;
    [SerializeField]
    public string tip;
    // Use this for initialization
    void Awake() {
        bubble = GameObject.FindGameObjectWithTag("SpeechBubbleText");
    }
	void Start () {
        timer = 0;
        inTrigger = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (inTrigger)
        {
            timer += Time.deltaTime;
            if (timer > 5)
            {
                bubble.transform.parent.gameObject.SetActive(true);
                bubble.GetComponent<Text>().text = tip;
            }
        }
        else
        {
            timer = 0;
            if (bubble != null)
            {
                bubble.transform.parent.gameObject.SetActive(false);
            }
        }
    }

    
}
