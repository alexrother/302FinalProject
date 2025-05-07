using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EnemyFollow : MonoBehaviour
{
    public Transform player;
    public float speed = 3.0f;
    public float killDistance = 0.5f;
    public TextMeshProUGUI gameOverText;

    private Rigidbody2D rb;
    private static bool gameOver = false;

    void Start()
    {
        gameOver = false;
        // Hide / turn off gameover text
        gameOverText.gameObject.SetActive(false);
        // Get enemy rigidbody
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // If speed is less than 5.5
        // increment speed
        if (speed < 5.5f)
        {
            speed += 0.01f;
        }

        // Get difference = player pos - enemy pos
        Vector2 direction = player.position - transform.position;
        // previous position + (direction * speed * relative to realtime)
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        // distance = Distance( enemy pos, player pos)
        float distance = Vector2.Distance(transform.position, player.position);

        // If enemy gets with distance
        // kill player and gameover
        if (distance <= killDistance)
        {
            TriggerGameOver();
        }
    }

    void Update()
    {
        // If game is over and user presses spacebar
        if (gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            // Set time back to normal
            Time.timeScale = 1f;
            // Reset gameOver bool
            gameOver = false;
            // Reset scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Gameover
    void TriggerGameOver()
    {   
        // Pause time
        Time.timeScale = 0f;
        // Show gameover text
        gameOverText.gameObject.SetActive(true);
    }
}
