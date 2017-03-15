﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ArtifactScript : MonoBehaviour {
    [SerializeField] private int itemType; // 1 is hat, 2 is shirt
    [SerializeField] private ArtifactUIScript ui;
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
        GhostMove GM = col.gameObject.GetComponent<GhostMove>();
        if (GM != null)
        {
            GetComponent<Rigidbody2D>().simulated = false;
            transform.position = GM.GetObjectPos(itemType).position;
            transform.parent = GM.GetObjectPos(itemType);
            if (SceneManager.GetActiveScene().name.Contains("Andrew's"))
            {
                ui.isGotten = true;
            }
            Destroy(this);
        }
    }
}
