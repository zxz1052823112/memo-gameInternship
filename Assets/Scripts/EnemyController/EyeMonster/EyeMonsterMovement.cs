using UnityEngine;
using DG.Tweening;

public class EyeMonsterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float stopDistance;
    [SerializeField] private float resumeDistance;
    [SerializeField] private GameObject bulletPrefab; // 子弹预制体
    [SerializeField] private Transform firePoint; // 子弹发射点
    [SerializeField] private float fireRate = 1f; // 子弹发射频率

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

        // 计算子弹的方向
        Vector2 fireDirection = (targetPosition - (Vector2)firePoint.position).normalized;
        //Debug.Log("targetPosition");
        //Debug.Log(fireDirection);


        // 设置子弹的速度沿着正确方向
        Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
        bulletRb.velocity = fireDirection * speed; // 确保速度是沿着方向向量的

        // 旋转子弹使其朝向目标
        float angle = Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
