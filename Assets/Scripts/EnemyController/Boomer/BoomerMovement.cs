using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed; // 自爆怪移动速度
    [SerializeField] private float explosionRadius; // 爆炸半径
    [SerializeField] private int damage; // 爆炸伤害
    [SerializeField] private float detonationDistance; // 自爆触发的距离
    [SerializeField] private float countdownTime; // 自爆倒计时

    private bool isDetonating = false; // 自爆是否已触发

    private void FixedUpdate()
    {
        if (!isDetonating)
        {
            MoveTowardsPlayer();
        }
        CheckDetonation();
    }

    // 不断向玩家移动
    private void MoveTowardsPlayer()
    {
        var playerPosition = PlayerManager.Position; // 假设有PlayerManager来获取玩家位置
        var position = (Vector2)transform.position;
        var direction = (playerPosition - position).normalized;

        // 设置怪物朝向
        rb.velocity = direction * moveSpeed;

        if (direction.x < 0)
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        else
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
    }

    // 检查是否触发自爆
    private void CheckDetonation()
    {
        var playerPosition = PlayerManager.Position;
        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition);

        if (distanceToPlayer <= detonationDistance && !isDetonating)
        {
            StartCoroutine(Detonate()); // 开始自爆倒计时
        }
    }

    // 自爆倒计时与爆炸
    private IEnumerator Detonate()
    {
        isDetonating = true;
        rb.velocity = Vector2.zero; // 停止移动

        //// 可以在此添加自爆动画或特效
        //Debug.Log("自爆怪开始倒计时...");
        yield return new WaitForSeconds(countdownTime);

        // 触发爆炸
        Explode();
    }

    // 爆炸逻辑
    private void Explode()
    {
        //Debug.Log("自爆怪爆炸了！");

        // 在爆炸半径内查找所有碰撞体
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D hit in hitColliders)
        {
            if (hit.CompareTag("Player") || hit.CompareTag("Enemy"))
            {
                // 获取 Damageable 组件
                var damageable = hit.GetComponent<Damageable>();
                if (damageable != null)
                {
                    // 给目标造成伤害
                    damageable.TakeDamage(damage);
                }
            }
        }

        // 爆炸后销毁自身
        Destroy(gameObject);
    }

    // 在编辑器中显示爆炸范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
