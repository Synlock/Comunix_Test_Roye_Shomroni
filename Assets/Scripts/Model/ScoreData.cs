using System.Collections.Generic;
using UnityEngine;

namespace ComunixTest.Model
{
    [CreateAssetMenu(fileName = "Score Data")]
    public class ScoreData : ScriptableObject
    {
        [SerializeField] List<Score> scores = new List<Score>();
        [SerializeField] bool hasPlayerBeenAdded = false;

        public List<Score> GetScores() => scores;

        public bool GetHasPlayerBeenAdded() => hasPlayerBeenAdded;
        public void SetHasPlayerBeenAdded(bool isAdded) => hasPlayerBeenAdded = isAdded;
    }
}