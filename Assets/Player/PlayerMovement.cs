using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovement
{
    public void ChargeTowards(Vector3 from, Vector3 to, float procentage)
    {
        transform.position = Vector3.Lerp(from, to, procentage);
    }

    public Vector2 GetDirection()
    {
        return (Input.GetAxisRaw("Horizontal") * Vector2.right + Input.GetAxisRaw("Vertical") * Vector2.up).normalized;
    }

    public Vector2 GetDirection(Vector2 towards)
    {
        throw new System.NotImplementedException();
    }

    public void MoveTowards(Vector2 direction, float speed)
    {

        KeepInScreen();
        transform.Translate(direction*speed*Time.deltaTime);
        
    }

    public void KeepInScreen()
    {
        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
    
    public void PlayStep(AudioClip clip)
    {
        AudioManager.PlayClip(clip);
    }
}
