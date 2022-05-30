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
    Animator animator;
    public AudioClip sfx;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 1 / fireRate);
        currentFire = Time.time + fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFire <= Time.time&&isReady)
        {
            Action();
            currentFire = Time.time + fireRate;
            animator.SetTrigger("Charge");
            animator.SetFloat("Speed", 1 / fireRate);
        }
        if(currentFire-0.01f <= Time.time && !isReady)
            animator.SetFloat("Speed", 0);
        
    }

    public void Action()
    {
        AudioManager.PlayClip(sfx);
        Instantiate(bullet, transform.position + transform.up * shootFromDistance, transform.rotation);
    }

    public void Enable(bool enable)
    {
        isReady = enable;
    }
}
