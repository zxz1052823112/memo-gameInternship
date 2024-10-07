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

            // ����Ƿ�ɹ���ȡ�� Damageable ���
            if (damageable != null)
            {
                damageable.TakeDamage(damage);
                TimersManager.SetTimer(this, 2, canAttack);
                _canAttack = false;
            }
            else
            {
                Debug.LogWarning("��ײ����ȱ�� Damageable ���");
            }
        }
    }

    private void canAttack()
    {
        _canAttack = true;
    }
}
