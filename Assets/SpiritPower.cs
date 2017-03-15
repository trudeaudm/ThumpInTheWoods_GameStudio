using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritPower : MonoBehaviour {

    public float powerAmt;
    private GhostMove player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<GhostMove>();
    }
    public void OnParticleCollision(GameObject other)
    {
        if (other.gameObject == player.gameObject)
        {
            StartCoroutine(player.ModifyGhostlyPower(true, powerAmt, 0.005f));
        }
    }

}
