using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {
    public float vYMin, vYMax, vXMin, vXMax;
    public float durationMin, durationMax;
    public float width;
    public float crackInterval;
    public float blinkMin, blinkMax;
    public GameObject lightningChild;
    public int lightSeries;
    public GameObject thunder1, thunder2, thunder3;
    private TrailRenderer myTrail;
    // Use this for initialization
    void Start () {
        myTrail = GetComponent<TrailRenderer>();
        myTrail.widthMultiplier = width;
        InvokeRepeating("Crack", crackInterval, crackInterval);
        Destroy(gameObject, Random.Range(durationMin, durationMax));
        if (!transform.parent)
        {
            InvokeRepeating("Blink", 0, Random.Range(blinkMin, blinkMax));
            float aud = Random.Range(1.0f, 4.0f);
            if (aud >= 1 && aud < 2)
            {
                GameObject thunderClap = Instantiate(thunder1);
                thunderClap.transform.position = transform.position;
                Destroy(thunderClap, 10);
            }
            else if (aud >= 2 && aud < 3)
            {
                GameObject thunderClap = Instantiate(thunder2);
                thunderClap.transform.position = transform.position;
                Destroy(thunderClap, 10);
            }
            else if (aud >= 3 && aud < 4)
            {
                GameObject thunderClap = Instantiate(thunder3);
                thunderClap.transform.position = transform.position;
                Destroy(thunderClap, 10);
            }
        }
    }
    void Crack()
    {
        float velY = Random.Range(vYMin, vYMax);
        float velX = Random.Range(vXMin, vXMax);
        Vector2 newPos = transform.position;
        newPos.y = newPos.y + velY;
        newPos.x = newPos.x + velX;
        transform.position = newPos;
        int split = Random.Range(0, 2);
        if (lightSeries > 0 && split == 1)
        {
            GameObject lightChild = Instantiate(lightningChild);
            lightChild.GetComponent<Lightning>().SetLightSeries(lightSeries - 1);
            lightSeries = lightSeries - 1;
            lightChild.transform.position = transform.position;
            lightChild.transform.parent = this.transform;
        }
    }
    void Blink()
    {
        if (gameObject.activeInHierarchy == true)
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
    void SetLightSeries(int series)
    {
        lightSeries = series;
    }

}
