using GameDevWithMarco.ObserverPattern;
using GameDevWithMarco.RandomStuff;
using UnityEngine;

namespace GameDevWithMarco.Data
{

    [CreateAssetMenu(fileName = "New Blobal Data", menuName = "Scriptable Objects/Data")]
    public class GlobalData : ScriptableObject
    {
        private int score = 0;
        private int scoreRequiredToWin;
        [SerializeField] GameEvent gameWon;

        public int Score
        {
            get
            {
                return score;
            }
        }

        public void ResetScore()
        {

            //to reset the score each time we start playing 
            //without this code the code will persist between runs
            score = 0;
        }

        public void SetTheScoreRequiredToWin()
        {
            //reset the starting point
            scoreRequiredToWin = 0;
            //store all the coins into the array
            Coin[] storeAllCoins = FindObjectsOfType<Coin>();

            //goes trough the array and adds the coin value to
            //the score required to win varible
            foreach (Coin coin in storeAllCoins)
            {
                scoreRequiredToWin += coin.CoinValue;
            }
        }

        public void AddToScore(int amountToAdd)
        {
            //change amount to positive so we stop negative values from being added

            int sortedScore = Mathf.Abs(amountToAdd);

            //then add the score
            score += sortedScore;

            //to check if we won
            if (score >= scoreRequiredToWin)
            {
                gameWon.Raise();
            }
        }
    }
}
