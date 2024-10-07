using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BrainMonstor : MonoBehaviour
{
    public Transform player;  // 玩家对象的Transform
    public float speed = 2.0f;  // 敌人移动速度

    private void Update()
    {
        // 计算敌人到玩家的方向向量
        Vector3 direction = player.position - transform.position;
        direction.Normalize();  // 归一化方向向量，使其长度为1

        // 移动敌人朝向玩家
        transform.position += direction * speed * Time.deltaTime;

        // 如果敌人朝向左边（X方向是负值）
        if (direction.x < 0)
            transform.eulerAngles = new Vector3(x: 0f, y: 180f, z: 0f);
        else
            transform.eulerAngles = new Vector3(x: 0f, y: 0f, z: 0f);

    }
}
