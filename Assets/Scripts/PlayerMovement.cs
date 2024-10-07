using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private Rigidbody2D rb;
    private Vector2 _inputDirection;
    [SerializeField] private float speed;

    public void Move(InputAction.CallbackContext context)
    {
        _inputDirection = context.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        var position = (Vector2)transform.position;
        var targetPosition = _inputDirection + position;

        if (position == targetPosition) return;

        rb.DOMove(targetPosition, speed).SetSpeedBased();
    }
}
