using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private float timeLimit = 60f;


    [Header("UI")]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject gameOverPanel;

    private bool isGameOver =false;

    // 外部公開
    public bool IsGameOver => isGameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // はじめは非表示
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        CountDownTimeLimit();
    }

    /// <summary>
    /// 制限時間
    /// </summary>
    void CountDownTimeLimit()
    {
        // ゲームオーバー後は停止
        if (isGameOver)
            return;

        // 時間減少
        timeLimit -= Time.deltaTime;

        // 0未満防止
        if (timeLimit <= 0)
        {
            timeLimit = 0;
            // 時間切れ！
            GameOver();
        }

        // UI更新
        timeText.text = "TIME : " + timeLimit.ToString("F1");
    }



    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    {
        // すでにゲームオーバーなら何もしない
        if (isGameOver)
            return;

        isGameOver = true;

        // ゲームオーバー表示
        gameOverPanel.SetActive(true);

        // ゲーム停止
        Time.timeScale = 0f;
    }

    #region ボタンから呼び出す関数
    // リトライ
    public void OnRetryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // タイトル
    public void OnTitleButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Title");
    }


    #endregion
}
