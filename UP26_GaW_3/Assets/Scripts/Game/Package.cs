using UnityEngine;

public class Package : MonoBehaviour
{
    [Header("á‚Í‰½F")]
    [SerializeField] private ColorType colorType;

    #region Get
    public ColorType ColorType => colorType;
    #endregion
}