using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrainMonstor : MonoBehaviour
{
    public Transform player;  // ��Ҷ����Transform
    public float speed = 2.0f;  // �����ƶ��ٶ�

    private void Update()
    {
        // ������˵���ҵķ�������
        Vector3 direction = player.position - transform.position;
        direction.Normalize();  // ��һ������������ʹ�䳤��Ϊ1

        // �ƶ����˳������
        transform.position += direction * speed * Time.deltaTime;

        // ������˳�����ߣ�X�����Ǹ�ֵ��
        if (direction.x < 0)
            transform.eulerAngles = new Vector3(x: 0f, y: 180f, z: 0f);
        else
            transform.eulerAngles = new Vector3(x: 0f, y: 0f, z: 0f);

    }
}
