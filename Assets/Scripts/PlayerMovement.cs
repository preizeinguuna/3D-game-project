using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    bool alive = true;

    [Header("Movement")]
    public float speed = 10;
    [SerializeField] Rigidbody rb;
    float horizontalInput;
    [SerializeField] float horizontalMultiplier = 1.7f;
    [SerializeField] float jumpForce = 600f;
    [SerializeField] LayerMask groundMask;

    [Header("Subway Surfers Settings")]
    public float speedIncreaseOverTime = 0.05f;
    public float speedIncreasePerPoint = 0.5f;

    [Header("Audio")]
    public AudioSource effectsSource;
    public AudioClip coinSound;
    public AudioClip crashSound;
    public AudioSource backgroundMusic;

    private void FixedUpdate()
    {
        if (!alive || Time.timeScale == 0) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    private void Update()
    {
        if (!alive || Time.timeScale == 0) return;

        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        speed += speedIncreaseOverTime * Time.deltaTime;
    }

    public void CollectCoin()
    {
        if (!alive || Time.timeScale == 0) return;

        if (effectsSource != null && coinSound != null)
        {
            effectsSource.PlayOneShot(coinSound, 0.3f);
        }

        if (GameManager.inst != null)
        {
            GameManager.inst.IncrementScore();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!alive) return;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
        }
    }

    public void Die()
    {
        if (!alive) return;

        alive = false;

        if (effectsSource != null && crashSound != null)
            effectsSource.PlayOneShot(crashSound, 1f);

        if (backgroundMusic != null)
            backgroundMusic.Stop();

        Invoke(nameof(OpenGameOver), 0.5f);
    }

    void OpenGameOver()
    {
        if (GameManager.inst != null)
        {
            GameManager.inst.ShowGameOver();
        }
    }

    void Jump()
    {
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        if (isGrounded && alive)
            rb.AddForce(Vector3.up * jumpForce);
    }
}