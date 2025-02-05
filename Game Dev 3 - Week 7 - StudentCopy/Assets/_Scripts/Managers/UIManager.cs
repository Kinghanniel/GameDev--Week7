using GameDevWithMarco.Data;
using TMPro;
using UnityEngine;

namespace GameDevWithMarco.Managers
{
    // Manages UI updates related to score display
    public class UIManager : MonoBehaviour
    {
        // Reference to the TextMeshPro text component that displays the player's score
        [SerializeField] TMP_Text scoreText;

        // Reference to the TextMeshPro text component for displaying the score on the win screen
        [SerializeField] TMP_Text winScoreText;

        // Reference to the global data containing the player's score
        [SerializeField] GlobalData globalData;

        /// <summary>
        /// Updates the score text in the main UI during gameplay.
        /// </summary>
        public void UpdateScoreText()
        {
            if (globalData != null)
            {
                // Uses rich text to make the score bold using HTML-like notation
                scoreText.text = $"<b>Score</b>: {globalData.Score}";
            }
            else
            {
                Debug.LogWarning("GlobalData is not assigned to UIManager.");
            }
        }

        /// <summary>
        /// Updates the score text displayed on the win screen.
        /// </summary>
        public void UpdateWinScoreText()
        {
            // Ensures that both globalData and winScoreText are assigned before updating the UI
            if (globalData != null && winScoreText != null)
            {
                winScoreText.text = $"<b>Points</b>: {globalData.Score}";
            }
            else
            {
                Debug.LogWarning("GlobalData or winScoreText is not assigned to UIManager.");
            }
        }
    }
}
