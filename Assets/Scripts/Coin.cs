using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;
    [SerializeField] AudioClip collectSound;
    [SerializeField] float collectVolume = 1f;

    private float canCollectTime;

    void Start()
    {
        canCollectTime = Time.unscaledTime + 0.2f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.gameStarted) return;
        if (Time.unscaledTime < canCollectTime) return;

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.CollectCoin();
            }

            if (collectSound != null)
            {
                AudioSource.PlayClipAtPoint(collectSound, transform.position, collectVolume);
            }

            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
        }
    }
}