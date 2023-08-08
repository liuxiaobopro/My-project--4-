using Sundry;
using UnityEngine;

namespace Character.Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        [Tooltip("玩家移动速度")] [SerializeField] private float moveSpeed = 5f;

        private PlayerControls playerControls; // 玩家控制器
        private Vector2 movementInput; // 玩家移动输入
        private Rigidbody2D rb; // 玩家刚体

        protected override void Awake()
        {
            base.Awake();

            rb = GetComponent<Rigidbody2D>();
            playerControls = new PlayerControls();
        }

        private void OnEnable()
        {
            playerControls.Enable();
        }

        private void OnDisable()
        {
            playerControls.Disable();
        }

        private void Update()
        {
            playerInput();
        }

        private void FixedUpdate()
        {
            Move();
        }

        /// <summary>
        /// 玩家移动
        /// </summary>
        private void playerInput()
        {
            // 获取玩家输入
            movementInput = playerControls.Movement.Move.ReadValue<Vector2>();
        }

        /// <summary>
        /// 玩家移动
        /// </summary>
        private void Move()
        {
            rb.MovePosition(rb.position + movementInput * (moveSpeed * Time.fixedDeltaTime));
        }
    }
}