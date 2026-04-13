using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Coin : MonoBehaviour
{
    [SerializeField] float turnSpeed = 90f;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the coin spawned inside an obstacle and destroy it if so
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }

        // Check that the object we collided with has the "Player" tag
        // Make sure your "Mouse" object in the Inspector has Tag set to "Player"
        if (!other.gameObject.CompareTag("Player"))
        {
            return;
        }

        // Add to the player's score
        // Double-check if your GameManager uses "IncremnentScore" or "IncrementScore"
        GameManager.inst.IncremnentScore();

        // Destroy this coin object
        Destroy(gameObject);
    }

    private void Update()
    {
        // Rotate the coin every frame
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}