using TMPro;
using UnityEngine;
using UnityEngine.UI;  // 如果使用 TextMeshPro 则改为： using TMPro;

public class CountdownTimer : MonoBehaviour
{
    // 倒计时文本
    public TMP_Text countdownText;  // 如果使用 TextMeshPro 则改为： public TextMeshProUGUI countdownText;

    // 倒计时时间（单位：秒），这里设置为20分钟
    public float countdownTime = 20 * 60;

    // 用于检测倒计时结束的事件
    public delegate void TimerEnded();
    public static event TimerEnded OnTimerEnd;

    private bool isTimerRunning = true;

    void Start()
    {
        // 初始化倒计时显示
        UpdateCountdownUI();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (countdownTime > 0)
            {
                // 每帧减少时间
                countdownTime -= Time.deltaTime;
                UpdateCountdownUI();
            }
            else
            {
                // 倒计时结束
                isTimerRunning = false;
                countdownTime = 0;
                UpdateCountdownUI();

                // 触发倒计时结束事件
                if (OnTimerEnd != null)
                {
                    OnTimerEnd.Invoke();
                }
            }
        }
    }

    // 更新倒计时UI
    void UpdateCountdownUI()
    {
        // 防止时间低于0
        countdownTime = Mathf.Max(countdownTime, 0);

        // 计算分钟和秒
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);

        // 更新文本显示
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
