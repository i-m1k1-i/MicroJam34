using TMPro;
using UnityEngine;

public class AimHand : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Aim _aim;

    private Vector2 _referenceDirection;

    private void Awake()
    {
        _referenceDirection = _hand.right;
    }

    private void Update()
    {
        Vector2 direction = (_aim.CurrentPoint - _hand.position).normalized;

        float angle = Vector2.SignedAngle(_referenceDirection, direction);
        _hand.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
