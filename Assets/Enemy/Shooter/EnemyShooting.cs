using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public float fireRate;
    float currentFire;
    float shootFromDistance = 0.2f;
    public bool isReady = false;
    public void Fire()
    {
        Instantiate(bullet,transform.position + transform.up*shootFromDistance,transform.rotation);
    }
    // Start is called before the first frame update
    void Start()
    {
        currentFire = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFire <= Time.time && isReady)
        {
            Fire();
            currentFire = Time.time + fireRate;
        }
    }
}
