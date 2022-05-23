using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public Ability ability;
    EnemyMovement eMovement;
    EnemyShooting eShooting;
    // Start is called before the first frame update
    void Start()
    {
        eMovement = GetComponent<EnemyMovement>();
        eShooting = GetComponent<EnemyShooting>();
    }

    // Update is called once per frame
    void Update()
    {
        if(eMovement != null)
        {
            if (eMovement.state == MovementState.reached)
            {
                if(eShooting != null)
                {
                    eShooting.isReady = true;
                }
            }
            else
            {
                if (eShooting != null)
                {
                    eShooting.isReady = false;
                }
            }

        }
    }
}
