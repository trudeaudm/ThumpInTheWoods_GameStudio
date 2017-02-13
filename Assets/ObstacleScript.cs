using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    public float interp = 0;
	// Use this for initialization
	void Start () {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[0], true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[1], true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[2], true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[3], true);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (gameObject.name == "Spinning Wall")
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, -90, interp));
            transform.position = Vector3.Lerp(new Vector3(36.56f, 0.27f, 0), new Vector3(40.3f, -4.5f, 0), interp);
        }
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
        if (gameObject.name == "Wall")
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(1000, 200));
        }
        if (gameObject.name == "Spinning Wall")
        {
            interp += 0.01f;
        }
    }
}
