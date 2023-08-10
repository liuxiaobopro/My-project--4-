using System;
using define.enums;
using Sundry;
using UnityEngine;

namespace Character.Player
{
    public class PlayerController : Singleton<PlayerController>
    {
        [Tooltip("玩家移动速度")] [SerializeField] private float moveSpeed = 5f;

        private PlayerControls playerControls; // 玩家控制器
        private Vector2 movement; // 玩家移动输入
        private Rigidbody2D rb; // 玩家刚体
        private PlayerDirection playerDirection; // 玩家方向
        private Animator animator; // 玩家动画控制器
        private Camera mainCamera; // 主摄像机
        private SpriteRenderer spriteRenderer; // 玩家精灵渲染器

        private static readonly int AnimatorDirection = Animator.StringToHash("playerDirection");


        protected override void Awake()
        {
            base.Awake();

            rb = GetComponent<Rigidbody2D>();
            playerControls = new PlayerControls();
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
            mainCamera = Camera.main;
            spriteRenderer = GetComponent<SpriteRenderer>();
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
            PlayerInput();
            SetPlayerDirection();
            SetPlayerAnimator();
        }

        private void FixedUpdate()
        {
            Move();
        }

        // 设置玩家朝向
        public void SetPlayerDirection()
        {
            Vector3 mousePosition = Input.mousePosition; // 获取鼠标位置
            Vector3 playerScreenPosition = mainCamera.WorldToScreenPoint(transform.position); // 获取玩家在屏幕上的位置

            // 计算玩家朝向
            if (mousePosition.x < playerScreenPosition.x)
            {
                spriteRenderer.flipX = true;
            }
            else if (mousePosition.x > playerScreenPosition.x)
            {
                spriteRenderer.flipX = false;
            }
        }

        /// <summary>
        /// 玩家移动
        /// </summary>
        private void PlayerInput()
        {
            movement = playerControls.Movement.Move.ReadValue<Vector2>(); // 获取玩家移动输入
            if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
            {
                playerDirection = PlayerDirection.Idle;
            }
            else
            {
                if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
                {
                    playerDirection = movement.x > 0 ? PlayerDirection.Right : PlayerDirection.Left;
                }
                else
                {
                    playerDirection = movement.y > 0 ? PlayerDirection.Up : PlayerDirection.Down;
                }
            }
        }

        /// <summary>
        /// 设置玩家动画
        /// </summary>
        private void SetPlayerAnimator()
        {
            animator.SetInteger(AnimatorDirection, (int)playerDirection);
        }

        /// <summary>
        /// 玩家移动
        /// </summary>
        private void Move()
        {
            rb.MovePosition(rb.position + movement * (moveSpeed * Time.fixedDeltaTime)); // 移动玩家
        }
    }
}