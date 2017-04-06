using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RecallPosition : MonoBehaviour {
    [SerializeField] private List<Vector2> positions = new List<Vector2>();
    [SerializeField] private float timeBetweenPositions, distanceBetweenPositions;
    [SerializeField] private Vector2 startPos;
    private bool grounded;
    private GhostMove myGhost;
    private bool isRecalling;
    private Vector2 recallPos;
    private LightningSpawner lSpawn;
	// Use this for initialization
	void Start () {
        lSpawn = FindObjectOfType<LightningSpawner>();
        startPos = transform.position;
        myGhost = GetComponent<GhostMove>();
        InvokeRepeating("AddPosition", 0, timeBetweenPositions);
	}
    void AddPosition()
    {
        if (grounded)
        {
            if (positions.Count() > 0 && Vector2.Distance(positions.Last(), transform.position) > distanceBetweenPositions)
            {
                positions.Add(transform.position);
            }
            else if (positions.Count() == 0)
            {
                positions.Add(transform.position);
            }
        }
    }
    public void WakeAtLastPosition()
    {
        if (!isRecalling)
        {
            if (positions.Count() > 0)
            {
                recallPos = positions.Last();
                positions.Remove(positions.Last());
            }
            else
            {
                recallPos = startPos;
            }
            isRecalling = true;
            Invoke("Recall", 1.0f);
        }
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "BaseTerrain")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }
    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "BaseTerrain")
        {
            //AddPosition();
            grounded = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Wet")
        {
            WakeAtLastPosition();
            lSpawn.ZapPositionMinor(transform.position);
        }
    }
    void Recall()
    {
        transform.position = recallPos;
        myGhost.SetGhostPower(1.5f);
        isRecalling = false;
    }
    


}
