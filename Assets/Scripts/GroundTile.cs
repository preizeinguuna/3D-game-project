using UnityEngine;
using System.Collections.Generic;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;

    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject redBarrierPrefab;
    [SerializeField] GameObject greenBigDumpsterPrefab;

    [SerializeField] float dumpsterChance = 0.2f;

    // Pievieno šo, lai var?tu Inspector log? pieregul?t augstumu (piem?ram, -0.5f vai 0.2f)
    [SerializeField] float obstacleHeightOffset = 0f;

    private void Start()
    {
        groundSpawner = FindFirstObjectByType<GroundSpawner>();
    }

    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile(true);
        Destroy(gameObject, 2);
    }

    public void SpawnObstacle()
    {
        // Izmantojam sarakstu, lai š??rš?i nekad nesp?notos viens otram virs?
        List<int> availableIndices = new List<int> { 2, 3, 4 };
        int obstaclesToSpawn = Random.Range(1, 3);

        for (int i = 0; i < obstaclesToSpawn; i++)
        {
            if (availableIndices.Count == 0) break;

            int listIndex = Random.Range(0, availableIndices.Count);
            int spawnIndex = availableIndices[listIndex];
            availableIndices.RemoveAt(listIndex);

            GameObject obstacleToSpawn = (Random.Range(0f, 1f) < dumpsterChance) ? greenBigDumpsterPrefab : redBarrierPrefab;

            if (obstacleToSpawn != null)
            {
                Transform spawnPoint = transform.GetChild(spawnIndex);

                // Izveidojam poz?ciju ar manu?lo augstuma korekciju
                Vector3 spawnPos = spawnPoint.position;
                spawnPos.y += obstacleHeightOffset;

                Instantiate(obstacleToSpawn, spawnPos, Quaternion.identity, transform);
            }
        }
    }

    public void SpawnCoin()
    {
        // Tavs esošais SpawnCoin kods...
        int coinsToSpawn = 5;
        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);
            temp.transform.position = GetRandomPointInCollider(GetComponent<Collider>());
        }
    }

    Vector3 GetRandomPointInCollider(Collider collider)
    {
        Vector3 point = new Vector3(
            Random.Range(collider.bounds.min.x, collider.bounds.max.x),
            Random.Range(collider.bounds.min.y, collider.bounds.max.y),
            Random.Range(collider.bounds.min.z, collider.bounds.max.z)
        );
        point.y = 1f; // Mon?t?m šis augstums ir OK, lai pele t?m var?tu izl?kt cauri
        return point;
    }
}