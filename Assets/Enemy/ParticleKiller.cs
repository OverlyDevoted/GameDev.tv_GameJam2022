using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKiller : MonoBehaviour
{
    float destroyTime;

    // Start is called before the first frame update
    void Start()
    {
        destroyTime = Time.time + GetComponent<ParticleSystem>().duration;    
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyTime < Time.time)
            Destroy(gameObject);
    }
}
