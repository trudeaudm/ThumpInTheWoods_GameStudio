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
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectsOfType<ObstacleScript>()[0].gameObject.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectsOfType<ObstacleScript>()[1].gameObject.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectsOfType<ObstacleScript>()[2].gameObject.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectsOfType<ObstacleScript>()[3].gameObject.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectsOfType<ObstacleScript>()[4].gameObject.GetComponent<BoxCollider2D>(), true);
        Physics2D.IgnoreCollision(GetComponent<BoxCollider2D>(), FindObjectsOfType<ObstacleScript>()[5].gameObject.GetComponent<BoxCollider2D>(), true);
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
        speechBubble.GetComponentInChildren<TextMesh>().text = "That's a " + gameObject.name + "!";
    }

    public void OnMouseExit()
    {
        GameObject speechBubble = FindObjectOfType<GhostMove>().GetComponent<GhostMove>().speechBubble;
        speechBubble.SetActive(false);
    }
    void OnParticleCollision(GameObject other) {
        if (gameObject.GetComponent<MeshRenderer>().isVisible)
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
