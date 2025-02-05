using UnityEngine;

namespace GameDevWithMarco.RandomStuff
{
    // Coin class represents a collectible coin with a specified value
    public class Coin : MonoBehaviour
    {
        // The value of the coin, settable from the Unity Inspector
        [SerializeField] int coinValue;

        // Public property to access the coin's value
        public int CoinValue
        {
            get
            {
                return coinValue;
            }
        }
    }
}
