using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class ThreeHitterAbility : Ability
{
    public int healthBonus = 2;
    
    public override void Activate(GameObject caller)
    {
        caller.GetComponent<PlayerManager>().BonusHealth(healthBonus);
    }
}
