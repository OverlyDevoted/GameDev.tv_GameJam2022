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

    public GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        OnMain.Invoke();
        playerManager = FindObjectOfType<PlayerManager>();
        
        playerManager.OnKilled.AddListener(() =>
        {
            gameState = GameState.Death;
            OnDeath.Invoke();
        }
        );

        OnMain.AddListener(() =>
        {
            DestroyObjectsWithTag("Kill");
            DestroyObjectsWithTag("Node");
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
                    Debug.Log("Die");
                }
                if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    OnMain?.Invoke();
                    Debug.Log("Reincarnate");
                }
                break;
            case GameState.Gameplay:
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