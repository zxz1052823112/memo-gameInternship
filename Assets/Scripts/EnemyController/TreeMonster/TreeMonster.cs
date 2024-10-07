using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeMonster : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float resumeDistance;
    [SerializeField] private Animator _animator;

    private void FixedUpdate()
    {
        var playerPosition = PlayerManager.Position;
        var position = (Vector2)transform.position;
        var direction = playerPosition - position;
        float distanceToPlayer = direction.magnitude;

        if (distanceToPlayer < resumeDistance)
        {
            _animator.SetBool(name: "Wake", value: true);
        }
        else
        {
            _animator.SetBool(name: "Wake", value: false);
        }
    }
}
