using ComunixTest.Model;
using UnityEngine;

namespace ComunixTest.Controller
{
    public class Player : MonoBehaviour
    {
        Animator animator;
        [SerializeField] AudioClip clip;
        [SerializeField] ParticleSystem hitFx;
        
        void Start()
        {
            animator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Balls"))
            {
                CharacterData data = GetComponentInParent<PlayerShooting>().GetCharacterData();
                
                //if hit by ball return
                if (data.GetIsHit()) return;

                //reduce lives by 1 if hit
                int currentLives = data.GetLives();
                data.SetLives(currentLives - 1);

                //play sfx
                if(clip != null)
                    AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);

                //play vfx
                if (hitFx != null)
                    hitFx.Play();

                //play death animation
                animator.SetTrigger("Dead");
                data.SetIsHit(true);
            }
        }
    }
}