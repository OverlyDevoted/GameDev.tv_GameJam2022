using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public List<UIelement> UIelements;
    // Start is called before the first frame update
    void Start()
    {
        foreach(UIelement element in UIelements)
        {
            foreach(UIelement elementTo in UIelements)
            {
                if (element != elementTo)
                    element.OnEnable.AddListener(elementTo.DisableElement);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
