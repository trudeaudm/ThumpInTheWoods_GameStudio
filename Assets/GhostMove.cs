using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour {

    public float windCharge = 0;
    public GameObject windLeavesParticles;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(0.1f, 0, 0);
        }
        if (Input.GetMouseButton(0)) {
            windCharge += 0.01f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            direction.Normalize();
            windCharge = 0;
        }
        ParticleSystem.EmissionModule e = windLeavesParticles.GetComponent<ParticleSystem>().emission;
        e.rateOverTime = windCharge;
    }
    
}
