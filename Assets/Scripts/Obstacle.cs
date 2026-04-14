using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    [SerializeField] AudioClip hitSound;
    [SerializeField] float hitVolume = 1.5f;

    void Start()
    {
        playerMovement = GameObject.FindFirstObjectByType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!GameManager.gameStarted) return;

        if (collision.gameObject.CompareTag("Player"))
        {
            if (hitSound != null)
            {
                AudioSource.PlayClipAtPoint(hitSound, transform.position, hitVolume);
            }

            playerMovement.Die();
        }
    }
}