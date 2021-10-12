using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    #endregion

    public Player player;
    public bool isGameOver = false;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private float delayToRestart;
    private float currentSpeed;
    private float timerRestart;

    void Awake()
    {
        _instance = this;

    }

    void Start()
    {
        ResetLevel();
    }

    private void Update()
    {
        if (isGameOver)
        {
            timerRestart += Time.deltaTime;
            if (timerRestart < delayToRestart)
                return;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                ResetLevel();
            
        }
    }

    public void GameOver()
    {
        isGameOver = true;
        player.gameObject.SetActive(false);
        gameOverUI.SetActive(true);
        //print("Game Over");
    }

    public void ResetLevel()
    {
        isGameOver = false;
        gameOverUI.SetActive(false);
        player.gameObject.SetActive(true);
        ScoreManager.Instance.Score = 0;
        timerRestart = 0f;
    }

    public float CurrentSpeed { 
        get { return currentSpeed; } 
        set { currentSpeed = value; }
    }
}
