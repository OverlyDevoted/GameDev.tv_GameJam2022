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

    List<int> randomNodes = new List<int>();
    private void Awake()
    {
        startSpawnRate = nodeSpawnRate;

        for (int i = 0; i < 3; i++)
        {
            randomNodes.Add(0);
        }
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
            int nodeType = Random.Range(0,3);
            Instantiate(spawnerNodeTypes[randomNodes[nodeType]], spawnPosition, Quaternion.identity, transform.parent);
            OffsetSpawnerTime();
        }

        if(currentWaveRate <= Time.time)
        {

            if (nodeSpawnRate >= 1.2)
                nodeSpawnRate = nodeSpawnRate * nodeSpawnRateModifier;
            currentWave++;
            if (currentLevel < spawnerNodeTypes.Count && currentWave ==3)
            {
                nodeSpawnRate = startSpawnRate;
                currentLevel++;
            }
            //Debug.Log( "Wave " +currentWave);

            //Debug.Log("Level " + currentLevel);
            if (currentWave == 3)
            {
                //Debug.Log("Wave node type list of level " + currentLevel);
                for(int i=0;i<3;i++)
                {    
                    int index = Random.Range(0, currentLevel);
                    if (currentLevel >= 3)
                    {
                        randomNodes[i] = index;
                    }
                    else if (currentLevel == 2)
                    {
                        randomNodes[currentLevel-2] = 0;
                        randomNodes[currentLevel] = 2;
                        randomNodes[currentLevel - 1] = 1;
                        continue;
                    }
                    else
                    {
                        randomNodes[0] = 1;
                        continue;
                    }
                }
                
                string lvl = "";
                foreach (int skaicius in randomNodes)
                    lvl += skaicius.ToString() + " ";
                //Debug.Log(lvl);
                currentWave = 0;
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
        currentWave = 0;

        for (int i = 0; i < 3; i++)
            randomNodes[i] = 0;
    }
    public void EnableSpawner()
    {
        this.gameObject.SetActive(true);
        nodeSpawnRate = startSpawnRate;


            OffsetSpawnerTime();
        OffsetLevelTime();
    }
}
