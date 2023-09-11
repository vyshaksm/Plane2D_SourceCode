using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI timeGameOver;

    private bool hasIncreased;

    private const int starCount = 2;

    private float time = 0;

    private void Timer()
    {
        time += Time.deltaTime;
        if ((int)time % 10 == 0 && !hasIncreased && (int)time!=0)
        {
            EventManager.onStarCollect?.Invoke(starCount);
            hasIncreased = true;
        }
        else if((int)time % 10 != 0)
        {
            hasIncreased = false;
        }

        int minute = (int)time / 60;
        int second = (int)time % 60;

        timeText.text = $"{minute.ToString("00")}:{second.ToString("00")}";
        timeGameOver.text = $"{minute.ToString("00")}:{second.ToString("00")}";
    }

    private void Update()
    {
        Timer();
    }
}
