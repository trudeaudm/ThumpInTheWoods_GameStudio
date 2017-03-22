using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubblePosition : MonoBehaviour {

    [SerializeField] private Vector2 offset;
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
	// Update is called once per frame
	void Update () {
        Vector3 speechPos = player.position;
        speechPos.y = speechPos.y + offset.y;
        speechPos.x = speechPos.x + offset.x;
        speechPos = Camera.main.WorldToScreenPoint(speechPos);
        transform.position = speechPos;
    }
}
