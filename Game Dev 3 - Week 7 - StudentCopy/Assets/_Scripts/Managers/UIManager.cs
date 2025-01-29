using GameDevWithMarco.Data;
using TMPro;
using UnityEngine;


namespace GameDevWithMarco.Managers
{
    public class UIManager : MonoBehaviour
    {

        [SerializeField] TMP_Text scoreText;
        [SerializeField] TMP_Text winScoreText;
        [SerializeField] GlobalData globalData;



        // Updates the score text in the main UI during gameplay
        public void UpdateScoreText()
        {
            if (globalData != null)
            {
                //uses rich text to make the score bold using html-like notation
                scoreText.text = $"<b>Score</b>:{globalData.Score}";
            }

            else
            {
                Debug.Log("global data no assigned to game manager");
            }
        }

        // Updates the score text on the win menu
        public void UpdateWinScoreText()
        {
            if (globalData != null && winScoreText!)
            {
                winScoreText.text = $"<b>Points</b>: {globalData.Score}";
            }
        }

    }
}
