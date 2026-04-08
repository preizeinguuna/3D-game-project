using UnityEngine;

public class GroundTile : MonoBehaviour
{

    GroundSpawner groundSpawner;
 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private  void Start()
    {
        groundSpawner = FindFirstObjectByType<GroundSpawner>();
        SpawnObstacle();
        SpawnCoin();
    }


    private void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public GameObject obstaclePrebaf;

   void SpawnObstacle()
    {
        int obsctacleSpawnIndex = Random.Range(2, 5);
        Transform spawnPoint = transform.GetChild(obsctacleSpawnIndex).transform;

        Instantiate(obstaclePrebaf, spawnPoint.position, Quaternion.identity, transform);
    }


    public GameObject coinPrefab;

    void SpawnCoin()
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
