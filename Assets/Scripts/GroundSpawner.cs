using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] GameObject groundTile;
    Vector3 nextSpawnPoint;

    public void SpawnTile(bool spawnItems)
    {
        GameObject temp = Instantiate(groundTile, nextSpawnPoint, Quaternion.identity);
        // NextSpawnPoint ir b?rns ar indeksu 1
        nextSpawnPoint = temp.transform.GetChild(1).transform.position;

        if (spawnItems)
        {
            GroundTile tileScript = temp.GetComponent<GroundTile>();
            tileScript.SpawnObstacle();
            tileScript.SpawnCoin();
        }
    }

    private void Start()
    {
        for (int i = 0; i < 15; i++)
        {
            // Tikai pati pirm? fl?ze b?s tukša (i < 1)
            if (i < 1)
            {
                SpawnTile(false);
            }
            else
            {
                SpawnTile(true);
            }
        }
    }
}