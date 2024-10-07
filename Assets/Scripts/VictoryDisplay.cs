using TMPro;
using UnityEngine;
using UnityEngine.UI;  // ���ʹ�� TextMeshPro ���Ϊ�� using TMPro;

public class VictoryDisplay : MonoBehaviour
{
    // ʤ���ı�
    public GameObject winPanel;

    void OnEnable()
    {
        // ���ĵ���ʱ�����¼�
        CountdownTimer.OnTimerEnd += ShowVictoryMessage;
    }

    void OnDisable()
    {
        // ȡ�����ĵ���ʱ�����¼�
        CountdownTimer.OnTimerEnd -= ShowVictoryMessage;
    }

    // ��ʾʤ����Ϣ
    void ShowVictoryMessage()
    {
        if (winPanel != null)
        {
            winPanel.SetActive(true);  // ����Win���
        }
        Debug.Log("Victory! You Win!");
        Time.timeScale = 0;
    }
}
