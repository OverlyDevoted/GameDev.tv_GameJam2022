using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public float bulletSpeed = 5f;
    public float lifeTime = 0.75f;
    public float inaccuracyAngle = 10f;
    public float knockback = 1f;
    float currentLifeTime;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        currentLifeTime = Time.time + lifeTime;
        rb = GetComponent<Rigidbody2D>();
        float currentAngle = Mathf.Atan2(transform.up.y, transform.up.x) * Mathf.Rad2Deg;
        Vector3 randomized = Quaternion.AngleAxis(currentAngle + Random.Range(-inaccuracyAngle,inaccuracyAngle), Vector3.forward) * Vector3.right;
        transform.up = randomized;
        rb.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }
    private void Update()
    {
        if (currentLifeTime <= Time.time)
            Destroy(gameObject);
    }
}
