using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("何を追うか")]
    [SerializeField] private Transform target;
    [Header("オフセット")]
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        // カメラ追従
        this.transform.position = this.target.position + offset;
    }
}