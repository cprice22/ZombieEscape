using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Prefab of the zombie to spawn

    private float laneDistance = 3f; // Distance between lanes

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SpawnZombie();
    }

    void SpawnZombie()
    {
        int lane = Random.Range(0, 3); // Randomly select a lane (0, 1, or 2)

        float xPos = (lane - 1) * laneDistance; // Calculate x position based on lane

        Vector3 spawnPos = new Vector3(xPos, 1f, transform.position.z + Random.Range(2f, 8f)); // Spawn position with random z offset

        Instantiate(zombiePrefab, spawnPos, Quaternion.identity); // Spawn the zombie at the calculated position
    }
}
