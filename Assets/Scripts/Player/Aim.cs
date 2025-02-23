using UnityEngine;

public class Aim : MonoBehaviour
{
    public Vector3 CurrentPoint { get; private set; }

    private Vector2 _mousePosition;

    private void Update()
    {
        CurrentPoint = GetWorldPosition();
    }

    public void SetMousePosition(Vector2 mousePosition)
    {
        _mousePosition = mousePosition;
    }

    private Vector3 GetWorldPosition()
    {
        Vector3 screenPosition = new Vector3(_mousePosition.x, _mousePosition.y, -Camera.main.transform.position.z);
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        return worldPosition;
    }
}
