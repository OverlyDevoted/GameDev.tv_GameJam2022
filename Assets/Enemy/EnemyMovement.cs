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
        Vector2 direction = (towards - new Vector2(transform.position.x, transform.position.y)).normalized;
        transform.up = direction;
        if (Vector2.Distance(transform.position, towards) < distance)
        {
            if(disableVelocityOnReached || !hasDisabled)
            {
                rb.velocity = Vector2.zero;
                hasDisabled = true;
            }
            if(rb.drag!=reachedDrag)
            rb.drag = reachedDrag;
            state = MovementState.reached;
            return Vector2.zero;
        }
        if(rb.drag!=followingDrag)
        rb.drag = followingDrag;
        if (initiallyDisableVelocity)
            hasDisabled = false;
        state = MovementState.following;
        return direction;
    }

    public void MoveTowards(Vector2 direction, float speed)
    {
        if(state!= MovementState.reached)
        rb.AddForce(direction*speed, ForceMode2D.Force);
    }

    public void ChargeTowards(Vector3 from, Vector3 to, float procentage)
    {
        throw new System.NotImplementedException();
    }
}
public enum MovementState { following, reached }

