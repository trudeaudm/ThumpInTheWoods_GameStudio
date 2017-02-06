using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GhostMove : MonoBehaviour {

    public float windCharge = 0;
    public GameObject windLeavesParticles, GPMeter, ghostlyWindParticles, speechBubble;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            transform.Translate(-0.1f, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(0.1f, 0, 0);
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && GPMeter.GetComponent<Slider>().value >= 1)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.Translate(0, 0.05f, 0);
            GPMeter.GetComponent<Slider>().value -= 1f;
        }
        if (Input.GetKeyUp(KeyCode.Space) || GPMeter.GetComponent<Slider>().value < 1)
        {
            GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        if (Input.GetMouseButton(0)) {
            windLeavesParticles.SetActive(true);
            ParticleSystem.MainModule m = windLeavesParticles.GetComponent<ParticleSystem>().main;
            m.startLifetime = Mathf.Infinity;
            windCharge += 0.1f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (windCharge > 1)
            {
                GPMeter.GetComponent<Slider>().value -= 50;
                ghostlyWindParticles.GetComponent<ParticleSystem>().Emit(Mathf.RoundToInt(windCharge));
            }
            else if (windCharge <= 1)
            {
                GPMeter.GetComponent<Slider>().value -= 10;
                ghostlyWindParticles.GetComponent<ParticleSystem>().Emit(5);
            }
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ghostlyWindParticles.transform.position;
            difference.Normalize();
            float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            ghostlyWindParticles.transform.rotation = Quaternion.Euler(-rotz, 90, 0f);
            windCharge = 0;
            windLeavesParticles.SetActive(false);
        }

        ParticleSystem.EmissionModule e = windLeavesParticles.GetComponent<ParticleSystem>().emission;
        e.rateOverTime = windCharge;
        GPMeter.GetComponent<Slider>().value += 0.1f;
    }

    public void OnMouseOver() {
        speechBubble.SetActive(true);
        speechBubble.GetComponentInChildren<TextMesh>().text = "It's me!";
    }
    public void OnMouseExit()
    {
        speechBubble.SetActive(false);
    }
    public void OnParticleCollision (GameObject other)
    {
        print("stuff");
        if (other.GetComponent<ObstacleScript>() != null) {
            Destroy(other);
        }
    }

}
