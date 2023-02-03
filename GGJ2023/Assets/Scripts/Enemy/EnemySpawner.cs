using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private int Amount = 5;
    [SerializeField] private float cooldown = 3f;
    public List<GameObject> Spawns = new List<GameObject>();
    public bool FinishedSpawning = false;

    private float timer = 0;
    private int counter = 0;

    // Update is called once per frame
    void Update()
    {
        if (counter < Amount)
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
        else
        {
            for (int i = 0; i < Spawns.Count; i++)
            {
                if (Spawns[i] == null)
                {
                    Spawns.Remove(Spawns[i]);
                }
            }

            if (Spawns.Count == 0)
            {
                FinishedSpawning = true;
            }
        }
    }

    private void SpawnEnemy()
    {
        GameObject enemyObj = Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
        timer = 0;
        counter++;
        Spawns.Add(enemyObj);
    }
}
