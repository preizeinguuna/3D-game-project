using UnityEngine;

public class Coin : MonoBehaviour
{
    public float turnsSpeed = 90f;

    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.GetComponent<Obstacle>() != null)
            {
                Destroy(gameObject);
                return;
        }

    
        //Check if the object we collided with is the player
        if (other.gameObject.name == "Player")
        {
            return;
        }

        //Add points to the player score 


        //Destroy the coin  object
        Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Rotate(0, 0, turnsSpeed * Time.deltaTime);
    }
}
