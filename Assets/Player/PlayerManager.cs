using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class KillEvent : UnityEvent<Ability, Ability, string> { }
[System.Serializable]
public class ReincarnateEvent : UnityEvent<Ability> { }

public class PlayerManager : MonoBehaviour
{
    public Transform spawnPosition;
    public SpriteRenderer model;
    public Color startColor;
    public Color hitColor;

    public KillEvent OnKilled;
    public ReincarnateEvent OnReincarnate;
    public UnityEvent OnDeath;
    public AbilityEvent OnAttack;
    public AbilityEvent OnDefence;

    bool isDead = false;
    bool isInvincible = false;
    float currentInvincible;
    public int baseHealth = 1;
    int health = 1;
    public float invincibilityAfterHit = 0.2f;
    private Movement movement;

    Animator animator;
    //this class should not handle this much ability logic
    //i could try to store it in a list smh to make it more dynamic
    public Ability baseMovement;
    
    public Ability attack;
    public KeyCode attackKey;
    
    public Ability defence;
    public KeyCode defenceKey;

    Ability acquiredAbility;
    public Ability empty;

    public AudioClip deathClip;
    public AudioClip gameStartClip;

    public AudioClip playerHit;
    public AudioClip absorb;
    public AudioClip loseAb;
    public MilkShake.ShakePreset shake;
    public ParticleSystem hitParticles;
    // Start is called before the first frame update
    void Start()
    {
        ResetPlayer();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {

        //maybe trying to swap functionalities with delegations
        if(attack != null )
        {
            if(attack.state == Ability.AbilityState.ready && Input.GetKeyDown(attackKey))
            {
                OnAttack.Invoke(attack.cooldown,attack.activeTime);
                attack.Activate(gameObject);
                animator.SetTrigger("Shoot");
            }

            if (attack.state == Ability.AbilityState.active)
                attack.Linger(gameObject);

            if (attack.state == Ability.AbilityState.cooldown)
                attack.Cooldown(gameObject);
        }

        if(defence != null)
        {
            if (defence.state == Ability.AbilityState.ready && Input.GetKeyDown(defenceKey))
            {
                OnDefence.Invoke(defence.cooldown, defence.activeTime);
                defence.Activate(gameObject);
                animator.SetTrigger("Shoot");
            }

            if (defence.state == Ability.AbilityState.active)
                defence.Linger(gameObject);

            if (defence.state == Ability.AbilityState.cooldown)
                defence.Cooldown(gameObject);
        }
        if(movement != null)
        {

            if (movement.state == MovementStateInner.idle)
                animator.SetBool("isWalking", false);
            else
            {
                animator.SetBool("isWalking", true);
            }
        }
        else
            animator.SetBool("isWalking", false);
        if (isInvincible && currentInvincible < Time.time)
        {   
            model.color = startColor;
            isInvincible = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if((collision.gameObject.CompareTag("Kill") || collision.gameObject.CompareTag("KillBullet")) && !isDead && !isInvincible)
        {
            health--;
            Instantiate(hitParticles, transform.position, Quaternion.identity);
            SetInvincible(invincibilityAfterHit);
            MilkShake.Shaker.ShakeAll(shake);
            AudioManager.PlayClip(playerHit);
            if(movement!=null)
            movement.state = MovementStateInner.idle;
            if (health == 0)
            {
                AudioManager.PlayClip(deathClip);
                acquiredAbility = collision.GetComponent<EnemyManager>().ability;
                Ability currentAbility = empty;
                switch (acquiredAbility.type)
                {
                    case Ability.AbilityType.based:
                        if (baseMovement != null)
                            currentAbility = baseMovement;
                        break;
                    case Ability.AbilityType.defence:
                        if(defence != null)
                            currentAbility = defence;
                        break;
                    case Ability.AbilityType.attack:
                        if(attack != null)
                            currentAbility = attack;
                        break;
                    case Ability.AbilityType.movement:
                        break;

                }
                
                OnKilled.Invoke(currentAbility, acquiredAbility, collision.gameObject.GetComponent<EnemyManager>().name);
                isDead = true;


                DisableAbilities();
                Destroy(collision.gameObject);
                return;
            }
        }
    }

    public void ResetPlayer()
    {
        transform.position = spawnPosition.position;
        model.color = startColor;
    }
    public void Reincarnate()
    {
        AudioManager.PlayClip(absorb);
        OnReincarnate.Invoke(acquiredAbility);
        switch (acquiredAbility.type) 
        {
            case Ability.AbilityType.based:
                if(movement == null)
                {
                    baseMovement = acquiredAbility;
                    baseMovement.Activate(this.gameObject);
                    movement = GetComponent<Movement>();
                    movement.state = MovementStateInner.idle;
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

        if(defence != null)
            defence.isEnabled = false;
    }
    public void EnableAbilities()
    {
        AudioManager.PlayClip(gameStartClip);
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
            else
                attack.state = Ability.AbilityState.ready;
        }

        if(defence != null)
        {
            defence.isEnabled = true;
            if (defence.state == Ability.AbilityState.passive)
                defence.Activate(gameObject);
            else
                defence.state = Ability.AbilityState.ready;
        }
    }
    public void BonusHealth(int bonus)
    {
        health += bonus;
    }
    public void Die()
    {
        AudioManager.PlayClip(loseAb);
        OnDeath.Invoke();
        if(movement != null)
            Destroy(movement);
        baseMovement = null;
        attack = null;
        defence = null;
    }
    public void SetInvincible(float invincibilityFrames)
    {
        isInvincible = true;
        currentInvincible = Time.time + invincibilityFrames;
        model.color = hitColor;
    }
}
