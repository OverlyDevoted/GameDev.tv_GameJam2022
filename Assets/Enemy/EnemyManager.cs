using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Ability ability;
    public string name;
    EnemyMovement eMovement;
    IEnemyAction eAction;
    Animator animator;
    public int health = 1;
    public SpriteRenderer model;
    public Color startColor;
    public Color hitColor;
    public ParticleSystem particleOnHit;
    public ParticleSystem particleDeath;
    public float hitTime = 0.1f;
    float currentHit;
    bool isHit;
    public AudioClip hitClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !gameObject.CompareTag("KillBullet"))
        {
            health--;
            Instantiate(particleOnHit, transform.position, Quaternion.identity);
            
            if (model != null)
                HitAnimations();
            if (health == 0)
            {
                Destroy(gameObject);
                Instantiate(particleDeath, transform.position, Quaternion.identity);
            }
            Destroy(collision.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHit = Time.time;
        model = GetComponentInChildren<SpriteRenderer>();
        if(model != null)
        model.color = startColor;
        eMovement = GetComponent<EnemyMovement>();
        eAction = GetComponent<IEnemyAction>();
    }
    public void HitAnimations()
    {
        isHit = true;
        currentHit = Time.time + hitTime;
        model.color = hitColor;

        eMovement.state = MovementState.stunned;
        AudioManager.PlayClip(hitClip);

    }
    // Update is called once per frame
    void Update()
    {
        if(eMovement != null)
        {
            if (eMovement.state == MovementState.reached)
            {
                if(eAction != null)
                {
                    eAction.Enable(true);
                }
            }
            else
            {
                if (eAction != null)
                {
                    eAction.Enable(false);
                }
            }

        }
        if(currentHit < Time.time && isHit)
        {
            isHit = false;
            model.color = startColor;
            eMovement.state = MovementState.following;
        }
    }
}
public interface IEnemyAction
{
    public void Action();
    public void Enable(bool enable);
}
