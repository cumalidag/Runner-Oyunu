using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    // Coinlerin spawnlanmasýný ve coinler için object pooling yapýsýný saðlar.
    [SerializeField] private float coinSpawnTime = 0.75f;
    [SerializeField] private float coinSpawnDistance = 30f;
    [SerializeField] private float coinSpawnHeight = 1f;
    private float[] coinPositions = { -2.5f, 0, 2.5f };
    public GameObject coinPrefab;
    public PlayerCollision playerCollision;
    public Transform playerTransform;
    public List<GameObject> coinPool = new List<GameObject>();
    public int coinPoolSize = 20;
    public int currentCoinIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        coinPool.Capacity = coinPoolSize;
        for (int i = 0; i < coinPoolSize; i++)
        {
            GameObject coin = Instantiate(coinPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            coin.SetActive(false);
            coinPool.Add(coin);
        }
        StartCoroutine(SpawnCoins());
    }


    private IEnumerator SpawnCoins()
    {
        while (!playerCollision.IsDead)
        {
            yield return new WaitForSeconds(coinSpawnTime);
            int randomIndex = Random.Range(0, coinPositions.Length);
            coinPool[currentCoinIndex].transform.position = new Vector3(coinPositions[randomIndex], coinSpawnHeight, playerTransform.position.z + coinSpawnDistance);
            coinPool[currentCoinIndex].SetActive(true);
            currentCoinIndex++;
            if (currentCoinIndex >= coinPoolSize)
            {
                currentCoinIndex = 0;
            }
        }
    }

}
