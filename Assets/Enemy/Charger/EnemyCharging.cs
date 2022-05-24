using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharging : MonoBehaviour, IEnemyAction
{
    public float chargingTime = 2f;
    float currentCharge;
    public float chargePower;
    Rigidbody2D rb;
    Transform target;
    bool isReady;
    public float inaccuracyAngle = 10f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GetComponent<Movement>().target;

        currentCharge = Time.time + chargingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentCharge < Time.time)
        {
            Action();
            currentCharge = Time.time + chargingTime;
        }
    }

    public void Action()
    {
        
        float currentAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        Vector3 randomized = Quaternion.AngleAxis(currentAngle + Random.Range(-inaccuracyAngle, inaccuracyAngle), Vector3.forward) * Vector3.right;
        transform.up = randomized;
        rb.AddForce(transform.up* chargePower, ForceMode2D.Impulse);
    }
    public void Enable(bool enable)
    {
        isReady = enable;
    }
}
