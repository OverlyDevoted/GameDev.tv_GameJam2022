using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour, IEnemyAction
{
    public GameObject bullet;
    public float fireRate;
    float currentFire;
    float shootFromDistance = 0.2f;
    bool isReady;
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
            Action();
            currentFire = Time.time + fireRate;
        }
    }

    public void Action()
    {
        Instantiate(bullet, transform.position + transform.up * shootFromDistance, transform.rotation);
    }

    public void Enable(bool enable)
    {
        /*if(isReady != enable)
            currentFire = Time.time + fireRate;
        */isReady = enable;
    }
}
