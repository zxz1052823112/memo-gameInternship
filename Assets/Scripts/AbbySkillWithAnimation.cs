using System.Collections;
using UnityEngine;

public class AbbySkillWithAnimation : MonoBehaviour
{
    public Animator animator;                // 用于播放倾斥动画的Animator组件
    public Gun gunScript;                    // Gun脚本，管理子弹发射和换弹
    private int totalBullets;                // 倾斥时总子弹数（最多可以发射多少颗子弹）
    public float fireRate = 0.1f;            // 每发子弹之间的时间间隔
    public Transform firePoint;              // 子弹发射位置
    public float bulletSpeed = 20f;          // 子弹飞行速度

    private bool isReloading = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && !isReloading)  // 右键按下并且不在换弹中
        {
            if (gunScript.GetCurrentAmmo() > 0)           // 只有当有子弹时才能触发倾斥
            {
                PlayBulletRainAnimation();
                StartCoroutine(FireAllBullets());
            }
            else
            {
                Debug.Log("没有足够的子弹进行倾泻，请换弹！");
            }
        }
        // 移除了 else 块，防止动画触发器被过早重置
    }

    // 播放倾斥动画
    void PlayBulletRainAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("BulletRain");  // 假设动画控制器中有一个名为"BulletRain"的触发器
        }
    }

    // 动画结束处理
    public void OnBulletRainAnimationEnd()
    {
        if (animator != null)
        {
            animator.ResetTrigger("BulletRain"); // 重置触发器，确保下一次能够正确触发
        }
        // 其他需要在动画结束后执行的操作
    }

    // 开始发射所有子弹
    IEnumerator FireAllBullets()
    {
        int bulletsToFire = gunScript.GetCurrentAmmo(); // 确保发射的子弹不会超过当前剩余子弹数
        float angleStep = 360f / bulletsToFire;         // 计算每颗子弹之间的角度间隔

        for (int i = 0; i < bulletsToFire; i++)
        {
            // 计算发射角度并发射子弹
            float angle = i * angleStep;
            FireBulletInDirection(angle);
            gunScript.DecreaseAmmo();  // 每次发射一颗子弹后减少Gun中的currentAmmo
            yield return new WaitForSeconds(fireRate);  // 控制子弹发射的时间间隔
        }

        // 所有子弹发射完毕后，调用动画结束处理
        //OnBulletRainAnimationEnd();

        if (gunScript.GetCurrentAmmo() <= 0 && Input.GetKeyDown(KeyCode.E))  // 如果发射完所有子弹后，子弹耗尽
        {
            StartCoroutine(gunScript.Reload());  // 调用Gun脚本中的换弹功能
        }
    }

    // 根据角度发射子弹
    void FireBulletInDirection(float angle)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(gunScript.bullet, firePoint.position, rotation);
        // 不再手动设置速度，Bullet 脚本会根据旋转方向自行设置
    }
}
