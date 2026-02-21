using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundPrefab; // Prefab for the ground segment

    public int tilesOnScreen = 5; // Number of ground segments to keep on screen

    public float tileLength = 10f; // Length of each ground segment

    private float spawnZ = 0f; // Z position to spawn the next ground segment


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < tilesOnScreen; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {
        Instantiate(groundPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        spawnZ += tileLength;
    }
}
