using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [Header("プレイヤーの移動速度")]
    [SerializeField] private float moveSpeed = 5f;

    private Vector2 moveInput;

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// 移動入力処理
    /// </summary>
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        // WASDや左スティックの入力値を取得
        moveInput = context.ReadValue<Vector2>();
    }
    /// <summary>
    /// 移動処理
    /// </summary>
    private void Move()
    {
        // Vector2をVector3へ変換
        Vector3 move = new Vector3(moveInput.x, 0f, moveInput.y);
        // プレイヤーを移動
        this.transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
    }
}