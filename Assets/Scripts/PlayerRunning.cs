//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerRunning : MonoBehaviour
{
    public float forwardSpeed = 8f;

    public float laneDistance = 3f;

    public float laneSwitchSpeed = 10f;

    private int currentLane = 1; // 0 = left, 1 = middle, 2 = right

    private Rigidbody rb;

    public GroundSpawner groundSpawner; // Reference to the GroundSpawner script

    private float lastSpawnZ = 0f; // Track the last Z position where a ground segment was spawned

    public GameObject gameOverPanel; // Reference to the Game Over UI panel

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameState.Started) return;

        //Constant Forward Movement
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, rb.linearVelocity.y, forwardSpeed);

        //Lane Input (Keyboard For Testing)
        if (Input.GetKeyDown(KeyCode.A))
            ChangeLane(-1);
        if (Input.GetKeyDown(KeyCode.D))
            ChangeLane(1);

        //Smooth Lane Movement
        Vector3 targetPosition = new Vector3((currentLane - 1) * laneDistance, transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, laneSwitchSpeed * Time.deltaTime);

        //Spawn Ground Segments
        if (transform.position.z - lastSpawnZ > 10f)
        {
            groundSpawner.SpawnTile();
            lastSpawnZ += 10f;
        }
    }

    void ChangeLane(int direction)
    {
        currentLane += direction;
        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }

    //Change to mobile
    public void ChangeLanePublic(int direction)
    {
        ChangeLane(direction);
    }

     void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        forwardSpeed = 0f; // Stop the player from moving forward
        gameOverPanel.SetActive(true); // Show the Game Over UI panel
        
        FindFirstObjectByType<GameTimer>()?.StopAndShowFinish();

        Debug.Log("GAME OVER");
    }
}
