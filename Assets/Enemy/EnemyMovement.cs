using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovement
{
    public float distance;
    public MovementState state;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = Vector2.zero;
            state = MovementState.reached;
            return Vector2.zero;
        }

        state = MovementState.following;
        return direction;
    }

    public void MoveTowards(Vector2 direction, float speed)
    {
        
        rb.AddForce(direction*speed, ForceMode2D.Force);
    }

}
public enum MovementState { following, reached }

