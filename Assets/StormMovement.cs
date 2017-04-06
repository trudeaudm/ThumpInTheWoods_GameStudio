using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormMovement : MonoBehaviour {
    private Transform player;
    public float maxOffsetX, offsetY;
    private LightningSpawner lSpawner;
	// Use this for initialization
	void Start () {
        lSpawner = FindObjectOfType<LightningSpawner>();

        GameObject playOb = GameObject.FindGameObjectWithTag("Player");
        if (playOb)
        {
            player = playOb.transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (player)
        {
            float offset = player.position.x - transform.position.x;
            float xOffset = Mathf.Abs(offset);
            if (xOffset > maxOffsetX)
            {
                Vector2 newPos = transform.position;
                newPos.x = Mathf.Lerp(newPos.x, newPos.x + 1.0f, 2 * Time.fixedDeltaTime);
                transform.position = newPos;
            }
            Vector2 newPosY = transform.position;
            newPosY.y = Mathf.Lerp(newPosY.y, player.transform.position.y + offsetY, 2 * Time.fixedDeltaTime);
            transform.position = newPosY;
        }

    }
    void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<RecallPosition>().WakeAtLastPosition();
            lSpawner.ZapPosition(player.transform.position);
        }

    }
}
