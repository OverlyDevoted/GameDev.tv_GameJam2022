using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, IMovement
{
    public Vector2 GetDirection()
    {
        throw new System.NotImplementedException();
    }

    public Vector2 GetDirection(Vector2 towards)
    {
        return (towards - new Vector2(transform.position.x, transform.position.y)).normalized;
    }

    public void MoveTowards(Vector2 direction, float speed)
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

}
