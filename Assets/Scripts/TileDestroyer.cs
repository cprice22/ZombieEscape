using UnityEngine;

public class TileDestroyer : MonoBehaviour
{
    public float destroyDelay = 8f; // Time in seconds before the tile is destroyed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, destroyDelay); // Schedule the destruction of this tile after the specified delay
    }

}
