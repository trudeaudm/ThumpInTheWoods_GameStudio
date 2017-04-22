using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritPower : MonoBehaviour {
    public bool canTriggerAI;
    public float powerAmt;
    private GhostMove player;
    void Start()
    {
        GameObject playOb = GameObject.FindGameObjectWithTag("Player");
        if (playOb)
        {
            player = playOb.GetComponent<GhostMove>();
        }

    }
    public void OnParticleCollision(GameObject other)
    {
        if (other.gameObject == player.gameObject)
        {
            StartCoroutine(player.ModifyGhostlyPower(true, powerAmt, 0.005f));
        }
        if (canTriggerAI && other.tag == "CreatureAI")
        {
            other.GetComponent<FadeAwayAI>().TriggerFade();
        }
    }

}
