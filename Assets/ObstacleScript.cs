using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[0], true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[1], true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[2], true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[3], true);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseOver()
    {
        GameObject speechBubble = FindObjectOfType<GhostMove>().GetComponent<GhostMove>().speechBubble;
        speechBubble.SetActive(true);
        speechBubble.GetComponentInChildren<TextMesh>().text = "That's a " + gameObject.name + "!";
    }

    public void OnMouseExit()
    {
        GameObject speechBubble = FindObjectOfType<GhostMove>().GetComponent<GhostMove>().speechBubble;
        speechBubble.SetActive(false);
    }
    void OnParticleCollision(GameObject other) {
        GetComponent<Rigidbody2D>().AddForce(new Vector2(20, 0));
    }
}
