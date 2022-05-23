using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 1f;
    public Transform target;
    public IMovement movement;

    void Start()
    {
        movement = gameObject.GetComponent<IMovement>();
        if(movement.GetType() == typeof(EnemyMovement))
            target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(movement == null)
            return;

        if (target == null)
        {
            movement.MoveTowards(movement.GetDirection(), movementSpeed);
        }
        else
        {
            movement.MoveTowards(movement.GetDirection(target.transform.position), movementSpeed);
        }
    }
}
public interface IMovement
{
    public void MoveTowards(Vector2 direction, float speed);
    public Vector2 GetDirection();
    public Vector2 GetDirection(Vector2 towards);
}
