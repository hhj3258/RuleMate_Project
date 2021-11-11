using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public UnityEvent eventGameStart;

    [SerializeField]
    public float limitTime = 90f;

    private float minute;
    private float second;

    public Text timer;

    float unscaleTimer;
    bool isGameStart;

    //나중에 쓸꺼
    private bool timeOver = false;
    InGameUIManager InGameUIManager;

    public bool IsTimeOver
    {
        get { return IsTimeOver; }
    }

    private void Start()
    {
        isGameStart = false;
        InGameUIManager = FindObjectOfType<InGameUIManager>();
    }

    void Update()
    {
        limitTime -= Time.deltaTime;
        minute = (limitTime % 3600) / 60;
        second = (limitTime % 3600) % 60;

        timer.text = ((int)minute + " : " + (int)second).ToString();

        if (limitTime <= 59f)
        {
            timeOver = true;
            InGameUIManager.OnResult();
            Time.timeScale = 0;
        }

        unscaleTimer += Time.unscaledDeltaTime;
        if (unscaleTimer >= 0.8f && isGameStart == false)
        {
            Time.timeScale = 1;
            eventGameStart.Invoke();
            isGameStart = true;
        }
    }
}
