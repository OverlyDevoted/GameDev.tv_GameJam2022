using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ChargeAbility : Ability
{

    PlayerManager pm;
    PlayerMovement pv;
    Vector3 startPos;
    Vector3 endPos;
    public float dashDistance;
 
    public override void Activate(GameObject caller)
    {
        pv = caller.GetComponent<PlayerMovement>();
        Vector2 direction = pv.GetDirection();
        if(direction.magnitude == 0)
            return;

        pm = caller.GetComponent<PlayerManager>();
        
        pm.SetInvincible(activeTime);
        state = AbilityState.active;
        currentActiveTime = Time.time + activeTime;
        startPos = caller.transform.position;
        endPos = caller.transform.position + (new Vector3(direction.x,direction.y) * dashDistance);
    }
    public override void Linger(GameObject caller)
    {
        if (isEnabled)
        {
            //pv.ChargeTowards()
            if (currentActiveTime <= Time.time)
            {
                currentCooldown = Time.time + cooldown;
                state = AbilityState.cooldown;
            }

            pv.ChargeTowards(endPos, startPos, (currentActiveTime - Time.time) / activeTime);
        }
    }
}
