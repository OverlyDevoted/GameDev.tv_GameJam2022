using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    Vector3 startPos;
    Vector3 endPos;
    Vector3 center;
    Vector3 startRelativePos;
    Vector3 endRelativePos;
    public float distance =1f;
    public float swingSpeed =1f;
    public float centerOffset = 1f;
    public float swingArcModifier = 0.1f;
    float currentSwing;
    Collider2D collider;
    TrailRenderer trailRenderer;
    public float selfDestruct = 0.3f;
    float currentSelfDestruct;
    int direction;
    // Start is called before the first frame update
    private void Awake()
    {
    }
    void Start()
    {
        collider = GetComponent<Collider2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.time = selfDestruct;
        trailRenderer.enabled = false;
        if (Mathf.RoundToInt(Random.Range(0f, 1f)) == 0)
            direction = 1;
        else
            direction = -1;
        startPos = transform.position + (transform.up * centerOffset)+ (direction*-transform.right * distance / 2);
        endPos = transform.position + (transform.up * centerOffset)+(direction*transform.right * distance/2);
        center = (startPos + endPos) * 0.5f;
        center += transform.up * -swingArcModifier;
        
        startRelativePos = startPos - center;
        endRelativePos = endPos - center;

        transform.position = startRelativePos;
        transform.position += center;
        trailRenderer.enabled = true;
        currentSwing = Time.time + swingSpeed;

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSwing < Time.time)
        {
            if(collider.enabled)
            {
                currentSelfDestruct = Time.time + selfDestruct;
                collider.enabled = false;
                //trailRenderer.enabled = false;
            }

            if (currentSelfDestruct < Time.time)
                Destroy(gameObject);
        }
        transform.position = Vector3.Slerp(startRelativePos, endRelativePos, 1-((currentSwing - Time.time)/swingSpeed));
        transform.position += center;
    }
}
