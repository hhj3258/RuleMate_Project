using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] float limitTime = 90f;
    [SerializeField] float endTime = 0f;
    private float minute;
    private float second;

    public Text timer;
    bool isGameStart;

    //나중에 쓸꺼
    private bool isTimeOver = false;
    InGameUIManager InGameUIManager;

    public bool IsTimeOver
    {
        get { return isTimeOver; }
    }

    public bool IsGameStart
    {
        get { return isGameStart; }
        set { isGameStart = value; }
    }

    private void Start()
    {
        isGameStart = false;
        Time.timeScale = 1;
        InGameUIManager = FindObjectOfType<InGameUIManager>();
    }

    void Update()
    {
        if (isGameStart == false)
            return;

        limitTime -= Time.deltaTime;
        minute = (limitTime % 3600) / 60;
        second = (limitTime % 3600) % 60;

        if (limitTime <= endTime && isTimeOver == false)
        {
            isTimeOver = true;
            InGameUIManager.OnResult();
            timer.text = "0 : 00";
            LocalGameManager.instance.isStart = false;
        }
        else if (isTimeOver == false)
        {
            timer.text = string.Format("{0:D2} : {1:D2}", (int)minute, (int)second);
        }
    }
}
