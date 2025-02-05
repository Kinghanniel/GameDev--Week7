using GameDevWithMarco.Data;
using GameDevWithMarco.ObserverPattern;
using GameDevWithMarco.RandomStuff;
using UnityEngine;

namespace GameDevWithMarco
{
    // Handles coin collection when an object enters a trigger collider
    public class CollectCoinsOnTriggerEnter : MonoBehaviour
    {
        // Event raised when a coin is collected (used for observer pattern)
        [SerializeField] GameEvent coinColletected;

        // Reference to the global data for managing the score
        [SerializeField] GlobalData globalData;

        /// <summary>
        /// Triggered when another collider enters the object's trigger area.
        /// If the object is a coin, it adds to the player's score and destroys the coin.
        /// </summary>
        /// <param name="collision">The collider that entered the trigger.</param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Check if the collided object has the tag "Coin"
            if (collision.gameObject.CompareTag("Coin"))
            {
                // Try to get the Coin component
                Coin coin = collision.GetComponent<Coin>();

                if (coin != null)
                {
                    // Get the coin's value
                    int coinValue = coin.CoinValue;

                    // Add the coin's value to the player's score
                    if (globalData != null)
                    {
                        globalData.AddToScore(coinValue);
                    }
                    else
                    {
                        Debug.LogWarning("GlobalData is not assigned to CollectCoinsOnTriggerEnter.");
                    }

                    // Destroy the collected coin
                    Destroy(collision.gameObject);

                    // Raise the event to notify listeners about coin collection
                    if (coinColletected != null)
                    {
                        coinColletected.Raise();
                    }
                    else
                    {
                        Debug.LogWarning("CoinCollected event is not assigned to CollectCoinsOnTriggerEnter.");
                    }
                }
                else
                {
                    Debug.LogWarning("Coin component not found on object tagged as 'Coin'.");
                }
            }
        }
    }
}
