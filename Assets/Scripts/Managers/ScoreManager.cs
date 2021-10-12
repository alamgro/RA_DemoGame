using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    #region SINGLETON
    private static ScoreManager _instance;
    public static ScoreManager Instance { get { return _instance; } }
    #endregion

    [SerializeField] private float scoreRate; //time (in seconds) to gain: 1 exp
    [SerializeField] private TextMeshProUGUI scoreUI;
    private int score;
    private float timer;

    private void Awake()
    {
        _instance = this;
        score = 0;
    }

    void Update()
    {
        if (GameManager.Instance.isGameOver)
            return;

        timer += Time.deltaTime;

        if (timer >= scoreRate)
        {
            timer = 0.0f; //Restart timer
            Score++;
        }
    }

    public int Score {
        get { return score; } 
        set { 
            score = value;
            scoreUI.text = "" + score.ToString("00000"); //Update timer on UI
        } 
    }
}
