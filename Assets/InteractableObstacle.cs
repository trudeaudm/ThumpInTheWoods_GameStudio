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
        curs = GameObject.FindGameObjectWithTag("Cursor").transform;
    }

    public void Activate(GameObject other)
    {
        if (usePhysics)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (!rb)
            {
                myRB = gameObject.AddComponent<Rigidbody2D>();
            }

            myRB.AddForce((curs.position - transform.position).normalized *  0.5f, ForceMode2D.Impulse);
            
                
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
                Activate(other);
            }
        }
    }

}
