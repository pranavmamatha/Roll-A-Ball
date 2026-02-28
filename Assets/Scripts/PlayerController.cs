using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI scoreText;

    private Rigidbody rb;
    private int count;
    private int score = 0;
    private float movementX;
    private float movementY;
    private bool gameActive = true;

    void Start()
    {
        count = GameObject.FindGameObjectsWithTag("PickUp").Length;
        rb = GetComponent<Rigidbody>();
        statusText.gameObject.SetActive(false);
        UpdateScoreText();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void FixedUpdate()
    {
        if (!gameActive) return;

        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        if (transform.position.y < -5f && gameActive)
        {
            StartCoroutine(EndGame(false));
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + (score * 100);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score++;
            count--;
            UpdateScoreText();

            if (count <= 0)
            {
                StartCoroutine(EndGame(true));
            }
        }
        else if (other.gameObject.CompareTag("Danger"))
        {
            other.gameObject.SetActive(false);
            StartCoroutine(EndGame(false));
        }
    }

    IEnumerator EndGame(bool won)
    {
        gameActive = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        statusText.gameObject.SetActive(true);
        statusText.text = won ? "Win!!!!!\nScore: " + (score * 100) : "Lose :(\nScore: " + (score * 100);
        statusText.color = won ? Color.green : Color.red;

        yield return new WaitForSeconds(3f);

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }
}