using System.Collections.Generic;
using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;

    private void Awake()
    {
        instance = this;
    }

    public int currentExperience;
    public ExperiencePickup pickup;

    public List<int> expLevels;
    public int currentLevel = 1, levelCount = 100;

    // 最大 damage 限制
    public int maxDamageIncrease = 26;

    // 引用 Health 组件
    private Health playerHealth;
    private int initialHealth;

    void Start()
    {
        // 获取玩家的 Health 组件并保存初始生命值
        playerHealth = FindObjectOfType<Health>();
        if (playerHealth != null)
        {
            initialHealth = playerHealth.Value;
        }

        // 初始化经验等级列表，假设第一级经验为100
        if (expLevels.Count == 0)
        {
            expLevels.Add(100); // 初始化第一等级所需经验
        }

        // 按照递增比例填充经验等级列表
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;

        // 使用 currentLevel 来确定当前等级所需的经验
        if (currentExperience >= expLevels[currentLevel])
        {
            LevelUp();
        }
        UIController.Instance.UpdateExperience(currentExperience, expLevels[currentLevel], currentLevel);
    }

    public void SpawnExp(Vector3 position, int expValue)
    {
        Instantiate(pickup, position, Quaternion.identity).expValue = expValue;
    }

    void LevelUp()
    {
        currentExperience -= expLevels[currentLevel];
        currentLevel++;

        Debug.Log("Level Up! Current Level: " + currentLevel);

        if (currentLevel >= expLevels.Count)
        {
            currentLevel = expLevels.Count - 1;
        }

        // 调用更新所有子弹伤害的方法
        Debug.Log("Calling UpdateAllBulletsDamage");
        UpdateAllBulletsDamage();

        // 恢复生命值到初始值
        if (playerHealth != null)
        {
            playerHealth.DecreaseHealth(-initialHealth); // 恢复生命值到初始值
            Debug.Log("Health restored to: " + initialHealth);
        }
    }

    void UpdateAllBulletsDamage()
    {
        Bullet[] bullets = FindObjectsOfType<Bullet>();
        foreach (var bullet in bullets)
        {
            Debug.Log("Updating Bullet damage, current level: " + currentLevel);
            bullet.UpdateDamage(Mathf.Min(currentLevel, maxDamageIncrease));
        }
    }
}
