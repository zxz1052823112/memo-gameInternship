using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Death : MonoBehaviour
{
    [SerializeField] private UnityEvent died;

    public int expToGive = 1;

    public void CheckDeath(int health)
    {
        if (health <= 0)
        {
            Die();
            ExperienceLevelController.instance.SpawnExp(transform.position, expToGive);
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
        died.Invoke();
    }
}
