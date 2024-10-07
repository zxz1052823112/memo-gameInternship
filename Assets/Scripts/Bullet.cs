using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private string targetTag;

    private float damage = 1;
    public float destroyDistance;

    private Rigidbody2D rb;
    private Vector3 startPos;

    // Reference to the Attack script
    private BulletAttack attackScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        startPos = transform.position;

        // Find and set the Attack script if it's on the same GameObject
        attackScript = GetComponent<BulletAttack>();

        // If there's an attack script, update its damage value
        //if (attackScript != null)
        //{
        //    attackScript.SetDamage(Mathf.CeilToInt(damage));
        //}
        // 使用当前等级来设置初始伤害
        UpdateDamage(Mathf.Min(ExperienceLevelController.instance.currentLevel, ExperienceLevelController.instance.maxDamageIncrease));
    }

    // 更新伤害值
    public void UpdateDamage(int levelDamage)
    {
        // 更新 damage 为当前等级
        damage = levelDamage;

        Debug.Log("Bullet damage updated to: " + damage);

        if (attackScript != null)
        {
            attackScript.SetDamage(Mathf.CeilToInt(damage));
        }
    }

    void Update()
    {
        float distance = (transform.position - startPos).sqrMagnitude;
        if (distance > destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            Destroy(gameObject);
        }
    }
}
