using UnityEngine;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    void Start()
    {
        // Find the player movement script in the scene
        playerMovement = GameObject.FindFirstObjectByType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object we collided with has the "Player" tag
        // This will now work for your "Mouse" object
        if (collision.gameObject.CompareTag("Player"))
        {
            playerMovement.Die();
        }
    }
}