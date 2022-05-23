using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> spawnerNodeTypes;
    public float nodeSpawnRate;
    float startSpawnRate;
    public float nodeSpawnRateModifier;
    float currentSpawnRate;
    public float randomizerMinMax;
    public float levelTransitionRate;
    float currentTransitionRate;
    public int currentLevel = 0;
    private void Awake()
    {

        startSpawnRate = nodeSpawnRate;
    }
    // Start is called before the first frame update
    void Start()
    {
        OffsetSpawnerTime();
        OffsetLevelTime();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSpawnRate <= Time.time)
        {
            Vector3 spawnPosition = Camera.main.ScreenToWorldPoint(new Vector3(Random.Range(0, Camera.main.pixelWidth), Random.Range(0, Camera.main.pixelHeight)));
            spawnPosition.z = 0f;
            int nodeType = Mathf.RoundToInt(Random.Range(0, currentLevel*1.0f));
            Debug.Log(nodeType);
            Instantiate(spawnerNodeTypes[nodeType], spawnPosition, Quaternion.identity, transform.parent);
            OffsetSpawnerTime();
        }
        if(currentTransitionRate <= Time.time)
        {
            if (currentLevel < spawnerNodeTypes.Count - 1)
            {
                currentLevel++;
            }

            if (nodeSpawnRate >= .3f)
                nodeSpawnRate = nodeSpawnRate * nodeSpawnRateModifier;
            OffsetLevelTime();
            
        }
    }
    void OffsetSpawnerTime()
    {
        currentSpawnRate = Time.time + nodeSpawnRate + Random.Range(-randomizerMinMax, randomizerMinMax);
    }
    void OffsetLevelTime()
    {
        currentTransitionRate = Time.time + levelTransitionRate;
    }
    public void DisableSpawner()
    {
        this.gameObject.SetActive(false);
        currentLevel = 0;
    }
    public void EnableSpawner()
    {
        this.gameObject.SetActive(true);
        nodeSpawnRate = startSpawnRate;
    }
}
