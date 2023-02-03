using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private int Amount = 5;
    [SerializeField] private float cooldown = 3f;
    private float timer = 0;
    private int counter = 0;

    // Update is called once per frame
    void Update()
    {
        if (counter <= Amount)
        {
            if (timer >= cooldown)
            {
                SpawnEnemy();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(EnemyPrefab,transform.position,Quaternion.identity);
        timer = 0;
        Amount++;
    }
}
