using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class logicscript : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;

    public float gameSpeed = 5f;
    public float speedIncreaseRate = 0.2f;
    public float maxGameSpeed = 10f;

    public GameObject gameStartscreen;
    public GameObject gameOverScreen;

    public bool gameStarted = false;
    public static bool hasStartedOnce = false; // Keep this static

    private birdscript bird;

    void Start()
    {
        // Find the bird and set its logic reference. This needs to happen consistently.
        bird = GameObject.FindGameObjectWithTag("Player").GetComponent<birdscript>();
        bird.SetLogicReference(this); // Reconnect logic to bird

        playerScore = 0;
        scoreText.text = "0";
        gameOverScreen.SetActive(false);
        gameSpeed = 5f;

        // The logic for displaying screens and freezing/resetting the bird
        // should primarily be driven by the game state (gameStarted and hasStartedOnce)
        if (!hasStartedOnce) // First time game loads
        {
            gameStartscreen.SetActive(true);
            bird.FreezeBird(); // Bird is frozen for the start screen
            gameStarted = false; // Game is not started yet
        }
        else // Subsequent loads (after "Play Again")
        {

            gameStartscreen.SetActive(false);
            StartGame();
        }
    }

    void Update()
    {
        if (gameStarted && gameSpeed < maxGameSpeed)
        {
            gameSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }

    public void StartGame()
    {
        hasStartedOnce = true; // Mark that the game has started at least once
        gameStarted = true;    // Set game state to active

        gameStartscreen.SetActive(false);
        gameOverScreen.SetActive(false);

        playerScore = 0;
        scoreText.text = "0";
        gameSpeed = 5f;

        // Make sure the bird is fully reset and ready for play
        if (bird != null) // Add null check for safety
        {
            bird.ResetBird();
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void addScore()
    {
        if (gameStarted) // Only add score if the game is active
        {
            playerScore++;
            scoreText.text = playerScore.ToString();
        }
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        gameStarted = false; // Game stops
        // Optionally, freeze pipes/background here if they aren't controlled by gameStarted
        if (bird != null)
        {
            bird.FreezeBird(); // Keep the bird frozen after collision
        }
    }
}
