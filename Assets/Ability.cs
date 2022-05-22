using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public string name;
    public float cooldown;
    public float activeTime;
    public enum AbilityState
    {
        ready,
        active,
        cooldown,
        disabled
    }
    AbilityState state;
    public virtual void Activate(GameObject caller) { }
}
