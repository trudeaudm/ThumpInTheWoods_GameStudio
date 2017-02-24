using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GhostMove : MonoBehaviour {

    public float windCharge = 5;
    public GameObject windLeavesParticles, GPMeter, ghostlyWindParticles, speechBubble;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = new Vector3(transform.position.x + 8.5f, 0, -10);
        if (GameObject.Find("Hat") == null) {
            speechBubble.SetActive(true);
            speechBubble.GetComponentInChildren<TextMesh>().text = "End of Alpha!";
        }
        if (transform.position.y <= -6) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            GetComponent<Animator>().SetBool("Walking", true);
            transform.Translate(-0.1f, 0, 0);
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            GetComponent<Animator>().SetBool("Walking", true);
            transform.Translate(0.1f, 0, 0);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walking", false);
        }
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space)) && GPMeter.GetComponent<Image>().fillAmount >= 0.01f)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.Translate(0, 0.05f, 0);
            GPMeter.GetComponent<Image>().fillAmount -= 0.02f;
        }
        if (Input.GetKeyUp(KeyCode.Space) || GPMeter.GetComponent<Image>().fillAmount < 0.01)
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
                GPMeter.GetComponent<Image>().fillAmount -= 0.5f;
                ghostlyWindParticles.GetComponent<ParticleSystem>().Clear();
                ghostlyWindParticles.GetComponent<ParticleSystem>().Emit(Mathf.RoundToInt(windCharge));
            }
            else if (windCharge <= 1)
            {
                GPMeter.GetComponent<Image>().fillAmount -= .1f;
                ghostlyWindParticles.GetComponent<ParticleSystem>().Clear();
                ghostlyWindParticles.GetComponent<ParticleSystem>().Emit(5);
            }
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - ghostlyWindParticles.transform.position;
            difference.Normalize();
            float rotz = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            ghostlyWindParticles.transform.rotation = Quaternion.Euler(-rotz, 90, 0f);
            windCharge = 5;
            windLeavesParticles.SetActive(false);
        }

        ParticleSystem.EmissionModule e = windLeavesParticles.GetComponent<ParticleSystem>().emission;
        e.rateOverTime = windCharge;
        GPMeter.GetComponent<Image>().fillAmount += 0.01f;
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
        if (other.GetComponent<ObstacleScript>() != null) {
            Destroy(other);
        }
    }

}
