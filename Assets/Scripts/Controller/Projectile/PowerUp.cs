using UnityEngine;

namespace ComunixTest.Controller
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField] Model.ProjectileData powerUpType;
        [SerializeField] AudioClip clip;

        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                PlayerShooting ps = collision.gameObject.GetComponentInParent<PlayerShooting>();
                //set projectile params
                ps.GetCharacterData().SetMaxShots(powerUpType.GetShotsAmount());
                ps.SetProjectileData(powerUpType);

                //play sfx
                if(clip != null)
                    AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

                //prevent over spawning
                SpawnPowerUps.isSpawned = false;

                //can change this to object pooling to save on resources 
                Destroy(gameObject);
            }
        }
    }
}