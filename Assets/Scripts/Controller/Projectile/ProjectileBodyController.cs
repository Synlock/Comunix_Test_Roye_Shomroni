using ComunixTest.Model;
using UnityEngine;

namespace ComunixTest.Controller
{
    public class ProjectileBodyController : MonoBehaviour
    {
        [SerializeField] ProjectileBehavior projectile;

        public ProjectileBehavior GetProjectile() => projectile;

        void Update()
        {
            ActivateProjectileBody();

        }

        //conditional of body activation
        void ActivateProjectileBody()
        {
            if (projectile.GetPlayer() != null)
            {
                if (projectile.gameObject.activeInHierarchy || projectile.isLocked)
                {
                    LoopThroughChildren(true);
                }
                else
                {
                    LoopThroughChildren(false);
                }
            }
        }

        //activate projectile body one at a time
        public void LoopThroughChildren(bool areActive)
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                if (projectile != null)
                    if (projectile.transform.position.y > transform.GetChild(i).position.y)
                        transform.GetChild(i).gameObject.SetActive(areActive);
            }
            if (!areActive)
            {
                if (projectile.GetPlayer() != null)
                    transform.position = new Vector2(projectile.GetPlayer().transform.position.x, 0f);
            }
        }
    }
}