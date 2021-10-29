using UnityEngine;

namespace ComunixTest.Model
{
    [CreateAssetMenu(menuName ="CharacterConfig")]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] string name = "Guest";
        [SerializeField] int score = 0;
        [SerializeField] int highScore = 0;

        [SerializeField] int lives = 3;
        [SerializeField] float moveSpeed = 7f;
        [SerializeField] int maxShots = 1;
        [SerializeField] int shots = 1;

        [SerializeField] bool isHit = false;

        public string GetName() => name;
        public void SetName(string newName) => name = newName; 

        public int GetScore() => score;
        public void SetScore(int newScore) => score = newScore;

        public int GetHighScore() => score;
        public void SetHighScore(int newHighScore) => highScore = newHighScore;

        public int GetLives() => lives;
        public void SetLives(int newLives) => lives = newLives;

        public float GetMoveSpeed() => moveSpeed;
        public void SetMoveSpeed(float newMoveSpeed) => moveSpeed = newMoveSpeed;

        public int GetMaxShots() => maxShots;
        public void SetMaxShots(int newMaxShots) => maxShots = newMaxShots;

        public int GetShots() => shots;
        public void SetShots(int newshots) => shots = newshots;

        public bool GetIsHit() => isHit;
        public void SetIsHit(bool newIsHit) => isHit = newIsHit;

        public void ResetAllPlayerData()
        {
            score = 0;
            lives = 3;
            moveSpeed = 7f;
            maxShots = 1;
            shots = 1;
            isHit = false;
        }
        public void ResetDataNoScore()
        {
            lives = 3;
            moveSpeed = 7f;
            maxShots = 1;
            shots = 1;
            isHit = false;
        }
    }
}