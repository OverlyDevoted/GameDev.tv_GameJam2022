using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class KillEvent : UnityEvent<Ability, Ability, GameObject>
{
}
public class PlayerManager : MonoBehaviour
{
    public Transform spawnPosition;
    public KillEvent OnKilled;
    public UnityEvent OnReincarnate;
    public UnityEvent OnDeath;
    private Movement movement;
    bool isDead = false;

    //i could try to store it in a list smh to make it more dynamic
    public Ability movementAb;
    float currentMovement;
    public Ability attack;
    float currentAttack;
    public KeyCode attackKey;
    public Ability defence;
    float currentDefence;

    Ability acquiredAbility;
    // Start is called before the first frame update
    void Start()
    {
        ResetPlayer();
    }
    private void Update()
    {

        //maybe trying to swap functionalities with delegations
        if(attack != null )
        {
            if(attack.state == Ability.AbilityState.ready && Input.GetKeyDown(attackKey))
                attack.Activate(gameObject);

            if (attack.state == Ability.AbilityState.active)
                attack.Linger(gameObject);

            if (attack.state == Ability.AbilityState.cooldown)
                attack.Cooldown(gameObject);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if((collision.gameObject.CompareTag("Kill") || collision.gameObject.CompareTag("KillBullet")) && !isDead)
        {
            Debug.Log("Death");
            acquiredAbility = collision.GetComponent<EnemyManager>().ability;
            OnKilled.Invoke(acquiredAbility,acquiredAbility,collision.gameObject);
            isDead = true;
            /*Ability currentAbility;
            
                        bool fillAbilitySlot = false;
            switch (acquiredAbility.type)
            {
                case Ability.AbilityType.based:
                    if (movement != null)
                    {
                        fillAbilitySlot = true;
                        currentAbility = movementAb;
                    }
                    break;
                case Ability.AbilityType.attack:
                    if 
                    (attack != null) {
                        fillAbilitySlot = true;
                        currentAbility = attack;
                    }
                    break;
                case Ability.AbilityType.defence:
                    if
                    (defence != null)
                    {
                        fillAbilitySlot = true;
                        currentAbility = defence;
                    }
                    break;
            }

            acquiredAbility = collision.GetComponent<EnemyManager>().ability;
            
            if(fillAbilitySlot)
            OnKilled?.Invoke(currentAbility, acquiredAbility,collision.gameObject);
            else
                OnKilled?.Invoke(empty, acquiredAbility, collision.gameObject);
            */

            DisableAbilities();
        }
    }

    public void ResetPlayer()
    {
        transform.position = spawnPosition.position;
        
    }
    public void Reincarnate()
    {

        OnReincarnate.Invoke();
        switch (acquiredAbility.type) 
        {
            case Ability.AbilityType.based:
                if(movement == null)
                {
                    movementAb = acquiredAbility;
                    movementAb.Activate(this.gameObject);
                    movement = GetComponent<Movement>();
                    movement.enabled = false;
                }

                break;
            case Ability.AbilityType.attack:
                attack = acquiredAbility;
                break;
            case Ability.AbilityType.defence:
                defence = acquiredAbility;
                break;
        }


    }
    public void DisableAbilities()
    {
        if(movement != null)
        movement.enabled = false;

        if (attack != null)
            attack.isEnabled = false;
    }
    public void EnableAbilities()
    {
        isDead = false;
        if(movement !=null)
        movement.enabled = true;

        if(attack != null)
        attack.isEnabled = true;
    }
    public void Die()
    {
        OnDeath.Invoke();
        if(movement != null)
            Destroy(movement);
        movementAb = null;
        attack = null;
        defence = null;
    }
}
