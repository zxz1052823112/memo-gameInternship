using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainMonsterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    //[SerializeField] private PlayerManager playerManager;

    private void FixedUpdate()
    {
        var playerPosition = PlayerManager.Position;
        var position = (Vector2)transform.position;
        var direction = playerPosition - position;
        direction.Normalize();
        var targetPosition = position + direction;
        rb.DOMove(targetPosition, speed).SetSpeedBased();
        if (direction.x < 0)
            transform.eulerAngles = new Vector3(x: 0f, y: 180f, z: 0f);
        else
            transform.eulerAngles = new Vector3(x: 0f, y: 0f, z: 0f);
    }
}
