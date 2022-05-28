using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnMain;
    public UnityEvent OnGameplay;
    public UnityEvent OnDeath;

    public SpawnManager spawnManager;
    public PlayerManager playerManager;
    public UIManager uiManager;

    public GameState gameState;

    private float highscore = 0;
    private float score;
    
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(7, 7);
        OnMain.Invoke();
        playerManager = FindObjectOfType<PlayerManager>();
        uiManager = FindObjectOfType<UIManager>();
        playerManager.OnAttack.AddListener(uiManager.PlayAttackAnimation);
        playerManager.OnDefence.AddListener(uiManager.PlayDefenceAnimation);
        playerManager.OnKilled.AddListener((Ability current, Ability acquired, string killer) =>
        {
            if (score > highscore)
                highscore = score;
            uiManager.SetDeathScores((int)score,(int)highscore);
            gameState = GameState.Death;
            OnDeath.Invoke();
            uiManager.SetKillUI(current, acquired, killer);
        }
        );
        playerManager.OnDeath.AddListener(uiManager.ResetAbilityUI);
        playerManager.OnReincarnate.AddListener((Ability ability) => { uiManager.SetAbility(ability.icon, ability.type, ability.cooldown, ability.activeTime); });
        OnMain.AddListener(() =>
        {
            score = 0;
            DestroyObjectsWithTag("Kill");
            DestroyObjectsWithTag("Node");
            DestroyObjectsWithTag("Enemy");
            DestroyObjectsWithTag("KillBullet");
            playerManager.ResetPlayer();
            gameState = GameState.Main;
        });

        OnGameplay.AddListener(() =>
        {
            gameState = GameState.Gameplay;
            spawnManager = FindObjectOfType<SpawnManager>();
        });
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case GameState.Main:
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    OnGameplay?.Invoke();
                }
                break;
            case GameState.Death:
                if(Input.GetKeyDown(KeyCode.LeftControl))
                {
                    OnMain?.Invoke();
                    playerManager.Die();
                    Debug.Log("Die");
                }
                if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    OnMain?.Invoke();
                    playerManager.Reincarnate();
                    Debug.Log("Reincarnate");
                }
                break;
            case GameState.Gameplay:
                score += Time.deltaTime;
                uiManager.SetScoreText((int)score);
                break;
        }
    }
    private void DestroyObjectsWithTag(string tag)
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(tag);
        if (objects == null)
            return;
        foreach (GameObject toDestroy in objects)
            Destroy(toDestroy);
    }
}
public enum GameState
{
    Main,
    Gameplay,
    Death
}