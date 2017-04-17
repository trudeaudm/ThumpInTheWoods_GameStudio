using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyBehavior : MonoBehaviour {

    private ParticleSystem myPs;
    [SerializeField] private float flashRateMin, flashRateMax;
    [SerializeField] private float velChangeMin, velChangeMax;
    [SerializeField] private float velMin, velMax;
    [SerializeField] private float flockDist;

    private Transform parent;
    private Vector2 vel;
    void Start()
    {
        GameObject playOb = GameObject.FindGameObjectWithTag("Player");
        myPs = GetComponent<ParticleSystem>();
        float flashRate = Random.Range(flashRateMin, flashRateMax);
        Invoke("Flash", flashRate);
        parent = transform.parent.transform;
	}
	
	// Update is called once per frame
	void Flash () {
        myPs.Play();
        float flashRate = Random.Range(flashRateMin, flashRateMax);
        Invoke("Flash", flashRate);
        float velChangeRate = Random.Range(velChangeMin, velChangeMax);
        Invoke("GetNewVelocity", velChangeRate);
    }
    void GetNewVelocity()
    {
        float dist = Vector2.Distance(parent.position, transform.position);
        if (parent && dist < flockDist)
        {
            float velnewX = Random.Range(velMin, velMax);
            float velnewY = Random.Range(velMin, velMax);
            vel.x = Mathf.Clamp(vel.x + velnewX, -1, 1);
            vel.y = Mathf.Clamp(vel.y + velnewY, -1, 1);
        }
        else
        {
            vel = (parent.position - transform.position).normalized;
        }


        float velChangeRate = Random.Range(velChangeMin, velChangeMax);
        Invoke("GetNewVelocity", velChangeRate);
    }
    void FixedUpdate()
    {
        transform.Translate(vel * Time.fixedDeltaTime);
    }

}
