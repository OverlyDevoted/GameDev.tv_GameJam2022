using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> spawnerNodeTypes;
    List<GameObject> initialSpawner;
    public float nodeSpawnRate;
    float startSpawnRate;
    public float nodeSpawnRateModifier;
    float currentSpawnRate;
    public float randomizerMinMax;
    public float waveTransitionRate;
    float currentWaveRate;
    float currentWave;
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
        OffsetWaveTime();
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

        if(currentWaveRate <= Time.time)
        {

            if (nodeSpawnRate >= 1.2)
                nodeSpawnRate = nodeSpawnRate * nodeSpawnRateModifier;
            currentWave++;
            if (currentLevel < spawnerNodeTypes.Count - 1 && currentWave ==3)
            {
                currentWave = 0;
                nodeSpawnRate = startSpawnRate;
                currentLevel++;
            }

            OffsetLevelTime();
            OffsetWaveTime();
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
    void OffsetWaveTime()
    {
        currentWaveRate = Time.time + waveTransitionRate;
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

        OffsetSpawnerTime();
        OffsetLevelTime();
    }
}
