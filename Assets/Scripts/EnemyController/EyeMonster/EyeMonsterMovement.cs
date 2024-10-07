using UnityEngine;
using DG.Tweening;

public class EyeMonsterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float stopDistance;
    [SerializeField] private float resumeDistance;
    [SerializeField] private GameObject bulletPrefab; // �ӵ�Ԥ����
    [SerializeField] private Transform firePoint; // �ӵ������
    [SerializeField] private float fireRate = 1f; // �ӵ�����Ƶ��

    private bool isAttacking = false;
    private float nextFireTime = 0f;

    private void FixedUpdate()
    {
        var playerPosition = PlayerManager.Position;
        var position = (Vector2)transform.position;
        var direction = playerPosition - position;
        float distanceToPlayer = direction.magnitude;

        if (distanceToPlayer > resumeDistance)
        {
            MoveTowardsPlayer(direction, position);
        }
        else if (distanceToPlayer <= stopDistance)
        {
            if (!isAttacking)
            {
                isAttacking = true;
                rb.velocity = Vector2.zero;
            }
            AttackPlayer(playerPosition);
        }
        else if (distanceToPlayer > stopDistance && distanceToPlayer <= resumeDistance)
        {
            isAttacking = false;
            MoveTowardsPlayer(direction, position);
        }
    }

    private void MoveTowardsPlayer(Vector2 direction, Vector2 position)
    {
        direction.Normalize();
        var targetPosition = position + direction;
        rb.DOMove(targetPosition, speed).SetSpeedBased();

        if (direction.x < 0)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        else
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    private void AttackPlayer(Vector2 playerPosition)
    {
        if (Time.time >= nextFireTime)
        {
            FireBullet(playerPosition);
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    private void FireBullet(Vector2 targetPosition)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // �����ӵ��ķ���
        Vector2 fireDirection = (targetPosition - (Vector2)firePoint.position).normalized;
        //Debug.Log("targetPosition");
        //Debug.Log(fireDirection);


        // �����ӵ����ٶ�������ȷ����
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = fireDirection * speed; // ȷ���ٶ������ŷ���������

        // ��ת�ӵ�ʹ�䳯��Ŀ��
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
