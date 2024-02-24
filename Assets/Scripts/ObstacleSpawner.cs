using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    // Objeler pooling yönetimi ile spawn edilir.
    [SerializeField] private float obstacleSpawnTime = 5f;
    [SerializeField] private float obstacleSpawnDistance = 80f;
    private float[] obstaclePositions = { -2.5f, 0, 2.5f };
    [SerializeField] private GameObject[] obstaclePrefabs;
    [SerializeField] private PlayerCollision playerCollision;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private List<GameObject> obstaclePool = new List<GameObject>();
    [SerializeField] private int obstaclePoolSize = 20;
    private int currentObstacleIndex = 0;


    // Start is called before the first frame update
    void Start()
    {
        obstaclePool.Capacity = obstaclePoolSize;
        for (int i = 0; i < obstaclePoolSize; i++)
        {
            int randomIndex = Random.Range(0, obstaclePositions.Length);
            GameObject obstacle = Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], Vector3.zero, Quaternion.Euler(0,180,0));
            obstacle.SetActive(false);
            obstaclePool.Add(obstacle);
        }
        StartCoroutine(SpawnObstacle());
    }


    private IEnumerator SpawnObstacle()
    {
        while (!playerCollision.IsDead)
        {
            yield return new WaitForSeconds(obstacleSpawnTime);
            int randomIndex = Random.Range(0, obstaclePositions.Length);

            // If the current obstacle is not active, update its position
            if (!obstaclePool[currentObstacleIndex].activeSelf)
            {
                obstaclePool[currentObstacleIndex].transform.position = new Vector3(0.0f, 0.0f, playerTransform.position.z + obstacleSpawnDistance);
            }

            // Activate the obstacle
            obstaclePool[currentObstacleIndex].SetActive(true);

            // Move to the next obstacle index
            currentObstacleIndex++;
            if (currentObstacleIndex >= obstaclePoolSize)
            {
                currentObstacleIndex = 0;
            }

        }
    }
}
