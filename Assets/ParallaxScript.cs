using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour {

    bool hasleft, hasright;
    public float moverate;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        hasleft = transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x / 2 < Camera.main.transform.position.x + Screen.width/2;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moverate, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moverate, 0, 0);
        }
    }
}
