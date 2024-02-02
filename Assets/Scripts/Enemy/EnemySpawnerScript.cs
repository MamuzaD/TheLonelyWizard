using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Vector2 spawnArea;
    [SerializeField] private float minSpawnTime;
    [SerializeField] private float maxSpawnTime;

    private float spawnTime;
    private Transform player;
    // Start is called before the first frame update
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        SetSpawnTime();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyPosition = new Vector3(Random.Range(-spawnArea.x, spawnArea.x), Random.Range(-spawnArea.y, spawnArea.y), 0);
        enemyPosition += player.position;
        spawnTime -= Time.deltaTime;
        if (spawnTime <= 0) 
        { 
            Instantiate(enemyPrefab, enemyPosition, Quaternion.identity);
            SetSpawnTime();
        }
    }

    private void SetSpawnTime()
    {
        spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
    }
}
