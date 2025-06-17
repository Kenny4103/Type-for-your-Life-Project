using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private Vector2 _movement;
    private Rigidbody2D _rb;

    public bool canMove = true; // Add this flag

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!canMove)
        {
            _rb.linearVelocity = Vector2.zero;
            return;
        }

        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        _rb.linearVelocity = _movement * _moveSpeed;
    }
}
