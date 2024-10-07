using System.Collections;
using UnityEngine;

public class AbbySkillWithAnimation : MonoBehaviour
{
    public Animator animator;                // ���ڲ�����⶯����Animator���
    public Gun gunScript;                    // Gun�ű��������ӵ�����ͻ���
    private int totalBullets;                // ���ʱ���ӵ����������Է�����ٿ��ӵ���
    public float fireRate = 0.1f;            // ÿ���ӵ�֮���ʱ����
    public Transform firePoint;              // �ӵ�����λ��
    public float bulletSpeed = 20f;          // �ӵ������ٶ�

    private bool isReloading = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isReloading)  // �Ҽ����²��Ҳ��ڻ�����
        {
            if (gunScript.GetCurrentAmmo() > 0)           // ֻ�е����ӵ�ʱ���ܴ������
            {
                PlayBulletRainAnimation();
                StartCoroutine(FireAllBullets());
            }
            else
            {
                Debug.Log("û���㹻���ӵ�������к���뻻����");
            }
        }
        // �Ƴ��� else �飬��ֹ��������������������
    }

    // ������⶯��
    void PlayBulletRainAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("BulletRain");  // ���趯������������һ����Ϊ"BulletRain"�Ĵ�����
        }
    }

    // ������������
    public void OnBulletRainAnimationEnd()
    {
        if (animator != null)
        {
            animator.ResetTrigger("BulletRain"); // ���ô�������ȷ����һ���ܹ���ȷ����
        }
        // ������Ҫ�ڶ���������ִ�еĲ���
    }

    // ��ʼ���������ӵ�
    IEnumerator FireAllBullets()
    {
        int bulletsToFire = gunScript.GetCurrentAmmo(); // ȷ��������ӵ����ᳬ����ǰʣ���ӵ���
        float angleStep = 360f / bulletsToFire;         // ����ÿ���ӵ�֮��ĽǶȼ��

        for (int i = 0; i < bulletsToFire; i++)
        {
            // ���㷢��ǶȲ������ӵ�
            float angle = i * angleStep;
            FireBulletInDirection(angle);
            gunScript.DecreaseAmmo();  // ÿ�η���һ���ӵ������Gun�е�currentAmmo
            yield return new WaitForSeconds(fireRate);  // �����ӵ������ʱ����
        }

        // �����ӵ�������Ϻ󣬵��ö�����������
        //OnBulletRainAnimationEnd();

        if (gunScript.GetCurrentAmmo() <= 0 && Input.GetKeyDown(KeyCode.E))  // ��������������ӵ����ӵ��ľ�
        {
            StartCoroutine(gunScript.Reload());  // ����Gun�ű��еĻ�������
        }
    }

    // ���ݽǶȷ����ӵ�
    void FireBulletInDirection(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(gunScript.bullet, firePoint.position, rotation);
        // �����ֶ������ٶȣ�Bullet �ű��������ת������������
    }
}
