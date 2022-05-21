using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public Movement movement;
    public Transform spawnPosition;
    public UnityEvent OnKilled;
    public UnityEvent OnReincarnate;
    public UnityEvent OnDeath;

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
        ResetPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Kill"))
        {
            OnKilled.Invoke();
            Debug.Log("Death");
            movement.enabled = false;
        }
    }
    public void ResetPlayer()
    {
        transform.position = spawnPosition.position;
        movement.enabled = false;
    }
    public void Reincarnate()
    {
        OnReincarnate.Invoke();
    }

    public void Die()
    {
        OnDeath.Invoke();
    }
}
