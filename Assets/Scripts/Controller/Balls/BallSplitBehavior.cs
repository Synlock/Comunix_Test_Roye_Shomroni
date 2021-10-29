using UnityEngine;

namespace ComunixTest.Controller {
    public class BallSplitBehavior : MonoBehaviour
    {
        [SerializeField] GameObject splitInto;

        void SplitController()
        {
            //I was considering creating this feature with object pooling but ultimately decided to implement with instantiate/destroy
            if (splitInto != null)
            {
                // Instantiate two new balls
                GameObject ball1 = Instantiate(splitInto, transform.position, Quaternion.identity, transform.parent);
                GameObject ball2 = Instantiate(splitInto, transform.position, Quaternion.identity, transform.parent);

                //set them to move to opposite directions
                ball1.GetComponent<BallMovement>().SetIsGoingRight(true);
                ball2.GetComponent<BallMovement>().SetIsGoingRight(false);
            }
            //destroy original ball
            Destroy(gameObject, 0.1f);

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                SplitController();

                Transform collisionObjTransform = collision.gameObject.transform;
                ProjectileBodyController projectileBody = collisionObjTransform.parent.gameObject.GetComponent<ProjectileBodyController>();

                AddScoreToPlayer(projectileBody);
                TurnOffProjectile(projectileBody);
            }
        }

        //add score to player
        void AddScoreToPlayer(ProjectileBodyController projectileBody)
        {
            if (projectileBody != null)
            {
                ProjectileBehavior projectile = projectileBody.GetProjectile();
                PlayerShooting player = projectile.GetPlayer();
                Model.CharacterData characterData = player.GetCharacterData();
                int ballScore = GetComponent<BallMovement>().GetBallData().GetScore();
                characterData.SetScore(ballScore + characterData.GetScore());
            }
        }

        //turn off projectile and remove powerup lock
        void TurnOffProjectile(ProjectileBodyController projectileBody)
        {
            if (projectileBody != null)
            {
                projectileBody.LoopThroughChildren(false);
                PlayerShooting player = projectileBody.GetProjectile().GetPlayer();
                int index = player.GetProjectilesList().IndexOf(projectileBody.GetProjectile());
                ProjectileBehavior currentProj = player.GetProjectilesList()[index];
                currentProj.isLocked = false;
                player.ResetShot(player.GetProjectilesList().IndexOf(projectileBody.GetProjectile()));
            }
        }
    }
}