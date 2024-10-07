using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // ����UI�����ռ�

public class AmmoUI : MonoBehaviour
{
    public TMP_Text ammoText; // ����UI�ı������������ʾ�ӵ���
    private Gun gun; // ����Gun�ű�

    // Start is called before the first frame update
    void Start()
    {
        // �ڳ������ҵ�Gun���󣬲���ȡ��Gun�ű�
        gun = FindObjectOfType<Gun>();

        // ��ʼ����ʾʣ���ӵ���
        UpdateAmmoUI(gun.GetCurrentAmmo(), gun.GetMaxAmmo());
    }

    // ����UI��ʾʣ���ӵ����ķ���
    public void UpdateAmmoUI(int currentAmmo, int maxAmmo)
    {
        ammoText.text = currentAmmo + " / " + maxAmmo; // ������ʾ��ʽ
    }

    // �����������ʱ���������ʱ�ĸ��²���
    void Update()
    {
        // ÿ֡����UI��ȷ��UI�뵱ǰǹ֧���ӵ���ͬ��
        UpdateAmmoUI(gun.GetCurrentAmmo(), gun.GetMaxAmmo());
    }
}
