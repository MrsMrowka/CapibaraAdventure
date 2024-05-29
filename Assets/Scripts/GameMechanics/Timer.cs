using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    void Update()
    {
        GlobalVariables.Timer += Time.deltaTime;
        timerText.text = FormatTime();
    }

    public static string FormatTime()
    {
        var minutes = Mathf.FloorToInt(GlobalVariables.Timer / 60);
        var seconds = Mathf.FloorToInt(GlobalVariables.Timer % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
