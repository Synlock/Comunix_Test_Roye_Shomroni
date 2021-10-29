using ComunixTest.Model;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ComunixTest.View
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] ScoreData scoreData;

        public ScoreData GetScoreData() => scoreData;

        public IEnumerable<Score> GetHighScores()
        {
            return scoreData.GetScores().OrderByDescending(x => x.score);
        }
        public void AddScore(Score score)
        {
            scoreData.GetScores().Add(score);
        }
    }
}