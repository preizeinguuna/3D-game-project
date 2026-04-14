using UnityEngine;
using System.Collections.Generic;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;

    [Header("Prefabs")]
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject redBarrierPrefab;
    [SerializeField] GameObject greenBigDumpsterPrefab;

    [Header("Settings")]
    [SerializeField] float coinHeight = 1.0f;
    [SerializeField] int coinsToSpawn = 5;
    [Range(0, 1)][SerializeField] float dumpsterChance = 0.2f;

    private void Start()
    {
        groundSpawner = Object.FindFirstObjectByType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

    public void SpawnObstacle()
    {
        List<int> availableIndices = new List<int> { 2, 3, 4 };
        int obstaclesToSpawn = Random.Range(1, 3);

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            int listIndex = Random.Range(0, availableIndices.Count);
            int spawnIndex = availableIndices[listIndex];
            availableIndices.RemoveAt(listIndex);

            GameObject prefab = (Random.Range(0f, 1f) < dumpsterChance) ? greenBigDumpsterPrefab : redBarrierPrefab;
            Transform spawnPoint = transform.GetChild(spawnIndex);

            GameObject obstacle = Instantiate(prefab, spawnPoint.position, Quaternion.identity, transform);

            Vector3 pos = obstacle.transform.position;
            pos.y = 0f;
            obstacle.transform.position = pos;
        }
    }

    public void SpawnCoin()
    {
        Collider tileCollider = GetComponent<Collider>();

        float minZ = tileCollider.bounds.min.z + 5f;
        float maxZ = tileCollider.bounds.max.z - 1f;

        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);

            float randomX = Random.Range(-2.5f, 2.5f);
            float randomZ = Random.Range(minZ, maxZ);

            temp.transform.position = new Vector3(randomX, coinHeight, randomZ);
        }
    }
}