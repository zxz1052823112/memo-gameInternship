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

    // ��� damage ����
    public int maxDamageIncrease = 26;

    // ���� Health ���
    private Health playerHealth;
    private int initialHealth;

    void Start()
    {
        // ��ȡ��ҵ� Health ����������ʼ����ֵ
        playerHealth = FindObjectOfType<Health>();
        if (playerHealth != null)
        {
            initialHealth = playerHealth.Value;
        }

        // ��ʼ������ȼ��б������һ������Ϊ100
        if (expLevels.Count == 0)
        {
            expLevels.Add(100); // ��ʼ����һ�ȼ����辭��
        }

        // ���յ���������侭��ȼ��б�
        while (expLevels.Count < levelCount)
        {
            expLevels.Add(Mathf.CeilToInt(expLevels[expLevels.Count - 1] * 1.1f));
        }
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;

        // ʹ�� currentLevel ��ȷ����ǰ�ȼ�����ľ���
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

        // ���ø��������ӵ��˺��ķ���
        Debug.Log("Calling UpdateAllBulletsDamage");
        UpdateAllBulletsDamage();

        // �ָ�����ֵ����ʼֵ
        if (playerHealth != null)
        {
            playerHealth.DecreaseHealth(-initialHealth); // �ָ�����ֵ����ʼֵ
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
