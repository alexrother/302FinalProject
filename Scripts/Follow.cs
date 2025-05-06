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
        gameOverText.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (speed < 5.5f)
        {
            speed += 0.01f;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= killDistance)
        {
            TriggerGameOver();
        }
    }

    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 1f;
            gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void TriggerGameOver()
    {
        gameOver = true;
        Time.timeScale = 0f;
        gameOverText.gameObject.SetActive(true);
    }
}
