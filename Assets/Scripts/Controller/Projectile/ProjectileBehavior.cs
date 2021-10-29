using ComunixTest.Model;
using UnityEngine;

namespace ComunixTest.Controller
{
    public class ProjectileBehavior : MonoBehaviour
    {
        [SerializeField] ProjectileData projectileData;
        [SerializeField] PlayerShooting player;
        [SerializeField] ProjectileBodyController projectileBody;

        [SerializeField] AudioClip clip;

        [SerializeField] ParticleSystem hitFX;

        public Collider2D[] childrenCols;

        public bool isOn = false;
        public bool isLocked = false;

        public PlayerShooting GetPlayer() => player;
        public void SetPlayer(PlayerShooting newPlayer) => player = newPlayer;

        public ProjectileData GetProjectileData() => projectileData;
        public void SetPowerUpType(PowerUpType newPowerUpType) => projectileData.SetPowerUpType(newPowerUpType);
        
        void Start()
        {
            childrenCols = GetComponentsInChildren<Collider2D>();
        }

        void Update()
        {
            TranslateProjectileHead();
        }

        void TranslateProjectileHead()
        {
            if (isOn)
                transform.Translate(Vector2.up * (projectileData.GetSpeed() * Time.deltaTime), Space.World);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Top") || other.gameObject.layer == LayerMask.NameToLayer("Balls"))
            {
                PlayFX();
                PowerUpSwitch();
            }
        }
        void PowerUpSwitch()
        {
            //can easily add new power ups to this switch or
            //we can also create new powerups in sub classes to make the code easier to read
            switch (player.GetProjectileData().GetPowerUpType())
            {
                case PowerUpType.Double:
                    PowerUpBase();
                    break;

                case PowerUpType.Locked:
                    PowerUpBase(true);
                    projectileBody.transform.position = new Vector2(transform.position.x, 0f);
                    break;

                case PowerUpType.Gun:
                    PowerUpBase();
                    break;

                default:
                    PowerUpBase();
                    break;
            }
        }
        //the base parameters of all powerups
        void PowerUpBase(bool locked = false)
        {
            projectileBody.LoopThroughChildren(false);
            isOn = false;
            isLocked = locked;
            gameObject.SetActive(false);
        }
        //play vfx & sfx on hit
        void PlayFX()
        {
            if (hitFX != null)
            {
                hitFX.transform.position = transform.position;
                hitFX.Play();
            }
            if(clip != null)
            {
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            }
        }
    }
}