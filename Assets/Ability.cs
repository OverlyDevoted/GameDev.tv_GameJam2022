using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string name;
    public string description;
    public float cooldown;
    protected float currentCooldown;
    public float activeTime;
    protected float currentActiveTime;
    public bool isEnabled = false;
    public Texture icon;
    public enum AbilityState
    {
        ready,
        active,
        cooldown,
        disabled
    }
    public enum AbilityType
    {
        based,
        attack,
        defence
    }
    public AbilityType type;
    public AbilityState state;
    public virtual void Activate(GameObject caller) { }
    public virtual void Linger(GameObject caller) 
    {
        if (isEnabled)
        {
            if (currentActiveTime < Time.time)
            {
                currentCooldown = Time.time + cooldown;
                state = AbilityState.cooldown;
            }
        }
    }
    public virtual void Cooldown(GameObject caller) 
    {
        if (isEnabled)
        {
            if (currentCooldown < Time.time)
            {
                state = AbilityState.ready;
            }
        }
    }
    public static void CopyTo(Ability from, Ability to)
    {
        to.name = from.name;
        to.description = from.description; 
        to.activeTime = from.activeTime;
        to.isEnabled = from.isEnabled;
        to.icon = from.icon;
        to.type = from.type;
        to.cooldown = from.cooldown;
    }
}
