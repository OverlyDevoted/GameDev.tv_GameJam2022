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

    bool isDead = false;
    bool isInvincible = false;
    float currentInvincible;
    public int baseHealth = 1;
    int health = 1;

    private Movement movement;

    //i could try to store it in a list smh to make it more dynamic
    public Ability baseMovement;
    
    public Ability attack;
    public KeyCode attackKey;
    
    public Ability defence;
    public KeyCode defenceKey;

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

        if(defence != null)
        {
            if (defence.state == Ability.AbilityState.ready && Input.GetKeyDown(defenceKey))
                defence.Activate(gameObject);

            if (defence.state == Ability.AbilityState.active)
                defence.Linger(gameObject);

            if (defence.state == Ability.AbilityState.cooldown)
                defence.Cooldown(gameObject);
        }


        if(isInvincible && currentInvincible < Time.time)
        {   
            Debug.Log("Not Invincible");
            isInvincible = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if((collision.gameObject.CompareTag("Kill") || collision.gameObject.CompareTag("KillBullet")) && !isDead && !isInvincible)
        {
            health--;
            
            if(health == 0)
            {
                Debug.Log("Death");
                acquiredAbility = collision.GetComponent<EnemyManager>().ability;
                OnKilled.Invoke(acquiredAbility, acquiredAbility, collision.gameObject);
                isDead = true;


                DisableAbilities();
                Destroy(collision.gameObject);
            }
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
                    baseMovement = acquiredAbility;
                    baseMovement.Activate(this.gameObject);
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
        health = baseHealth;
        isDead = false;
        if (movement != null)
        {
            movement.enabled = true;

        }

        if (attack != null)
        {
            attack.isEnabled = true;

            if (attack.state == Ability.AbilityState.passive)
                attack.Activate(gameObject);
        }

        if(defence != null)
        {
            defence.isEnabled = true;
            if (defence.state == Ability.AbilityState.passive)
                defence.Activate(gameObject);
        }
    }
    public void BonusHealth(int bonus)
    {
        health += bonus;
    }
    public void Die()
    {
        OnDeath.Invoke();
        if(movement != null)
            Destroy(movement);
        baseMovement = null;
        attack = null;
        defence = null;
    }
    public void SetInvincible(float invincibilityFrames)
    {
        Debug.Log("Invincible");
        isInvincible = true;
        currentInvincible = Time.time + invincibilityFrames;
    }
}
