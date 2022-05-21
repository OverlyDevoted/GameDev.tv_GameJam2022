using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIelement : MonoBehaviour
{
    public UnityEvent OnEnable;
    public void EnableElement()
    {
        this.gameObject.SetActive(true);
        OnEnable.Invoke();
    }
    public void DisableElement()
    {
        this.gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
