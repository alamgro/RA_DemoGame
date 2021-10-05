using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region SINGLETON
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }
    #endregion

    void Awake()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        //print("Game Over");
    }
}
