using UnityEngine;

namespace ComunixTest.Model
{
    [CreateAssetMenu(menuName = "BallConfig")]
    public class BallData : ScriptableObject
    {
        [SerializeField] int score = 0;

        [Tooltip("Maximum Bounce")]
        [SerializeField] float maxVelocity = 5f;

        [SerializeField] float moveSpeed = 3f;

        [Tooltip("Force applied on Start")]
        [SerializeField] float upwardsForce = 200f;

        public int GetScore() => score;
        public void SetScore(int newScore) => score = newScore;

        public float GetMaxVelocity() => maxVelocity;
        public void SetMaxVelocity(float newVel) => maxVelocity = newVel;

        public float GetMoveSpeed() => moveSpeed;
        public void SetMoveSpeed(float newSpeed) => moveSpeed = newSpeed;

        public float GetUpwardsForce() => upwardsForce;
        public void SetUpwardsForce(float newForce) => upwardsForce = newForce;
    }
}