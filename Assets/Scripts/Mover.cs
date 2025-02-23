using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpPower;

    private bool _onGround;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        LayerMask layerMask = ~LayerMask.GetMask("Player", "Ignore Raycast");
        _onGround = Physics2D.Raycast(transform.position, transform.up * -1, 0.7f, layerMask);
    }

    public void Move(float inputDirection)
    {
        float horizontalMove = 0;
        if (inputDirection < 0)
        {
            horizontalMove = -1;
        }
        if (inputDirection > 0)
        {
            horizontalMove = 1;
        }

        Vector2 moveVector = new Vector2(horizontalMove * _moveSpeed, 0);
        transform.position = (_rigidbody.position + moveVector * Time.deltaTime);
    }

    public void Jump()
    {
        if (_onGround)
        {
            Vector2 jumpVector = new(0, _jumpPower);
             _rigidbody.AddForce(jumpVector, ForceMode2D.Impulse);
            _onGround = false;
        }
        Debug.Log("Jump");
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (transform.up * -1) * 0.7f);
    }
}
