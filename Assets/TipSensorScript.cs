using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipSensorScript : MonoBehaviour {

    public float timer;
    private GameObject bubble;
    [SerializeField]
    public string tip;
    public float duration;
    private Coroutine messageTimerRoutine;
    public GameObject indicatorOb;
    public float indicatorBlinkRate;
    private bool messageOn;
    // Use this for initialization
    void Awake() {
        bubble = GameObject.FindGameObjectWithTag("SpeechBubbleText");
        if (indicatorOb)
        {
            indicatorOb.SetActive(false);
        }
    }
    void SetMessage()
    {
        messageOn = true;
        if (duration > 0)
        {
            StartCoroutine(EndMessageTimer());
        }
        if (indicatorOb)
        {
            StartCoroutine(BlinkIndicator());
        }
    
        bubble.transform.parent.gameObject.SetActive(true);
        bubble.GetComponent<Text>().text = tip;
    }
    void EndMessage(){
        messageOn = false;
        if (bubble != null && bubble.GetComponent<Text>().text == tip)
        {
            bubble.transform.parent.gameObject.SetActive(false);
        }

    }
    IEnumerator EndMessageTimer()
    {
        yield return new WaitForSecondsRealtime(duration);
        EndMessage();
    }
    IEnumerator MessageTimer()
    {
        yield return new WaitForSecondsRealtime(timer);
        SetMessage();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            messageTimerRoutine = StartCoroutine(MessageTimer());
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            StopCoroutine(messageTimerRoutine);
            EndMessage();
        }
    }
    IEnumerator BlinkIndicator()
    {
        while (messageOn)
        {
            indicatorOb.SetActive(true);
            yield return new WaitForSecondsRealtime(indicatorBlinkRate);
            indicatorOb.SetActive(false);
            yield return new WaitForSecondsRealtime(indicatorBlinkRate);
        }
    }

}
