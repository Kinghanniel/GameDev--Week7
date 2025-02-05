using System.Collections;
using GameDevWithMarco.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDevWithMarco.Managers
{
    // Manages the overall game state, including win conditions and scene transitions
    public class GameManager : MonoBehaviour
    {
        // Reference to global game data (tracks score and win conditions)
        [SerializeField] GlobalData globalData;

        // UI element for the win menu
        [SerializeField] GameObject winMenu;

        // Reference to UI Manager for updating UI elements
        [SerializeField] private UIManager uiManager;

        /// <summary>
        /// Initializes the game state on start.
        /// Resets the score and sets the required score to win.
        /// </summary>
        private void Start()
        {
            if (globalData != null)
            {
                globalData.ResetScore();
                globalData.SetTheScoreRequiredToWin();
            }
            else
            {
                Debug.LogWarning("GlobalData is not assigned to GameManager.");
            }
        }

        /// <summary>
        /// Starts the win sequence, triggering a coroutine with a delay.
        /// </summary>
        public void GameWon()
        {
            StartCoroutine(GameWonWithDelay());
        }

        /// <summary>
        /// Handles game win behavior, including UI updates and scene transitions.
        /// </summary>
        IEnumerator GameWonWithDelay()
        {
            // Wait for 2 seconds before showing the win menu
            yield return new WaitForSeconds(2);

            // Make the cursor visible and unlock it
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            // Display the win menu if assigned
            if (winMenu != null)
            {
                winMenu.SetActive(true);

                // Update the UI to reflect the player's score
                uiManager.UpdateWinScoreText();
            }

            // Wait 5 seconds before transitioning to the next scene
            yield return new WaitForSeconds(5);

            // Freeze game time
            Time.timeScale = 0;

            // Determine the next scene
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            // Load the next scene if it exists
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
                Time.timeScale = 1; // Unfreeze time after loading the scene
            }

            // Log win message to console
            Debug.Log("GAME WON");
        }
    }
}
