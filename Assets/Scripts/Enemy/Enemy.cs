using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private float _Damage;

    private Transform _target;
    private float _inputDirection;

    public static event UnityAction<float> TargetReached;

    private void Start()
    {
        Vector3 targetDirection = _target.position - transform.position;
        if (targetDirection.x > 0)
        {
            _inputDirection = 1;
        }
        else if (targetDirection.x < 0)
        {
            _inputDirection = -1;
        }
    }

    private void Update()
    {
        _mover.Move(_inputDirection); 
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Spaceship>(out Spaceship _))
        {
            GetComponent<EnemyHealth>().Death();
            TargetReached?.Invoke(10);
        }
    }
}
