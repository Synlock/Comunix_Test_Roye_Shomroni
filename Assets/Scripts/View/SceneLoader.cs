using ComunixTest.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ComunixTest.View
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] CharacterData player1Data;
        [SerializeField] CharacterData player2Data;

        [SerializeField] GameObject leaderboardPanel;
        [SerializeField] GameObject multiLeaderboardPanel;

        static int currentLevel = 1;
        
        bool showLeaderboard = false;
        bool showMultiLeaderboard = false;

        #region Getters/Setters
        public static int GetCurrentLevel() => currentLevel;
        #endregion
        
        public void LoadMainMenu()
        {
            SpawnPowerUps.isSpawned = false;
            currentLevel = 1;
            player1Data.ResetAllPlayerData();
            player2Data.ResetAllPlayerData();
            Time.timeScale = 1f;
            SceneManager.LoadScene(0);
        }
        public void LoadSinglePlayer()
        {
            SpawnPowerUps.isSpawned = false;
            currentLevel = 1;
            GameManager.SetIsMultiplayer(false);
            player1Data.ResetAllPlayerData();
            SceneManager.LoadScene(1);
        }
        public void LoadMultiplayer()
        {
            SpawnPowerUps.isSpawned = false;
            currentLevel = 1;
            GameManager.SetIsMultiplayer(true);
            player1Data.ResetAllPlayerData();
            player2Data.ResetAllPlayerData();
            SceneManager.LoadScene(1);
        }
        public void LoadNextLevel()
        {
            SpawnPowerUps.isSpawned = false;
            currentLevel++;
            Time.timeScale = 1f;
            player1Data.ResetDataNoScore();
            player2Data.ResetDataNoScore();
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            //loop to first level if reached last scene in build index
            if (currentSceneIndex >= SceneManager.sceneCountInBuildSettings - 1)
                SceneManager.LoadScene(1);
            else SceneManager.LoadScene(currentSceneIndex + 1);
        }
        public void ReloadLevel()
        {
            SpawnPowerUps.isSpawned = false;
            Time.timeScale = 1f;
            player1Data.SetIsHit(false);
            player2Data.SetIsHit(false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void ShowLeaderboard()
        {
            showLeaderboard = !showLeaderboard;

            if(showLeaderboard)
                leaderboardPanel.gameObject.SetActive(true);
            else leaderboardPanel.gameObject.SetActive(false);
        }
        public void ShowMultiLeaderboard()
        {
            showMultiLeaderboard = !showMultiLeaderboard;

            if(showMultiLeaderboard)
                multiLeaderboardPanel.gameObject.SetActive(true);
            else multiLeaderboardPanel.gameObject.SetActive(false);
        }
        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}