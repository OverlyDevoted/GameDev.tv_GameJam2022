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
    public Animator attackCurtain;

    public RawImage defenceAbility;
    public Animator defenceCurtain;
    
    public Texture empty;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highscoreText;
    public TextMeshProUGUI highscoreFlavor;
    public TextMeshProUGUI deathScoreText;
    float currentHighscore = 0;
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
    public void SetDeathScores(float score, float highscore)
    {
        if (currentHighscore < highscore)
        {
            highscoreFlavor.text = "New Highscore!";
            currentHighscore = highscore;
        }
        else
            highscoreFlavor.text = "Highscore";
        highscoreText.text = highscore.ToString();
        deathScoreText.text = score.ToString();
    }

    public void SetScoreText(float score)
    {
        scoreText.text = score.ToString();
    }
    IEnumerator PlayCurtain(Animator curtain, float cooldown, float active)
    {
        curtain.Play("Drop");
        curtain.SetFloat("Speed", 0);
        yield return new WaitForSeconds(active);
        curtain.SetFloat("Speed", 1/cooldown);
    }
    public void PlayAttackAnimation(float cooldown, float active)
    {
        StartCoroutine(PlayCurtain(attackCurtain, cooldown, active));
    }
    public void PlayDefenceAnimation(float cooldown, float active)
    {
        StartCoroutine(PlayCurtain(defenceCurtain, cooldown, active));
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
                break;
            case Ability.AbilityType.defence:
                defenceAbility.texture = icon;
                break;
        }

    }
}
