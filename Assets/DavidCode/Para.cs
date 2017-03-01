using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Para : MonoBehaviour {
    [SerializeField] GameObject workLight;
    private Transform mainCam;
    [SerializeField]private float xRate, yRate;
    // Use this for initialization
    void Start () {
        workLight.SetActive(false);
        transform.localScale = new Vector3(1, 1, 1);
        mainCam = Camera.main.transform;
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 offset = mainCam.position;
        offset.x = offset.x / xRate;
        offset.y = offset.y / yRate;
        transform.position = Vector2.Lerp(transform.position, offset, 0.5f);
	}
}
