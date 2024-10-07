using TMPro;
using UnityEngine;
using UnityEngine.UI;  // ���ʹ�� TextMeshPro ���Ϊ�� using TMPro;

public class CountdownTimer : MonoBehaviour
{
    // ����ʱ�ı�
    public TMP_Text countdownText;  // ���ʹ�� TextMeshPro ���Ϊ�� public TextMeshProUGUI countdownText;

    // ����ʱʱ�䣨��λ���룩����������Ϊ20����
    public float countdownTime = 20 * 60;

    // ���ڼ�⵹��ʱ�������¼�
    public delegate void TimerEnded();
    public static event TimerEnded OnTimerEnd;

    private bool isTimerRunning = true;

    void Start()
    {
        // ��ʼ������ʱ��ʾ
        UpdateCountdownUI();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (countdownTime > 0)
            {
                // ÿ֡����ʱ��
                countdownTime -= Time.deltaTime;
                UpdateCountdownUI();
            }
            else
            {
                // ����ʱ����
                isTimerRunning = false;
                countdownTime = 0;
                UpdateCountdownUI();

                // ��������ʱ�����¼�
                if (OnTimerEnd != null)
                {
                    OnTimerEnd.Invoke();
                }
            }
        }
    }

    // ���µ���ʱUI
    void UpdateCountdownUI()
    {
        // ��ֹʱ�����0
        countdownTime = Mathf.Max(countdownTime, 0);

        // ������Ӻ���
        int minutes = Mathf.FloorToInt(countdownTime / 60);
        int seconds = Mathf.FloorToInt(countdownTime % 60);

        // �����ı���ʾ
        countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
