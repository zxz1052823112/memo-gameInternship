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
        // 获取鼠标在屏幕上的位置，并转换为世界坐标
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 将 z 轴设为0，因为我们只关注 2D 平面上的 x 和 y 坐标
        mousePosition.z = 0f;

        // 更新物体的位置，使其跟随鼠标
        transform.position = mousePosition;
    }
}
