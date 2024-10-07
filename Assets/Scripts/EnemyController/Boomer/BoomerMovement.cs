using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed; // �Ա����ƶ��ٶ�
    [SerializeField] private float explosionRadius; // ��ը�뾶
    [SerializeField] private int damage; // ��ը�˺�
    [SerializeField] private float detonationDistance; // �Ա������ľ���
    [SerializeField] private float countdownTime; // �Ա�����ʱ

    private bool isDetonating = false; // �Ա��Ƿ��Ѵ���

    private void FixedUpdate()
    {
        if (!isDetonating)
        {
            MoveTowardsPlayer();
        }
        CheckDetonation();
    }

    // ����������ƶ�
    private void MoveTowardsPlayer()
    {
        var playerPosition = PlayerManager.Position; // ������PlayerManager����ȡ���λ��
        var position = (Vector2)transform.position;
        var direction = (playerPosition - position).normalized;

        // ���ù��ﳯ��
        rb.velocity = direction * moveSpeed;

        if (direction.x < 0)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        else
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    // ����Ƿ񴥷��Ա�
    private void CheckDetonation()
    {
        var playerPosition = PlayerManager.Position;
        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition);

        if (distanceToPlayer <= detonationDistance && !isDetonating)
        {
            StartCoroutine(Detonate()); // ��ʼ�Ա�����ʱ
        }
    }

    // �Ա�����ʱ�뱬ը
    private IEnumerator Detonate()
    {
        isDetonating = true;
        rb.velocity = Vector2.zero; // ֹͣ�ƶ�

        //// �����ڴ�����Ա���������Ч
        //Debug.Log("�Ա��ֿ�ʼ����ʱ...");
        yield return new WaitForSeconds(countdownTime);

        // ������ը
        Explode();
    }

    // ��ը�߼�
    private void Explode()
    {
        //Debug.Log("�Ա��ֱ�ը�ˣ�");

        // �ڱ�ը�뾶�ڲ���������ײ��
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("Player") || hit.CompareTag("Enemy"))
            {
                // ��ȡ Damageable ���
                var damageable = hit.GetComponent<Damageable>();
                if (damageable != null)
                {
                    // ��Ŀ������˺�
                    damageable.TakeDamage(damage);
                }
            }
        }

        // ��ը����������
        Destroy(gameObject);
    }

    // �ڱ༭������ʾ��ը��Χ
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
