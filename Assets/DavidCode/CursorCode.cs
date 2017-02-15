using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorCode : MonoBehaviour {
    [SerializeField]private Transform particles, cursor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Cursor.visible = false;
        transform.position = Input.mousePosition;
        Vector3 particlePos;
        particlePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        particlePos.z = -2.0f;
        particles.position = particlePos;
        cursor.position = particlePos;
	}
}
