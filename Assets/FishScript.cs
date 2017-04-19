using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour {

    public Vector3 startpoint, endpoint;
    public float hopHeight;
    public Sprite[] sprites;
    int[] indeces = { 0, 1, 2, 3, 4, 3, 2, 1, 0, 5, 6, 7, 8, 7, 6, 5 };
    int indexofindeces = 0;
    float timer;
    bool forward;

    // Use this for initialization
    void Start () {
        timer = 0;
        forward = true;
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<SpriteRenderer>().sprite = sprites[indeces[indexofindeces]];
        indexofindeces = (indexofindeces + 1) % indeces.Length;
        if (timer <= 1)
        {
            if (forward)
            {
                float angle = Mathf.LerpAngle(-80, 80, timer);
                transform.eulerAngles = new Vector3(0, 0, angle);
                float height = Mathf.Sin(Mathf.PI * timer) * hopHeight;
                transform.position = Vector3.Lerp(startpoint, endpoint, timer) + Vector3.up * height;
            }
            if (!forward)
            {
                float angle = Mathf.LerpAngle(80, -80, timer);
                transform.eulerAngles = new Vector3(0, 0, angle);
                float height = Mathf.Sin(Mathf.PI * timer) * hopHeight;
                transform.position = Vector3.Lerp(endpoint, startpoint, timer) + Vector3.up * height;
            }
        }
        timer += Time.deltaTime;
        if (timer > 2)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
            forward = !forward;
            timer = 0;
        }
    }
}
