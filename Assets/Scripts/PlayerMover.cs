using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    public void Move(float inputDirection)
    {
        Vector3 moveVector = Vector3.zero;

        if (inputDirection < 0)
        {
            moveVector = new Vector3(-1, 0, 0) * _moveSpeed;
        }
        if (inputDirection > 0)
        {
            moveVector = new Vector3(1, 0, 0) * _moveSpeed;
        }

        transform.Translate(moveVector * Time.deltaTime);
    }
}
