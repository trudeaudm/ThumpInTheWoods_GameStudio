using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScript : MonoBehaviour {

    bool hasleft, hasright;
    public float moverate;
    float spriteWidth;
	// Use this for initialization
	void Start () {
        spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
        if (!hasleft || !hasright) {
            float camHorizontalExtend = Camera.main.orthographicSize * Screen.width / Screen.height;
            float edgeVisiblePositionRight = (transform.position.x + spriteWidth / 2) - camHorizontalExtend;
            float edgeVisiblePositionLeft = (transform.position.x - spriteWidth / 2) + camHorizontalExtend;
            if (Camera.main.transform.position.x >= edgeVisiblePositionRight && !hasright) {
                MakeANewBuddy(1);
                hasright = true;
            }
            if (Camera.main.transform.position.x <= edgeVisiblePositionLeft && !hasleft)
            {
                MakeANewBuddy(-1);
                hasleft = true;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moverate, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moverate, 0, 0);
        }
    }

    void MakeANewBuddy(int rightorleft) {
        Vector3 newposition = new Vector3(transform.position.x + spriteWidth * rightorleft, transform.position.y, transform.position.z);
        Transform newBuddy = Instantiate(transform, newposition, transform.rotation) as Transform;
        newBuddy.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        if (rightorleft > 0) {
            newBuddy.GetComponent<ParallaxScript>().hasleft = true;
        }
        else if (rightorleft < 0)
        {
            newBuddy.GetComponent<ParallaxScript>().hasright = true;
        }
    }
}
