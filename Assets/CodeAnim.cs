using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeAnim : MonoBehaviour {
    public float rate;
    public bool isBreathing;
    public Vector2 breathAmt;
    private bool exhale;
    private Vector2 startPos;
	// Use this for initialization
	void Start () {
        startPos = transform.position;
        if (isBreathing)
        {
            InvokeRepeating("Breath", 0, 0.05f);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Breath()
    {
        if (!exhale)
        {
            Vector3 newSize = new Vector3(1 * breathAmt.x, 1 * breathAmt.y, 1);
            Vector3 newPos = startPos;
            newPos.y = newPos.y + breathAmt.y;
            transform.localScale = Vector3.MoveTowards(transform.localScale, newSize, rate * Time.fixedDeltaTime);
            transform.position = Vector3.MoveTowards(transform.position, newPos, rate * Time.fixedDeltaTime);
            if (transform.localScale == newSize)
            {
                exhale = true;
            }
        }
        else
        {
            Vector3 newSize = new Vector3(1, 1, 1);
            transform.localScale = Vector3.MoveTowards (transform.localScale, newSize, rate * Time.fixedDeltaTime);
            transform.position = Vector3.MoveTowards(transform.position, startPos, rate * Time.fixedDeltaTime);
            if (transform.localScale == newSize)
            {
                exhale = false;
            }
        }
    }
}
