using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Gマネ")]
    [SerializeField] private GameManager gameManager;

    [Header("移動")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float maxSpeed = 6f;
    [Header("ジャンプ")]
    [SerializeField] private float jumpForce = 7f;

    [Header("Respawn")]
    [SerializeField] private Transform respawnPoint;

    [Header("Fall")]
    [SerializeField] private float fallY = -3f;

    [Header("Ground Check")]
    [SerializeField] private float groundDistance = 0.3f;
    [SerializeField] private LayerMask groundMask;

    [Header("SE")]
    [SerializeField] private AudioClip landSE;
    [SerializeField] private float landSEVol = 0.7f;

    private AudioSource audioSource;


    private bool isGrounded;
    private bool wasGrounded;
    private bool jumpLock;


    private Rigidbody rb;

    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameManager.IsGameOver)
            return;

        CheckGround();
        CheckFall();
    }

    void FixedUpdate()
    {
        if (gameManager.IsGameOver)
            return;

        Move();
    }

    #region リスポーン
    /// <summary>
    /// リスポーンエンターテインメント
    /// </summary>
    private void CheckFall()
    {
        if (this.transform.position.y < fallY)
        {
            Respawn();
        }
    }

    /// <summary>
    /// 復活
    /// </summary>
    private void Respawn()
    {
        // 速度リセット
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        // 座標戻す
        this.transform.position = respawnPoint.position;
    }

    /// <summary>
    /// リス地更新
    /// </summary>
    public void SetRespawnPoint(Transform newPoint)
    {
        respawnPoint = newPoint;
    }
    #endregion

    #region Move - 移動 
    /// <summary>
    /// 移動入力処理
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
    /// <summary>
    /// 移動処理
    /// </summary>
    void Move()
    {
        // 入力方向取得
        Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);
        // 物理移動
        rb.AddForce(direction * moveForce);

        // 速度制限
        Vector3 velocity = rb.linearVelocity;
        // 前後左右移動のベクトル
        Vector3 horizontalVelocity = new Vector3(velocity.x, 0, velocity.z);

        if (horizontalVelocity.magnitude > maxSpeed)
        {
            // 縦方向は制限なし
            Vector3 limited = horizontalVelocity.normalized * maxSpeed;
            rb.linearVelocity = new Vector3(limited.x, velocity.y, limited.z);
        }
    }
    #endregion


    #region Jump - ジャンプ 
    /// <summary>
    /// 週刊少年ジャンプ入力処理
    /// </summary>
    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Jump();
    }
    /// <summary>
    /// 週刊少年ジャンプ処理
    /// </summary>
    void Jump()
    {
        if (!isGrounded) return;

        isGrounded = false;
        jumpLock = true;
        // 上昇
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    /// <summary>
    /// 接地処理
    /// </summary>
    private void CheckGround()
    {
        if (jumpLock)
        {
            // 少し空中猶予
            if (rb.linearVelocity.y < 0)
            {
                jumpLock = false;
            }
            else
            {
                return;
            }
        }

        wasGrounded = isGrounded;
        isGrounded = Physics.Raycast(this.transform.position, Vector3.down, groundDistance, groundMask);

        // 着地した瞬間
        if (!wasGrounded && isGrounded)
        {
            PlayLandSE();
        }
    }

    /// <summary>
    /// 着地SE再生
    /// </summary>
    private void PlayLandSE()
    {
        if (audioSource == null || landSE == null)
            return;

        audioSource.PlayOneShot(landSE, landSEVol);
    }
    #endregion


}