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
        // P?rbauda, vai tas, kas izg?ja cauri, ir sp?l?t?js
        if (other.CompareTag("Player"))
        {
            groundSpawner.SpawnTile(true);
            Destroy(gameObject, 2);
        }
    }

    public void SpawnObstacle()
    {
        // Š??rš?u punkti parasti ir b?rna objekti (Child) ar indeksiem 2, 3, 4
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

            // Piespiežam š??rsli b?t tieši uz zemes
            Vector3 pos = obstacle.transform.position;
            pos.y = 0f;
            obstacle.transform.position = pos;
        }
    }

    public void SpawnCoin()
    {
        if (coinsToSpawn <= 0) return;

        Collider tileCollider = GetComponent<Collider>();
        // Izvietojam mon?tas ar liel?k?m atstarp?m no mal?m, lai t?s nesaliptu
        float minZ = tileCollider.bounds.min.z + 3f;
        float maxZ = tileCollider.bounds.max.z - 3f;

        // Joslas: -2 (kreis?), 0 (vidus), 2 (lab?)
        float[] lanes = { -2f, 0f, 2f };

        // S?kam nejauš? josl?
        int currentLaneIndex = Random.Range(0, lanes.Length);

        for (int i = 0; i < coinsToSpawn; i++)
        {
            GameObject temp = Instantiate(coinPrefab, transform);

            // Vienm?r?gs att?lums starp mon?t?m uz vienas fl?zes
            float t = (float)i / (coinsToSpawn - 1);
            float zPos = Mathf.Lerp(minZ, maxZ, t);

            // ZIG-ZAG LO?IKA:
            // Ja esam vid? (index 1), ejam uz s?nu. 
            // Ja esam s?n?, ejam uz vidu.
            if (currentLaneIndex == 1)
            {
                currentLaneIndex = (Random.Range(0, 2) == 0) ? 0 : 2;
            }
            else
            {
                currentLaneIndex = 1; // Atgriežamies vid?
            }

            temp.transform.position = new Vector3(lanes[currentLaneIndex], coinHeight, zPos);
        }
    }
}