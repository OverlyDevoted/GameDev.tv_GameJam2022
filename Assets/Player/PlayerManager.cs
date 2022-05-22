using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public Transform spawnPosition;
    public UnityEvent OnKilled;
    public UnityEvent OnReincarnate;
    public UnityEvent OnDeath;
    private Movement movement;

    public Ability movementAb;
    float currentMovement;
    
    public Ability attack;
    float currentAttack;

    public Ability defense;
    float currentDefence;
    
    Ability toInherit;
    // Start is called before the first frame update
    void Start()
    {
        ResetPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Kill"))
        {
            OnKilled.Invoke();
            Debug.Log("Death");
            toInherit = collision.GetComponent<EnemyManager>().ability;
            if(movement!=null)
            movement.enabled = false;
        }
    }

    public void ResetPlayer()
    {
        transform.position = spawnPosition.position;
        
    }
    public void Reincarnate()
    {

        OnReincarnate.Invoke();
        switch (toInherit.name) 
        {
            case "Move":
                if(movement == null)
                {
                    movementAb = toInherit;
                    movementAb.Activate(this.gameObject);
                    movement = GetComponent<Movement>();
                    movement.enabled = false;
                }

                break;
            case "Attack":
                attack = toInherit;
                break;
            case "Defense":
                defense = toInherit;
                break;
        }


    }
    public void DisableAbilities()
    {
        if(movement != null)
        movement.enabled = false;
    }
    public void EnableAbilities()
    {
        if(movement !=null)
        movement.enabled = true;
    }
    public void Die()
    {
        OnDeath.Invoke();
        movementAb = null;
        attack = null;
        defense = null;
    }
}
