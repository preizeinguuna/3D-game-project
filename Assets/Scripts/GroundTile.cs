using UnityEngine;

public class GroundTile : MonoBehaviour
{

    GroundSpawner groundSpawner;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] GameObject obstaclePrebaf;
    [SerializeField] GameObject tallbstaclePrebaf;
    [SerializeField] float tallObstacleChance = 0.2f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private  void Start()
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
        GameObject obstacleToSpawn = obstaclePrebaf;
        float random = Random.Range(0f, 1f);
        if (random < tallObstacleChance)
        {
            obstacleToSpawn = tallbstaclePrebaf;
        }

        int obsctacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obsctacleSpawnIndex).transform;

        Instantiate(obstacleToSpawn, spawnPoint.position, Quaternion.identity, transform);
    }



    public void SpawnCoin()
    {
        int coinToSpawan = 10;
        for (int i = 0; i < coinToSpawan; i++)
        {
            GameObject temp = Instantiate(coinPrefab,transform);
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
    if (point != collider.ClosestPoint(point))
        {
            point = GetRandomPointInCollider(collider);
        }
        point.y =1;
        return point;

    } 

}
