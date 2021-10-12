using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Spawn parameters")]
    [SerializeField] private float minSpawnTime = 0f;
    [SerializeField] private float maxSpawnTime = 0f;
    [Header("Speed parameters")]
    [SerializeField] private float maxObstacleSpeed = 0f;
    [SerializeField] private float initObstacleSpeed = 0f;
    [Header("Other config")]
    [SerializeField] private GameObject[] pfbObstacles = null; //Prefabs of the obstacles
    [SerializeField] private Transform spawnPosition = null;
    private List<GameObject> poolObstacles;
    private float timer = 0f;

    private void Start()
    {
        poolObstacles = new List<GameObject>();
        GameManager.Instance.CurrentSpeed = initObstacleSpeed;
        timer = Random.Range(minSpawnTime, maxSpawnTime);
    }

    void Update()
    {
        //Turn off obstacles if the game is over
        if (GameManager.Instance.isGameOver)
        {
            timer = Random.Range(minSpawnTime, maxSpawnTime); //Restart timer
            GameManager.Instance.CurrentSpeed = initObstacleSpeed;
            for (int i = 0; i < poolObstacles.Count; i++)
            {
                poolObstacles[i].SetActive(false);
            }
            return;
        }

        timer -= Time.deltaTime;
        //Obstacle generation
        if(timer <= 0f )
        {
            GameObject obstacle = GenerateObstacle();
            obstacle.transform.position = spawnPosition.position;
            obstacle.GetComponent<Obstacle>().Speed = GameManager.Instance.CurrentSpeed * transform.parent.transform.localScale.x;
            timer = Random.Range(minSpawnTime, maxSpawnTime); //Restart timer
        }

        //If the max speed has not been reached, then increase the current obstacle speed
        if(GameManager.Instance.CurrentSpeed <= maxObstacleSpeed)
        {
            GameManager.Instance.CurrentSpeed += 0.2f * Time.deltaTime;
        }
        //print(currentSpeed);
    }

    private GameObject GenerateObstacle()
    {
        for (int i = 0; i < poolObstacles.Count; i++)
        {
            if (!poolObstacles[i].activeSelf)
            {
                poolObstacles[i].SetActive(true);
                poolObstacles[i].transform.position = transform.position;
                poolObstacles[i].transform.rotation = transform.rotation;
                return poolObstacles[i];
            }
        }
        int randIndex = Random.Range(0, pfbObstacles.Length);
        poolObstacles.Add(Instantiate(pfbObstacles[randIndex], transform.parent.transform)); //Instanciate obstacle
        return poolObstacles[poolObstacles.Count - 1];
    }

}
