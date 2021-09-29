using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float minSpawnTime;
    [SerializeField] private GameObject prefObstacle;
    [SerializeField] private Transform spawnPosition;


    void Start()
    {
        //yield return new WaitForSecondsRealtime(1f);
        print("Primer Spawn");

        StartCoroutine(Spawn());
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private IEnumerator Spawn()
    {
        print("Spawn");
        Instantiate(prefObstacle, spawnPosition);
        yield return new WaitForSecondsRealtime(1f);
        Spawn();
    }
}
