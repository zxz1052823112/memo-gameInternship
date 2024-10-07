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

    public int maxAmmo = 24; // ����ӵ���
    private int currentAmmo;  // ��ǰ�ӵ���
    private bool isReloading = false; // �Ƿ����ڻ���

    public float reloadTime = 2f; // ����ʱ��

    void Start()
    {
        currentAmmo = maxAmmo; // ��ʼʱ��������ӵ�
    }

    // Update is called once per frame
    void Update()
    {
        // ��ȡ���������ռ��е�λ��
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));

        // �����ǹ�����ķ���
        gunDirection = (mousePos - transform.position).normalized;

        // ����Ƕ�
        float angle = Mathf.Atan2(gunDirection.y, gunDirection.x) * Mathf.Rad2Deg;

        // ����ǹ����ת
        transform.eulerAngles = new Vector3(0, 0, angle);

        // ������ڻ��������ܷ����ӵ�
        if (isReloading)
        {
            return;
        }

        // �����������ӵ�
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            Fire();
        }

        // ����ӵ��ľ�����E����
        if (currentAmmo <= 0 && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(Reload());
        }
    }

    public void Fire()
    {
        // �����ӵ�
        Instantiate(bullet, muzzleTransform.position, Quaternion.Euler(transform.eulerAngles));
        currentAmmo--; // ÿ�η�������һ���ӵ�
    }

    public void DecreaseAmmo()
    {
        if (currentAmmo > 0)
        {
            currentAmmo--;  // ÿ�ε��ü���һ���ӵ�
        }
    }

    public IEnumerator Reload()
    {
        isReloading = true; // ����Ϊ����״̬
        Debug.Log("���ڻ���...");

        yield return new WaitForSeconds(reloadTime); // �ȴ�����ʱ��

        currentAmmo = maxAmmo; // ������ɣ������ӵ���
        isReloading = false; // ��������״̬

        Debug.Log("������ɣ�");
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
