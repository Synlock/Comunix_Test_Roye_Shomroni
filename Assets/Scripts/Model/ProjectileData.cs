using UnityEngine;

namespace ComunixTest.Model
{
    public enum PowerUpType { None, Locked, Double, Gun }

    [CreateAssetMenu(menuName = "ProjectileConfig")]
    public class ProjectileData : ScriptableObject
    {
        [SerializeField] int shotsAmount = 1;
        [SerializeField] float speed = 5f;
        [SerializeField] PowerUpType powerUpType;

        public int GetShotsAmount() => shotsAmount;
        public void SetShotsAmount(int newAmount) => shotsAmount = newAmount;

        public float GetSpeed() => speed;
        public void SetSpeed(float newSpeed) => speed = newSpeed;

        public PowerUpType GetPowerUpType() => powerUpType;
        public void SetPowerUpType(PowerUpType newType) => powerUpType = newType;
    }
}