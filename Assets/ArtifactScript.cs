using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseOver() {
        GameObject speechBubble = FindObjectOfType<GhostMove>().GetComponent<GhostMove>().speechBubble;
        speechBubble.SetActive(true);
        speechBubble.GetComponentInChildren<TextMesh>().text = "That's a " + gameObject.name + "!\nLet's pick it up!";
    }

    public void OnMouseExit()
    {
        GameObject speechBubble = FindObjectOfType<GhostMove>().GetComponent<GhostMove>().speechBubble;
        speechBubble.SetActive(false);
    }

    public void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<GhostMove>() != null)
        {
            Destroy(gameObject);
        }
    }
}
