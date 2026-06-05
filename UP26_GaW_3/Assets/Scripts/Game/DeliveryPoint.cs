using UnityEngine;

public class DeliveryPoint : MonoBehaviour
{
    [Header("á‚Í‰½F")]
    [SerializeField] private ColorType colorType;

    #region Get
    public ColorType ColorType => colorType;
    #endregion
}