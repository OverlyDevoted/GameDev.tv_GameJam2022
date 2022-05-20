using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNode : MonoBehaviour
{
    public GameObject enemy;
    public float spawnDelay = 3f;
    float timeToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        timeToSpawn = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeToSpawn <= Time.time)
        {
            Instantiate(enemy, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
