using GameDevWithMarco.Data;
using GameDevWithMarco.Singleton;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameDevWithMarco.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] GlobalData globalData;
        [SerializeField] GameObject winMenu;
        [SerializeField] private UIManager uiManager;



        private void Start()
        {
            if (globalData != null)
            {
                globalData.ResetScore();
                globalData.SetTheScoreRequiredToWin();
            }
            else
            {
                Debug.LogWarning(" global data no assigned to game manager");
            }
        }



        // setter
        public void GameWon()
        {
            StartCoroutine(GameWonWithDelay());
        }


        IEnumerator GameWonWithDelay()
        {
            yield return new WaitForSeconds(2);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (winMenu != null)
            {
                winMenu.SetActive(true);

                uiManager.UpdateWinScoreText(); // Update the win score text
            }

            yield return new WaitForSeconds(5);

            // stops the player
            Time.timeScale = 0;

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            // Check if the next scene index is within range
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
                Time.timeScale = 1;
            }

            //prints the message to console
            Debug.Log("GAME WON");
        }
    }
}
