using TMPro;
using UnityEngine;
using UnityEngine.UI;  // 如果使用 TextMeshPro 则改为： using TMPro;

public class VictoryDisplay : MonoBehaviour
{
    // 胜利文本
    public GameObject winPanel;

    void OnEnable()
    {
        // 订阅倒计时结束事件
        CountdownTimer.OnTimerEnd += ShowVictoryMessage;
    }

    void OnDisable()
    {
        // 取消订阅倒计时结束事件
        CountdownTimer.OnTimerEnd -= ShowVictoryMessage;
    }

    // 显示胜利消息
    void ShowVictoryMessage()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);  // 激活Win面板
        }
        Debug.Log("Victory! You Win!");
        Time.timeScale = 0;
    }
}
