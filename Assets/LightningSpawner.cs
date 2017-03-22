using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningSpawner : MonoBehaviour {
    public GameObject lightning;
    public float minInterval, maxInterval;
    public float lightBoundMin, lightBoundMax;
    private bool isZapping;
	// Use this for initialization
	void Start () {
        //InvokeRepeating("SpawnLightning", 0, 1.0f);

        SpawnLightning();
	}
    void SpawnLightning()
    {
        float lightningDelay = Random.Range(minInterval, maxInterval);
        Invoke("Lightning", lightningDelay);
        float nextSpawn = Random.Range(minInterval, maxInterval);
        Invoke("SpawnLightning", nextSpawn);
    }
    void Lightning()
    {
        Vector2 spawnPos = transform.position;
        float offset = Random.Range(lightBoundMin, lightBoundMax);
        spawnPos.x = spawnPos.x + offset;
        GameObject newLight = Instantiate(lightning);
        newLight.transform.position = spawnPos;

    }
    public void ZapPosition(Vector2 pos)
    {
        if (!isZapping)
        {
            isZapping = true;
            Vector2 spawnPos = pos;
            float offsetX = Random.Range(-1, 1);
            float offsetY = Random.Range(0, 2);
            spawnPos.x = spawnPos.x + offsetX;
            spawnPos.y = spawnPos.y + offsetY;
            GameObject newLight = Instantiate(lightning);
            Lightning light = newLight.GetComponent<Lightning>();
            light.vXMin = -3.0f;
            light.vXMax = 3.0f;
            light.vYMin = -3.0f;
            light.vYMax = 3.0f;
            light.width = 0.3f;
            newLight.transform.position = spawnPos;
            Invoke("ResetZap", 0.6f);
        }
    }
    public void ZapPositionMinor(Vector2 pos)
    {
        if (!isZapping)
        {
            isZapping = true;
            Vector2 spawnPos = pos;
            float offsetX = Random.Range(-1, 1);
            float offsetY = Random.Range(0, 2);
            spawnPos.x = spawnPos.x + offsetX;
            spawnPos.y = spawnPos.y + offsetY;
            GameObject newLight = Instantiate(lightning);
            Lightning light = newLight.GetComponent<Lightning>();
            light.vXMin = -3.0f;
            light.vXMax = 3.0f;
            light.vYMin = -3.0f;
            light.vYMax = 3.0f;
            light.width = 0.2f;
            light.durationMin = 0.3f;
            light.durationMax = 0.6f;
            light.blinkMax = 0.02f;
            newLight.transform.position = spawnPos;
            Invoke("ResetZap", 0.65f);
        }
    }
    void ResetZap()
    {
        isZapping = false;
    }
	
}
