using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private bool isActivated = false;

    private void OnTriggerEnter(Collider other)
    {
        // 一回だけ発動
        if (isActivated)
            return;

        // Player判定
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();

            if (player != null)
            {
                player.SetRespawnPoint(this.transform);

                isActivated = true;

                Debug.Log("チェックポイント更新");
            }
        }
    }
}