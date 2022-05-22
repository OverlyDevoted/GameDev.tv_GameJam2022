using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class MoveAbility : Ability
{
    public float movementSpeed;
    public override void Activate(GameObject caller)
    {
        Movement movement = caller.AddComponent<Movement>();
        movement.movementSpeed = movementSpeed;
    }
}
