using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // 引入UI命名空间

public class AmmoUI : MonoBehaviour
{
    public TMP_Text ammoText; // 引用UI文本组件，用于显示子弹数
    private Gun gun; // 引用Gun脚本

    // Start is called before the first frame update
    void Start()
    {
        // 在场景中找到Gun对象，并获取其Gun脚本
        gun = FindObjectOfType<Gun>();

        // 初始化显示剩余子弹数
        UpdateAmmoUI(gun.GetCurrentAmmo(), gun.GetMaxAmmo());
    }

    // 更新UI显示剩余子弹数的方法
    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        ammoText.text = currentAmmo + " / " + maxAmmo; // 更新显示格式
    }

    // 监听换弹完成时，或者射击时的更新操作
    void Update()
    {
        // 每帧更新UI，确保UI与当前枪支的子弹数同步
        UpdateAmmoUI(gun.GetCurrentAmmo(), gun.GetMaxAmmo());
    }
}
