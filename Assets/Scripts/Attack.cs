using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Timers;

public class Attack : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private int damage;

    private bool _canAttack = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DealDamage(collision);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        DealDamage(other);
    }

    private void DealDamage(Collider2D other)
    {
        if (!_canAttack) return;

        if (other.CompareTag(targetTag))
        {
            var damageable = other.GetComponent<Damageable>();

            // 检查是否成功获取到 Damageable 组件
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                TimersManager.SetTimer(this, 2, canAttack);
                _canAttack = false;
            }
            else
            {
                Debug.LogWarning("碰撞对象缺少 Damageable 组件");
            }
        }
    }

    private void canAttack()
    {
        _canAttack = true;
    }
}
