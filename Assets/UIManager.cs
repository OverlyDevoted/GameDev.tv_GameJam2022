using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public List<UIelement> UIelements;

    public TextMeshProUGUI killerText;
    
    public TextMeshProUGUI currentAbilityName;
    public TextMeshProUGUI currentAbilityDescription;
    public RawImage currentAbilityIcon;
    public TextMeshProUGUI acquiredAbilityName;
    public TextMeshProUGUI acquiredAbilityDescription;
    public RawImage acquiredAbilityIcon;

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
    public void SetKillUI(Ability current, Ability acquired, GameObject killer)
    {
        if (current.name == "")
        {
            currentAbilityName.text = "You have no ability of type " + acquired.type.ToString();
            currentAbilityDescription.text = "";
            currentAbilityIcon.texture = null;
        }
        else
        {
            currentAbilityName.text = "Current ability " + current.name;
            currentAbilityDescription.text = current.description;
            currentAbilityIcon.texture = current.icon;
        }

        acquiredAbilityName.text = "Current ability will be replaced by " + acquired.name;
        acquiredAbilityDescription.text=acquired.description;
        acquiredAbilityIcon.texture = acquired.icon;

        killerText.text = "You got killed by " + killer.name;
    }
}
