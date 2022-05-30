using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovement
{
    public float distance;
    public MovementState state;
    Rigidbody2D rb;
    public float followingDrag = 10f;
    public float reachedDrag = 0f;
    public bool disableVelocityOnReached = true;
    public bool initiallyDisableVelocity = false;
    bool hasDisabled;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(!initiallyDisableVelocity)
            hasDisabled = true;
    }
    public Vector2 GetDirection()
    {
        throw new System.NotImplementedException();
    }

    public Vector2 GetDirection(Vector2 towards)
    {
        if (state == MovementState.stunned)
            return Vector2.zero;

        Vector2 direction = (towards - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.up = direction;
        if (Vector2.Distance(transform.position, towards) < distance)
        {
            if(disableVelocityOnReached || !hasDisabled)
            {
                rb.velocity = Vector2.zero;
                hasDisabled = true;
            }
            if (rb.drag != reachedDrag)
                SetDrag(reachedDrag);
            state = MovementState.reached;
            return Vector2.zero;
        }
        if (rb.drag != followingDrag)
            SetDrag(followingDrag);
        if (initiallyDisableVelocity)
            hasDisabled = false;
        state = MovementState.following;
        return direction;
    }

    public void MoveTowards(Vector2 direction, float speed)
    {
        if(state!= MovementState.reached)
        rb.AddForce(direction*10f*speed, ForceMode2D.Force);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !gameObject.CompareTag("KillBullet"))
        {
            rb.AddForce(collision.gameObject.transform.up*10f*collision.GetComponent<BulletModifiers>().knockback, ForceMode2D.Impulse);
        }
    }
    public void ChargeTowards(Vector3 from, Vector3 to, float procentage)
    {
        throw new System.NotImplementedException();
    }
    public void SetDrag(float drag)
    {
        rb.drag = drag;
    }
}
public enum MovementState { following, reached, stunned }

