//using UnityEditor.Experimental.GraphView;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRunning : MonoBehaviour
{
    public enum ControlMode
    {
        Swipe,
        Buttons,
        Gyro
    }

    //Swipe Detection Variables
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private bool isSwiping = false;
    public float swipeThreshold = 50f; // Minimum distance for a swipe to be registered

    //Gyro Variables
    float tiltSensitivity = 2f;
    float tiltThreshold = 0.2f;

    public ControlMode currentMode = ControlMode.Buttons;

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

        if(SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;
        }
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

        //Swipe Logic
        if(currentMode == ControlMode.Swipe)
        {
            HandleSwipe();
        }

        //Gyro Logic
        if(currentMode == ControlMode.Gyro)
        {
            HandleGyro();
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

    //Swipe Handling
    void HandleSwipe()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Ended:
                    if (!isSwiping) return;

                    endTouchPosition = touch.position;
                    Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                    if(swipeDelta.magnitude > swipeThreshold)
                    {
                        //Horizontal swipe
                        if(Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                        {
                            if(swipeDelta.x > 0)
                            {
                                ChangeLane(1); // Swipe Right
                            }
                            else
                            {
                                ChangeLane(-1); // Swipe Left
                            }
                        }
                    }
                    isSwiping = false;
                    break;
            }
        }
       
#if UNITY_EDITOR
            // Mouse swipe simulation for testing in editor
            if (Input.GetMouseButtonDown(0))
            {
                startTouchPosition = Input.mousePosition;
                isSwiping = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (!isSwiping) return;

                endTouchPosition = Input.mousePosition;
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                if (swipeDelta.magnitude > swipeThreshold)
                {
                    if (swipeDelta.x > 0)
                        ChangeLane(1);
                    else
                        ChangeLane(-1);
                }

                isSwiping = false;
            }
 //Testing: swipe does not work in editor, so we can use this to simulate it
#else
    // Real touch input for mobile
    if (Input.touchCount > 0)
    {
        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                startTouchPosition = touch.position;
                isSwiping = true;
                break;

            case TouchPhase.Ended:
                if (!isSwiping) return;

                endTouchPosition = touch.position;
                Vector2 swipeDelta = endTouchPosition - startTouchPosition;

                if (swipeDelta.magnitude > swipeThreshold)
                {
                    if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                    {
                        if (swipeDelta.x > 0)
                            ChangeLane(1);
                        else
                            ChangeLane(-1);
                    }
                }

                isSwiping = false;
                break;
        }
    }
#endif
        }

    //Gyro Handling
    void HandleGyro()
    {
        float tilt = Input.acceleration.x;

        if (tilt > tiltThreshold)
        {
            ChangeLane(1); //tilt right
        }
        else if (tilt < -tiltThreshold)
        {
            ChangeLane(-1); //tilt left
        }
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
