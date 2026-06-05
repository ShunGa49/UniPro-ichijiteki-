using UnityEngine;

/// <summary>
/// プレイヤーの接触処理（荷物取得・配達）
/// </summary>
public class PlayerInteraction : MonoBehaviour
{
    private GameManager gameManager;

    [Header("荷物を持つ位置")]
    [SerializeField] private Transform holdPoint;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Package（荷物）に触れたとき
        Package package = other.GetComponent<Package>();

        if (package != null)
        {
            // すでに持っていたら無視
            if (gameManager.CurrentPackageIndex != -1)
                return;

            // 荷物情報をGameManagerに渡す
            gameManager.PickPackage(package.gameObject, (int)package.ColorType);

            // プレイヤーにくっつける
            package.transform.SetParent(holdPoint);
            package.transform.localPosition = Vector3.zero;
        }

        // DeliveryPointに触れたとき
        DeliveryPoint point = other.GetComponent<DeliveryPoint>();

        if (point != null)
        {
            // 配達判定
            gameManager.TryDelivery((int)point.ColorType);
        }
    }
}