using UnityEngine;

namespace EclipseProtocol
{
    public class PickupSpawner : MonoBehaviour
    {
        public GameObject[] pickupPrefabs;
        public Transform[] spawnPoints;
        public float spawnInterval = 10f;

        void Start()
        {
            InvokeRepeating("SpawnPickup", 1f, spawnInterval);
        }

        void SpawnPickup()
        {
            if (pickupPrefabs.Length == 0 || spawnPoints.Length == 0) return;

            int prefabIndex = Random.Range(0, pickupPrefabs.Length);
            int spawnIndex = Random.Range(0, spawnPoints.Length);

            Instantiate(pickupPrefabs[prefabIndex], spawnPoints[spawnIndex].position, Quaternion.identity);
        }
    }
}
