using System.Collections;
using System.Collections.Generic;
using Timers;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    [SerializeField] private string targetTag;
    private int damage;
    private bool _canAttack = true;

    // Allow other scripts to set damage
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
        Debug.Log("BulletAttack damage set to: " + damage);
    }


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

            // Check if Damageable component exists
            if (damageable != null)
            {
                damageable.TakeDamage(damage);  // Use the updated damage value
                //Debug.Log(damage);
                TimersManager.SetTimer(this, 2, canAttack);
                _canAttack = false;
            }
            else
            {
                Debug.LogWarning("Collision object missing Damageable component");
            }
        }
    }

    private void canAttack()
    {
        _canAttack = true;
    }
}
