using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField] float limitTime = 90f;

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

        timer.text = ((int)minute + " : " + (int)second).ToString();

        if (limitTime <= 87f)
        {
            isTimeOver = true;
            InGameUIManager.OnResult();
            Time.timeScale = 0;
            timer.text = "0 : 00";
            LocalGameManager.instance.isStart = false;
        }

    }
}
