using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour {

    private Light myLight;
    public float flickerAmt;
    private float startBright;
	// Use this for initialization
	void Start () {
        myLight = GetComponent<Light>();
        startBright = myLight.intensity;
        InvokeRepeating("Flicker", 0, 0.05f);
	}
    void Flicker()
    {
        float flick = Random.Range(-flickerAmt, flickerAmt);
        myLight.intensity = Mathf.Clamp(myLight.intensity + flick, startBright - 1f, startBright + 1f);
    }
	
}
