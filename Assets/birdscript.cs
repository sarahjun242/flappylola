using UnityEngine;

public class birdscript : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float flapStrength = 5f;
    public logicscript logic; // This reference will be null right after scene load initially
    public bool birdIsAlive = true;

    void Awake() // Use Awake() for GetComponent if you need it before Start()
    {
        if (myRigidbody == null)
        {
            myRigidbody = GetComponent<Rigidbody2D>();
        }
    }

    void Start()
    {
        // This ensures FreezeBird() only happens on the very first load.
        // For subsequent loads (Play Again), logic.gameStarted will be true or handled by ResetBird.
        if (logic == null || !logic.gameStarted)
        {
            FreezeBird();
        }
        else
        {
            ResetBird();
        }
    }

    void Update()
    {
        // It's safer to check for null on logic, although SetLogicReference should prevent it.
        if (logic == null || !logic.gameStarted) return;

        if (Input.GetKeyDown(KeyCode.Space) && birdIsAlive)
        {
            Debug.Log("SPACE pressed AND BIRD IS ALIVE!");
            myRigidbody.linearVelocity = Vector2.up * flapStrength;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!birdIsAlive || logic == null) return;

        Debug.Log("Hit: " + collision.gameObject.name);
        birdIsAlive = false;
        logic.gameOver();
    }

    public void SetLogicReference(logicscript logicScript)
    {
        logic = logicScript;
    }

    public void FreezeBird()
    {
        myRigidbody.simulated = false;
        myRigidbody.linearVelocity = Vector2.zero;
        birdIsAlive = false; // The key change will be how this is un-set
    }

    public void ResetBird()
    {
        transform.position = new Vector3(0, 0, 0); // Reset position
        myRigidbody.linearVelocity = Vector2.zero; // Clear velocity
        myRigidbody.simulated = true; // Enable physics
        birdIsAlive = true; // Set bird to alive
        // Ensure gravity is set if you've altered it elsewhere
        // myRigidbody.gravityScale = 1f; // Add this if you ever set gravityScale to 0
    }
}
