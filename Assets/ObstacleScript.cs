using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour {

    public float interp = 0;
    bool windtrigger = false;
    Vector3 prevpos;
    // Use this for initialization
    void Start () {
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[0], true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[1], true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[2], true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectOfType<GhostMove>().GetComponentsInChildren<EdgeCollider2D>()[3], true);
        for (int x = 0; x < FindObjectsOfType<ObstacleScript>().Length; x++)
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), FindObjectsOfType<ObstacleScript>()[x].gameObject.GetComponent<Collider2D>(), true);
        }
        prevpos = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (windtrigger && interp < 1) {
            interp += 0.01f;
        }
        if (interp == 1)
        {
            windtrigger = false;
        }
        if (gameObject.name.Contains("Wall") && !gameObject.name.Contains("Spinning"))
        {
            transform.position = Vector3.Lerp(prevpos, prevpos + new Vector3(100, 50, 0), interp);
        }
        if (gameObject.name.Contains("Spinning Wall"))
        {
            transform.eulerAngles = new Vector3(0, 0, Mathf.LerpAngle(0, -90, interp));
        }
    }

    public void OnMouseOver()
    {
        GameObject speechBubble = FindObjectOfType<GhostMove>().GetComponent<GhostMove>().speechBubble;
        speechBubble.SetActive(true);
        if (gameObject.name.Contains("Wall") && !gameObject.name.Contains("Spinning"))
        {
            speechBubble.GetComponentInChildren<TextMesh>().text = "That's a Wall!\nThat could be moved\nwith a quick click!";
        }
        if (gameObject.name.Contains("Spinning Wall"))
        {
            speechBubble.GetComponentInChildren<TextMesh>().text = "That's a Spinning Wall!\nThat might need more\nof a charge to move it\nout of the way!";
        }
    }
    public void OnMouseExit()
    {
        GameObject speechBubble = FindObjectOfType<GhostMove>().GetComponent<GhostMove>().speechBubble;
        speechBubble.SetActive(false);
    }
    void OnParticleCollision(GameObject other) {
        if (gameObject.GetComponent<Renderer>().isVisible && other.name == "Ghostly Wind")
        {
            if (gameObject.name.Contains("Wall") && !gameObject.name.Contains("Spinning"))
            {
                windtrigger = true;
            }
            if (gameObject.name == "Spinning Wall" && other.GetComponent<ParticleSystem>().particleCount > 6)
            {
                windtrigger = true;
            }
        }
        other.GetComponent<ParticleSystem>().Clear();
    }
}
