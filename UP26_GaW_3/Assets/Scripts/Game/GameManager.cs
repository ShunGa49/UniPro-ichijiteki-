using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// ゲーム全体管理（スコア・配達判定・ゲーム終了）
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject gameClearPanel;
    [SerializeField] private TextMeshProUGUI remainText;

    private int currentPackageIndex = -1;   // 持っている荷物の色
    private GameObject currentPackage;      // 持っている荷物

    private int remainingPackages;

    private bool isGameClear = false;        // ゲームオーバー判定

    public int CurrentPackageIndex
    {
        get { return currentPackageIndex; }
    }

    void Start()
    {
        // ゲームクリアUI非表示
        gameClearPanel.SetActive(false);
        remainingPackages = FindObjectsByType<Package>(FindObjectsSortMode.None).Length;

        UpdateUI();
    }

    /// <summary>
    /// 荷物を取得したとき
    /// </summary>
    public void PickPackage(GameObject package, int index)
    {
        // 持っている情報を保存するだけ（消さない！）
        currentPackageIndex = index;
        currentPackage = package;
    }

    /// <summary>
    /// 配達処理
    /// </summary>
    /// <summary>
    /// 配達処理
    /// </summary>
    public void TryDelivery(int deliveryIndex)
    {
        // 荷物を持っていないなら何もしない
        if (currentPackageIndex == -1)
            return;

        // 色が一致したら成功
        if (currentPackageIndex == deliveryIndex)
        {
            // 荷物削除
            Destroy(currentPackage);

            // 状態リセット
            currentPackage = null;
            currentPackageIndex = -1;

            remainingPackages--;
            // UI更新
            UpdateUI();

            if (remainingPackages <= 0)
            {
                GameClear();
            }
        }
    }

    /// <summary>
    /// UI更新（スコア・残り数）
    /// </summary>
    private void UpdateUI()
    {
        remainText.text = "REMAIN: " + remainingPackages;
    }

    /// <summary>
    /// ゲームクリア
    /// </summary>
    public void GameClear()
    {
        if (isGameClear)
            return;

        isGameClear = true;

        gameClearPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    #region UIボタン

    public void OnRetryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnTitleButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }

    #endregion
}