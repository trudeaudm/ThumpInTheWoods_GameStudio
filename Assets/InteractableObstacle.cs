using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObstacle : MonoBehaviour {
    public float moveRate;
    public Transform obstacleOb;
    public Transform moveToPos;
    public float durability;
    public bool usePhysics;
    private Rigidbody2D myRB;
    private Transform curs;

    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        curs = GameObject.FindGameObjectWithTag("Cursor").transform;
        if (moveToPos)
        {
            moveToPos.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public void Activate()
    {
        if (usePhysics)
        {
            if (myRB && myRB.isKinematic == true)
            {
                myRB.isKinematic = false;
            }

            myRB.AddForce((curs.position - transform.position).normalized * moveRate/4, ForceMode2D.Impulse);
            
                
        }
        else
        {
            GetComponent<Collider2D>().enabled = false;
            obstacleOb.GetComponent<Collider2D>().enabled = true;
            StartCoroutine("MoveObstacle");
        }
        
    }
    IEnumerator MoveObstacle()
    {
        while (obstacleOb.rotation != moveToPos.rotation)
        {
                obstacleOb.position = Vector3.MoveTowards(obstacleOb.position, moveToPos.position, moveRate * Time.fixedDeltaTime);
                obstacleOb.rotation = Quaternion.RotateTowards(obstacleOb.rotation, moveToPos.rotation, moveRate * 20 * Time.fixedDeltaTime);
                yield return null;
        }
    }
    void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Wind")
        {
            durability = durability - 1;
            if (durability <= 0)
            {
                Activate();
            }
        }
    }

}
