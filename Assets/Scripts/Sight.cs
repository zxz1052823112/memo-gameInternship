using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ��ȡ�������Ļ�ϵ�λ�ã���ת��Ϊ��������
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �� z ����Ϊ0����Ϊ����ֻ��ע 2D ƽ���ϵ� x �� y ����
        mousePosition.z = 0f;

        // ���������λ�ã�ʹ��������
        transform.position = mousePosition;
    }
}
