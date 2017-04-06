using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactScript : MonoBehaviour {
    [SerializeField] private int itemType; // 1 is hat, 2 is shirt

    public void OnCollisionEnter2D(Collision2D col)
    {
        GhostMove GM = col.gameObject.GetComponent<GhostMove>();
        if (GM != null)
        {
            GetComponent<Rigidbody2D>().simulated = false;
            transform.position = GM.GetObjectPos(itemType).position;
            transform.parent = GM.GetObjectPos(itemType);
            SpriteRenderer SR = GetComponent<SpriteRenderer>();
            Color transColor = SR.color;
            transColor.a = 0.5f;
            SR.color = transColor;
            Destroy(this);
        }
    }
}
