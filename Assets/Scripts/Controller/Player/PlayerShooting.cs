using ComunixTest.Model;
using System.Collections.Generic;
using UnityEngine;

namespace ComunixTest.Controller
{
    public class PlayerShooting : MonoBehaviour
    {
        Transform myTransform;
        [SerializeField] Transform projectileTransform;

        [SerializeField] CharacterData characterData;
        [SerializeField] ProjectileData projectileData;

        [SerializeField] AudioClip clip;

        int instantiateCounter = 0;
        int shotCounter = 0;

        List<ProjectileBehavior> projectiles = new List<ProjectileBehavior>();

        [SerializeField] KeyCode shootKey;

        [SerializeField] GameObject projPrefab;

        Animator animator;

        #region Unity Methods
        private void Start()
        {
            myTransform = GetComponent<Transform>();
            animator = GetComponentInChildren<Animator>();
        }
        private void Update()
        {
            if (!GameManager.GetHasGameStarted()) return;
            Shoot();
        }
        #endregion

        #region Getters/Setters
        public CharacterData GetCharacterData() => characterData;
        public void SetCharacterData(CharacterData newData) => characterData = newData;

        public ProjectileData GetProjectileData() => projectileData;
        public void SetProjectileData(ProjectileData newData) => projectileData = newData;

        public List<ProjectileBehavior> GetProjectilesList() => projectiles;
        #endregion
        void Shoot()
        {
            //always update shot counter
            if (shotCounter >= characterData.GetMaxShots())
                shotCounter = 0;

            OnButtonPress();
        }

        private void OnButtonPress()
        {
            if (!GameManager.isAndroid)
            {
                //on shoot button instantiate and add to list
                if (Input.GetKeyDown(shootKey))
                    ShootHandler();
            }
        }
        public void ShootHandler()
        {
            if (instantiateCounter < characterData.GetMaxShots())
            {
                instantiateCounter++;
                projectiles.Add(InstantiateShot(projPrefab));

                if (clip != null)
                    AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

                animator.SetTrigger("Shoot");
            }
            //if max shots is > list loop through list (dont instantiate anymore)
            else
            {
                ProjectileBehavior currentShot = projectiles[shotCounter];
                shotCounter++;
                if (!currentShot.isOn && !currentShot.isLocked)
                {
                    if (clip != null)
                        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

                    animator.SetTrigger("Shoot");
                    currentShot.transform.position = new Vector2(myTransform.position.x, myTransform.position.y + 1.3f);
                    currentShot.gameObject.SetActive(true);
                    currentShot.isOn = true;
                }

            }
        }
        private ProjectileBehavior InstantiateShot(GameObject shotPrefab)
        {
            GameObject projectile = Instantiate(shotPrefab, projectileTransform);
            projectile.transform.GetChild(0).gameObject.SetActive(true);

            //if we have a projectile without a body we can easily modify this portion
            ProjectileBodyController projectileBody = projectile.GetComponentInChildren<ProjectileBodyController>();
            projectileBody.transform.position = new Vector2(transform.position.x, 0f); ;

            ProjectileBehavior projectileHead = projectile.GetComponentInChildren<ProjectileBehavior>();
            projectileHead.transform.position = new Vector2(myTransform.position.x, myTransform.position.y + 1.3f);
            projectileHead.SetPlayer(this);
            projectileHead.isOn = true;

            return projectileHead;
        }
        public void ResetShot(int index)
        {
            ProjectileBehavior currentShot = projectiles[index];
            currentShot.isOn = false;
            currentShot.transform.position = new Vector2(myTransform.position.x, myTransform.position.y + 1.3f);
            foreach (Collider2D col in currentShot.childrenCols)
            {
                col.gameObject.SetActive(false);
            }
            currentShot.gameObject.SetActive(false);
        }
    }
}