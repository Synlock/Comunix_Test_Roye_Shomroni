using ComunixTest.Model;
using UnityEngine;

namespace ComunixTest.Controller
{
    //considered having this as a super class and inheriting from this for player 1 and 2 but
    //i just made the key presses available in the inspector
    public class PlayerMovement : MonoBehaviour
    {
        Transform playerTransform;
        [SerializeField] Model.CharacterData myData;
        bool isMovingRight = false;

        static int xDir = 0;
        static bool isButtonPressed = false;

        [SerializeField] KeyCode left;
        [SerializeField] KeyCode right;

        Animator animator;

        #region Unity Methods
        private void Start()
        {
            playerTransform = GetComponent<Transform>();
            animator = GetComponentInChildren<Animator>();
        }
        private void Update()
        {
            if (!Model.GameManager.GetHasGameStarted() || myData.GetIsHit()) return;

            if (GameManager.isAndroid)
                MobileMove(xDir);
            else KeyboardMove();

            MoveBoundries();
        }
        #endregion
        #region Getters / Setters
        public static void SetXDir(int newXDir) => xDir = newXDir;

        public static void SetIsButtonPressed(bool isPressed) => isButtonPressed = isPressed;
        #endregion
        #region My Methods
        public void MobileMove(int xDir)
        {
            if (!isButtonPressed)
            {
                animator.SetBool("isWalking", false);
                return;
            }
            xDir = Mathf.Clamp(xDir, -1, 1);
            CheckMovingRight();

            playerTransform.Translate(new Vector2(xDir, 0) * (myData.GetMoveSpeed() * Time.deltaTime), Space.World);
        }
        public void KeyboardMove()
        {
            if (Input.GetKey(left))
            {
                animator.SetBool("isWalking", true);
                playerTransform.Translate(Vector3.left * (myData.GetMoveSpeed() * Time.deltaTime), Space.World);
                playerTransform.localScale = new Vector3(-1, playerTransform.localScale.y, playerTransform.localScale.z);
            }
            else if (Input.GetKey(right))
            {
                animator.SetBool("isWalking", true);
                playerTransform.Translate(Vector3.right * (myData.GetMoveSpeed() * Time.deltaTime), Space.World);
                playerTransform.localScale = new Vector3(1, playerTransform.localScale.y, playerTransform.localScale.z);
            }
            else animator.SetBool("isWalking", false);
        }
        void CheckMovingRight()
        {
            animator.SetBool("isWalking", true);

            if (xDir >= 1)
            {
                isMovingRight = true;
                playerTransform.localScale = new Vector3(1, playerTransform.localScale.y, playerTransform.localScale.z);
            }
            else
            {
                isMovingRight = false;
                playerTransform.localScale = new Vector3(-1, playerTransform.localScale.y, playerTransform.localScale.z);
            }
        }
        //limit move boundries based off game manager variables
        void MoveBoundries()
        {
            playerTransform.localPosition = new Vector2(Mathf.Clamp(playerTransform.localPosition.x, GameManager.leftBound, GameManager.rightBound), playerTransform.localPosition.y);
        }
        #endregion
    }
}