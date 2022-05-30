using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public float movementSpeed = 1f;
    public Transform target;
    public IMovement movement;
    public MovementStateInner state;
    void Start()
    {
        movement = gameObject.GetComponent<IMovement>();
        if(movement.GetType() == typeof(EnemyMovement))
            target = GameObject.FindGameObjectWithTag("Player").transform;
        state = MovementStateInner.idle;
    }

    // Update is called once per frame
    void Update()
    {
        if(movement == null)
            return;
        Vector2 direction = Vector2.zero;
        if (target == null)
        {
            direction = movement.GetDirection();
        }
        if (direction.magnitude > 0)
            state = MovementStateInner.moving;
        else
            state = MovementStateInner.idle;
        movement.MoveTowards(direction, movementSpeed);
    }
    private void FixedUpdate()
    {
        if(target != null)
        {
            movement.MoveTowards(movement.GetDirection(target.transform.position), movementSpeed);
        }
    }
}
public interface IMovement
{
    public void ChargeTowards(Vector3 from, Vector3 to, float procentage);
    public void MoveTowards(Vector2 direction, float speed);
    public Vector2 GetDirection();
    public Vector2 GetDirection(Vector2 towards);
}
public enum MovementStateInner
{
    moving, idle
}