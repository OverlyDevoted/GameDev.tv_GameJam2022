using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class SingleShotAbility : Ability
{
    public GameObject bulletPrefab;

    public override void Activate(GameObject caller)
    {
        if (isEnabled)
        {
            GameObject bullet = Instantiate(bulletPrefab, caller.transform.position, Quaternion.identity);
            Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0f;
            bullet.transform.up = newPos - bullet.transform.position;
            state = AbilityState.active;
            currentActiveTime = Time.time + activeTime;
            if(clip!=null)
            AudioManager.PlayClip(clip);
        }
    }

}
