using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public Transform muzzleTransform;
    public Camera cam;

    private Vector3 mousePos;
    private Vector2 gunDirection;

    public int maxAmmo = 24; // 最大子弹数
    private int currentAmmo;  // 当前子弹数
    private bool isReloading = false; // 是否正在换弹

    public float reloadTime = 2f; // 换弹时间

    void Start()
    {
        currentAmmo = maxAmmo; // 初始时加载最大子弹
    }

    // Update is called once per frame
    void Update()
    {
        // 获取鼠标在世界空间中的位置
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

        // 计算从枪到鼠标的方向
        gunDirection = (mousePos - transform.position).normalized;

        // 计算角度
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;

        // 设置枪的旋转
        transform.eulerAngles = new Vector3(0, 0, angle);

        // 如果正在换弹，不能发射子弹
        if (isReloading)
        {
            return;
        }

        // 鼠标左键发射子弹
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            Fire();
        }

        // 如果子弹耗尽，按E换弹
        if (currentAmmo <= 0 && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Reload());
        }
    }

    public void Fire()
    {
        // 发射子弹
        Instantiate(bullet, muzzleTransform.position, Quaternion.Euler(transform.eulerAngles));
        currentAmmo--; // 每次发射消耗一发子弹
    }

    public void DecreaseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;  // 每次调用减少一颗子弹
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true; // 设置为换弹状态
        Debug.Log("正在换弹...");

        yield return new WaitForSeconds(reloadTime); // 等待换弹时间

        currentAmmo = maxAmmo; // 换弹完成，重置子弹数
        isReloading = false; // 结束换弹状态

        Debug.Log("换弹完成！");
    }

    public int GetCurrentAmmo()
    {
        return currentAmmo;
    }

    public int GetMaxAmmo()
    {
        return maxAmmo;
    }

}
