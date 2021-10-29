using ComunixTest.Model;
using System.Linq;
using UnityEngine;

namespace ComunixTest.View
{
    public class LeaderboardUI : MonoBehaviour
    {
        [SerializeField] CharacterData player1Data;
        [SerializeField] CharacterData player2Data;

        [SerializeField] RowUI rowUI;
        [SerializeField] ScoreManager scoreManager;
        [SerializeField] Transform content;

        string[] defaultNames = { "John", "Maria", "Joan", "Harrietta", "George", "Tom", "Timothy", "Janet", "Holly", "Wendy" };

        //score resets after every match instead of logging permanent high score
        void Start()
        {
            InitLeaderboard();
        }

        public void InitLeaderboard()
        {
            if (scoreManager.GetScoreData().GetScores().Count <= 10)
            {
                for (int i = 0; i < defaultNames.Length; i++)
                {
                    int randomNumber = Random.Range(100, 10001);
                    scoreManager.GetScoreData().GetScores().Add(new Score(defaultNames[i], randomNumber));
                }
            }

            Score[] scores = scoreManager.GetHighScores().ToArray();

            for (int i = 0; i < 10; i++)
            {
                RowUI row = Instantiate(rowUI, content).GetComponent<RowUI>();
                row.rank.text = (i + 1).ToString();
                row.name.text = scores[i].name;
                row.score.text = scores[i].score.ToString();
            }
        }
    }
}