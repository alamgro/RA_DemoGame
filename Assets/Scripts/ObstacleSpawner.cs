using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnTime;
    [SerializeField] private GameObject prefObstacle;
    [SerializeField] private Transform spawnPosition;
    private float timer = 0f;


    void Start()
    {
        //yield return new WaitForSecondsRealtime(1f);
        print("Primer Spawn");
        GameObject go = Instantiate(prefObstacle, transform);
        go.transform.position = spawnPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= minSpawnTime)
        {
            GameObject go = Instantiate(prefObstacle, transform);
            go.transform.position = spawnPosition.position;
            timer = 0f;
        }

    }

}
