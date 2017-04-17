using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : MonoBehaviour {
    private Coroutine timeChange;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void QuitGame()
    {
        Application.Quit();
    }
    public void QuitToMain()
    {
        Application.LoadLevel("StartMenu");
    }
    public void StopTime()
    {
        if (timeChange != null)
        {
            StopCoroutine(timeChange);
        }
        timeChange = StartCoroutine(ChangeTime(0.0f));
    }
    public void ResumeTime()
    {
        if (timeChange != null)
        {
            StopCoroutine(timeChange);
        }
        timeChange = StartCoroutine(ChangeTime(1.0f));
    }
    IEnumerator ChangeTime(float newVal)
    {
        while(Time.timeScale != newVal)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, newVal, 3 * Time.fixedDeltaTime);
            yield return null;
        }
    }
}
