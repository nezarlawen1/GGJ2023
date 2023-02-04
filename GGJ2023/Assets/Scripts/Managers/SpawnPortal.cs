using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPortal : MonoBehaviour
{
    public GameObject PortalPrefab;
    public float TimeBetweenSpawns = 10f;
    [SerializeField] private Vector3 _spawnSectorRange = new Vector3(70, 1, 25);
    private void Start()
    {
        StartCoroutine(RespawnDelay());
    }

    private void SpawnInRandomPos(Vector3 respawnPos)
    {
        GameObject newPortal = Instantiate(PortalPrefab, respawnPos, Quaternion.identity);
        newPortal.transform.LookAt(Vector3.zero);
    }

    IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(Random.Range(TimeBetweenSpawns, TimeBetweenSpawns + 5));
        Vector3 respawnPos = new Vector3(Random.Range(-_spawnSectorRange.x, _spawnSectorRange.x),
            Random.Range(-_spawnSectorRange.y, _spawnSectorRange.y), Random.Range(-_spawnSectorRange.z, _spawnSectorRange.z));
        SpawnInRandomPos(respawnPos+transform.position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawWireCube(transform.position, _spawnSectorRange * 2);

    }
}
