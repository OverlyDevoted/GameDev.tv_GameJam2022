using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> spawnerNodeTypes;
    public float nodeSpawnRate = 1.0f;
    float currentSpawnRate;
    public float randomizerMinMax = 0.2f;
    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerManager>().transform;
        OffsetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpawnRate <= Time.time)
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Camera.main.pixelWidth), Random.Range(0, Camera.main.pixelHeight)));
            spawnPosition.z = 0f;
            Instantiate(spawnerNodeTypes[0], spawnPosition, Quaternion.identity, transform.parent);
            OffsetTimer();
        }
    }
    void OffsetTimer()
    {
        currentSpawnRate = Time.time + nodeSpawnRate + Random.Range(-randomizerMinMax, randomizerMinMax);
    }
    public void DisableSpawner()
    {
        this.gameObject.SetActive(false);
    }
    public void EnableSpawner()
    {
        this.gameObject.SetActive(true);
    }
}
