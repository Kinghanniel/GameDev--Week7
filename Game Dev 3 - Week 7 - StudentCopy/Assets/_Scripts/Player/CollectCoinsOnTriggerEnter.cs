using GameDevWithMarco.Data;
using GameDevWithMarco.Managers;
using GameDevWithMarco.ObserverPattern;
using GameDevWithMarco.RandomStuff;
using UnityEngine;

namespace GameDevWithMarco
{
    public class CollectCoinsOnTriggerEnter : MonoBehaviour
    {

        [SerializeField] GameEvent coinColletected;
        [SerializeField] GlobalData globalData;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Coin")
            {
                // gets the value of the coin and stores it into a variable
                int coinValue = collision.GetComponent<Coin>().CoinValue;


                if (globalData != null)
                {
                    globalData.AddToScore(coinValue);
                }
                else
                {
                    Debug.Log("global data no assigned to game manager");
                }

  

                //destroys the coin
                Destroy(collision.gameObject);

                //raises the event for any listeners to reat to
                coinColletected.Raise();
            }
        }
    }
}
