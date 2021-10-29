using ComunixTest.Model;
using UnityEngine;

namespace ComunixTest.Controller
{
    public class BallMovement : MonoBehaviour
    {
        [SerializeField] BallData ballData;

        Vector3 rotateDir;

        bool isGoingRight = true;

        Rigidbody2D rb;

        #region Unity Methods
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();

            rotateDir = new Vector3(0f, 0f, Mathf.Sign(Random.Range(-10, 10)));

            if (GameManager.GetHasGameStarted())
            {
                rb.AddForce(Vector2.up * ballData.GetUpwardsForce());
            }
        }
        void Update()
        {
            if (!GameManager.GetHasGameStarted()) return;

            HandleStart();
            MoveHorizontally();
            LimitBounceHeight();

            //rotate balls
            transform.Rotate(rotateDir * 30f * Time.deltaTime);
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Walls"))
                isGoingRight = !isGoingRight;
        }
        #endregion

        #region Getters/Setters
        public bool GetIsGoingRight() => isGoingRight;
        public void SetIsGoingRight(bool isRight) => isGoingRight = isRight;

        public BallData GetBallData() => ballData;
        #endregion

        #region Methods

        void HandleStart()
        {
            if (!GameManager.GetHasGameStarted())
            {
                rb.simulated = false;
                return;
            }
            else rb.simulated = true;
        }
        public void TranslateDirection(Vector3 dir, float speed)
        {
            transform.Translate(dir * (speed * Time.deltaTime), Space.World);
        }

        void MoveHorizontally()
        {
            if (isGoingRight)
                TranslateDirection(Vector3.right, ballData.GetMoveSpeed());
            else TranslateDirection(Vector3.left, ballData.GetMoveSpeed());
        }

        void LimitBounceHeight()
        {
            float y = Mathf.Clamp(rb.velocity.y, -ballData.GetMaxVelocity(), ballData.GetMaxVelocity());

            rb.velocity = new Vector2(rb.velocity.x, y);
        }
        #endregion
    }
}