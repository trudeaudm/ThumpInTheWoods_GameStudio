using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactUIScript : MonoBehaviour {

    public bool isGotten;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (isGotten) {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
