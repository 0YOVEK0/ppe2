
using UnityEngine;
using GoSystem.Control;
using UnityEngine.UI;
namespace GoSystem
{
    [GBehaviourAttributeAttribute("Character Controller", true)]
    public class GoCharacterController : GoSystemsBehaviour
    {
        public GoSystems Gs = new GoSystems();
        [HideInInspector] public int Index;
        [Header("Idel Settings")]
        public float MoveSpeed = 7f;
        public float SprintRunSpeed = 10f;
        public KeyCode SprintRun = KeyCode.LeftShift;
        [Space(10)]
        [Header("Crouch Settings")]
        public float CrouchSpeed = 3;
        public KeyCode CrouchKey = KeyCode.LeftControl;
        [Space(10)]
        [Header("Jump Settings")]
        public KeyCode JumpKey = KeyCode.Space;
        public LayerMask layerJump;
        public float jumpForce = 5f;
        public float JumpHigh = 5;
        public float TimerJump;
        public LayerMask LayerWall;
        [Space(10)]
        [Range(0, 1)]
        [Header("Ik Foot Settings")]
        public float IKfootWeight;
        public Vector3 offsetFoodPosition;
        public LayerMask IKlayer;
        [Space(10)]
        private float _speed;
        [HideInInspector] public bool IsGround;
        [HideInInspector] public bool lockcode;
        [HideInInspector] public bool lockjump;
        public Vector3 offsetLLeg;
        public Vector3 offsetRLeg;
        [Space(10)]
        [Header("UI Settings")]
        public Slider SprintBar;
        public float TimeSprintRun = 3f;
        private void Awake()
        {
            GoSystems.Player = this;
            GoSystemsController.Getbone();
            _speed = MoveSpeed;
            Cursor.lockState = CursorLockMode.Locked;
            if (SprintBar == null) return;
                SprintBar.maxValue = TimeSprintRun;
                SprintBar.value = TimeSprintRun;
        }
        void Update()
        {
            GoTrigger.uplay();
            if (!lockcode)
            {
                Gs.MovimentBehaviour(MoveSpeed, SprintRunSpeed, SprintRun);
                if (lockjump == false)
                {
                    IsGround = Gs.IsGraunded(layerJump);
                    //When entering the Jump state in the Animator, output the message in the console
                    Jumping();
                    JumpAvoidance();
                    if (IsGround == false)
                    {
                        GoSystems.IsFoods = false;
                        GoSystems.IsLags = false;

                        GoSystemsController.stop();
                    }
                    else
                    {

                        GoSystemsController.Move();
                        Gs.IsJumping = false;
                    }

                }
                Gs.Crouch(CrouchKey, CrouchSpeed);
            }
        }
        void Jumping()
        {
            if (Input.GetKeyDown(JumpKey))
            {
                CharcterJumping();
            }
        }
        private bool IsCollidingWithObstacles()
        {
            var pos = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
            Collider[] colliders = Physics.OverlapSphere(pos, 0.5f, LayerWall);
            return colliders.Length > 0;
        }
        private void CharcterJumping()
        {
            GoSystems.IsFoods = true;
            GoSystems.IsLags = true;
            Gs.JumpBehaviour(TimerJump, JumpHigh, jumpForce, layerJump);
        }
        private void JumpAvoidance()
        {
            if (IsCollidingWithObstacles() && IsGround == false && Gs.Go_Velocity > 0)
            {
                GoSystemsController.rigidbody.velocity = new Vector3(GoSystemsController.rigidbody.velocity.x, -8, GoSystemsController.rigidbody.velocity.z);
                GoSystemsController.rigidbody.useGravity = false;
            }
            else
            {
                GoSystemsController.rigidbody.useGravity = true;
            }
        }
        private void FixedUpdate()
        {
            GoTrigger.GoEnter();
            GoTrigger.GoExit();
        }
        private void OnAnimatorIK(int layerIndex)
        {
            Gs.FixPosFood(IKfootWeight, offsetFoodPosition, ref IKlayer);
            Gs.FixPosLegs(offsetLLeg, offsetRLeg);
        }
        #region Evints Methods
        public void LockMove()
        {
            Gs.IsLockMove = true;
        }
        public void Unlockcode()
        {
            Gs.IsLockMove = false;
        }
        public void LockJump()
        {
            Gs.IsLockJump = true;
            lockjump = true;
        }
        public void UnlockJump()
        {
            lockjump = false;
            Gs.IsLockJump = false;
        }
        #endregion
        private void OnDrawGizmos()
        {
            Color color;
            var pos = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);
            ColorUtility.TryParseHtmlString("#FF8E28",out color);
            Gizmos.color = color;
            Gizmos.DrawWireSphere(pos, 0.8f);
        }
    }
}