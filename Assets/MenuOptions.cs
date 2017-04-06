using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOptions : MonoBehaviour {

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
}
