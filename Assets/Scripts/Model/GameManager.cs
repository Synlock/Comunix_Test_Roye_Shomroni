using ComunixTest.Controller;
using ComunixTest.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComunixTest.Model
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        static bool hasGameStarted = false;
        public static bool isMultiplayer = true;

        public static bool isAndroid = false;

        public static float leftBound { get { return -15.5f; } }
        public static float rightBound { get { return 15.5f; } }

        [SerializeField] ScoreData scoreData;
        [SerializeField] ScoreData multiplayerScoreData;
        [SerializeField] CharacterData player1Data;
        [SerializeField] CharacterData player2Data;

        [SerializeField] GameObject multiplayerButton;
        [SerializeField] GameObject multiLeaderboardButton;

        [SerializeField] GameObject player2Prefab;

        [SerializeField] AudioClip winClip;
        [SerializeField] AudioClip loseClip;
        bool hasClipPlayed = false;

        [Tooltip("Must be placed on balls parent")]
        BallsParent ballsParent;

        [SerializeField] GameObject WinPanel;
        [SerializeField] GameObject LosePanel;
        [SerializeField] GameObject GameOverPanel;

        [SerializeField] float initialDelayTimer = 2f;
        float timer = 2f;

        Predicate<int> startConditionGT = value => value > 0;
        Predicate<int> startConditionLTE = value => value <= 0;

        #region Unity Methods
        public void Awake()
        {
        }

        void Start()
        {
            //disable multiplayer on mobile
            if (isAndroid)
            {
                if(multiplayerButton != null)
                    multiplayerButton.SetActive(false);
                if (multiLeaderboardButton != null)
                    multiLeaderboardButton.SetActive(false);
            }

            Instance = this;
            InitScoreList();
            timer = initialDelayTimer;
            ballsParent = FindObjectOfType<BallsParent>();

            if (isMultiplayer)
                Instantiate(player2Prefab);
        }
        void Update()
        {
            WinLevel();
            LoseLevel();
        }
        #endregion

        #region Getters/Setters
        public static bool GetHasGameStarted() => hasGameStarted;
        public static void SetHasGameStarted(bool isStarted) => hasGameStarted = isStarted;

        public static bool GetIsMultiplayer() => isMultiplayer;
        public static void SetIsMultiplayer(bool isTwoPlayers) => isMultiplayer = isTwoPlayers;
        #endregion

        //if leaderboard is empty, this fills with default values
        //multiplayer values are seperate from single player
        public void InitScoreList()
        {
            if (!isMultiplayer)
            {
                if (!scoreData.GetHasPlayerBeenAdded())
                {
                    scoreData.GetScores().Add(new Score(player1Data.GetName(), 0));
                    scoreData.SetHasPlayerBeenAdded(true);
                }
            }
            else
            {
                if (!multiplayerScoreData.GetHasPlayerBeenAdded())
                {
                    multiplayerScoreData.GetScores().Add(new Score(player1Data.GetName(), 0));
                    multiplayerScoreData.GetScores().Add(new Score(player2Data.GetName(), 0));
                    multiplayerScoreData.SetHasPlayerBeenAdded(true);
                }
            }
        }

        public void WinLevel()
        {
            //if balls container reaches 0 children - winPanel becomes active
            if (WinPanel != null)
                EndLevelSlowMo(startConditionLTE, ballsParent.transform.childCount, WinPanel, winClip);
        }
        public void LoseLevel()
        {
            //if lives are greater than 0 show panel with restart button
            //otherwise show panel with only main menu button
            if (!isMultiplayer)
            {
                if (player1Data.GetIsHit() && player1Data.GetLives() > 0 && LosePanel != null)
                    EndLevelSlowMo(startConditionGT, player1Data.GetLives(), LosePanel, loseClip);
                else
                {
                    if (LosePanel != null)
                        EndLevelSlowMo(startConditionLTE, player1Data.GetLives(), GameOverPanel, loseClip);

                }
            }
            else
            {
                if ((player1Data.GetIsHit() && player1Data.GetLives() > 0) &&
                    (player2Data.GetIsHit() && player2Data.GetLives() > 0))
                {
                    if (LosePanel != null)
                        EndLevelSlowMo(startConditionGT, player1Data.GetLives(), LosePanel, loseClip);
                }
                else
                {
                    if (GameOverPanel != null)
                        EndLevelSlowMo(startConditionLTE, player1Data.GetLives(), GameOverPanel, loseClip);

                }
            }

        }
        void EndLevelSlowMo(Predicate<int> condition, int compareValue, GameObject panel, AudioClip clip = null)
        {
            //method to end a level with slow-mo
            if (condition(compareValue))
            {
                timer -= Time.unscaledDeltaTime;
                Time.timeScale -= Time.deltaTime;
                if (timer <= 0)
                {
                    SetHasGameStarted(false);
                    panel.SetActive(true);
                    timer = initialDelayTimer;

                    if (clip != null && !hasClipPlayed)
                    {
                        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                        hasClipPlayed = true;
                    }

                    if (isMultiplayer)
                    {
                        UpdateLeaderboard(player1Data, multiplayerScoreData);
                        UpdateLeaderboard(player2Data, multiplayerScoreData);
                    }
                    else UpdateLeaderboard(player1Data, scoreData);
                }
            }

        }

        private void UpdateLeaderboard(CharacterData data, ScoreData scoreData)
        {
            //update leaderboard score
            for (int i = 0; i < scoreData.GetScores().Count; i++)
            {
                if (scoreData.GetScores()[i].name == data.GetName())
                {
                    scoreData.GetScores()[i].score = data.GetHighScore();
                    data.SetHighScore(data.GetScore());
                }
            }
        }
    }
}