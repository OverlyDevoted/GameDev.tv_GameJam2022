using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Ability ability;
    public string name;
    EnemyMovement eMovement;
    IEnemyAction eAction;
    public int health = 1;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !gameObject.CompareTag("KillBullet"))
        {
            health--;
            if (health == 0)
            {
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        eMovement = GetComponent<EnemyMovement>();
        eAction = GetComponent<IEnemyAction>();
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
    }
}
public interface IEnemyAction
{
    public void Action();
    public void Enable(bool enable);
}
