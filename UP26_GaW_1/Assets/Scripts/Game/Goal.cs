using UnityEngine;

public class Goal : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject goalPanel;

    private bool isGoal;

    private void Start()
    {
        if (goalPanel == null)
            return;

        goalPanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isGoal) return;

        if (other.CompareTag("Player"))
        {
            isGoal = true;

            goalPanel.SetActive(true);

            // ŽžŠÔ’âŽ~
            Time.timeScale = 0f;
        }
    }
}