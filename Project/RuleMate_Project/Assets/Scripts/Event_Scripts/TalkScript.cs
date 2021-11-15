using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkScript : TalkEvent
{
    int n = 0;
    string[] lines;

    void Start()
    {
        if (LocalGameManager.instance == null)
            SetTable(1);
        else
        {
            Debug.Log("Day: " + LocalGameManager.instance.toDay);
            SetTable(LocalGameManager.instance.toDay + 1);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (n == lines.Length)
            {
                Debug.Log("대화 끝");
                LoadingSceneController.LoadingInstance.LoadScene("Local_InGame");
                return;
            }

            TalkSelect(n);
            n++;
        }
    }

    public void SetTable(int today)
    {
        // Resources 폴더 내의 EventScripts.csv 파일
        string strToday = "EventScripts/";
        if (today == 1)
            strToday += "Day01_Event";
        else if (today == 4)
        {
            strToday += "Day04or08_Event_";
            strToday += LocalGameManager.instance.randDay04Event;
        }
        else if (today == 8)
        {
            strToday += "Day04or08_Event_";
            strToday += LocalGameManager.instance.randDay08Event;
        }
        else if(today == 12)
        {
            //strToday += "Day12_Event_BreyWin";
            //strToday += "Day12_Event_MayWin";
        }
        else if(today == 14)
        {
            //strToday += "Day14_Event_BreyWin";
            //strToday += "Day14_Event_MayWin";
            //strToday += "Day14_Event_Draw";
        }
        else
        {
            return;
        }

        Debug.Log("strToday: " + strToday);
        TextAsset text = Resources.Load<TextAsset>(strToday);
        string content = text.text;
        lines = content.Split('\n');
    }

    void TalkSelect(int n)
    {
        string[] column = lines[n].Split(',');

        string txtTemp = "";
        for (int i = 1; i < column.Length; i++)
        {
            if(i==column.Length-1)
                txtTemp += (column[i]);
            else
                txtTemp += (column[i] + ',');
        }

        Talk(column[0], txtTemp);
    }

    public void OnClickSkip()
    {
        LoadingSceneController.LoadingInstance.LoadScene("Local_InGame");
    }
}
