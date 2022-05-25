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

    public RawImage baseAbility;
    
    public RawImage attackAbility;
    float attackCooldown;
    float attackActive;
    float attackCurrentCool;
    float attackCurrentActive;
    public Animator attackCurtain;

    public RawImage defenceAbility;
    float defenceCooldown;
    float defenceActive;
    float defenceCurrentCool;
    float defenceCurrentActive;
    public Animator defenceCurtain;
    
    public Texture empty;

    // Start is called before the first frame update
    void Start()
    {
        foreach(UIelement element in UIelements)
        {
            Debug.Log(element.name);
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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (attackAbility != null)
            {
                
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if (defenceAbility != null)
            { 
            
            }
        }

    }
    public void SetupCurtain(RectTransform curtain)
    {

    }
    public void AnimateCooldown(float cooldown, float active, float currentCool, float currentActive, RawImage curtain)
    {
        
    }
    public void SetKillUI(Ability current, Ability acquired, string killer)
    {
        
        currentAbilityName.text = "Current ability " + current.name;
        currentAbilityDescription.text = current.description;
        currentAbilityIcon.texture = current.icon;
        
        if(current.name == "empty")
        acquiredAbilityName.text = "You will aqcuire " + acquired.name + " ability";
        else if(current == acquired)
            acquiredAbilityName.text = "You already own " + acquired.name + " ability"; 
        else
            acquiredAbilityName.text = "Your current ability will be replaced by " + acquired.name + " ability";

        acquiredAbilityDescription.text=acquired.description;
        acquiredAbilityIcon.texture = acquired.icon;

        killerText.text = "You got killed by " + killer;
    }
    public void ResetAbilityUI()
    {
        baseAbility.texture = empty;
        attackAbility.texture = empty;
        defenceAbility.texture = empty;
    }
    public void SetAbility(Texture icon, Ability.AbilityType type, float cooldown, float active)
    {
        switch (type)
        {
            case Ability.AbilityType.based:
                baseAbility.texture = icon;
                break;
            case Ability.AbilityType.attack:
                attackAbility.texture = icon;
                attackCooldown = cooldown;
                attackActive = active;
                break;
            case Ability.AbilityType.defence:
                defenceAbility.texture = icon;
                defenceCooldown = cooldown;
                defenceActive = active;
                break;
        }

    }
}
