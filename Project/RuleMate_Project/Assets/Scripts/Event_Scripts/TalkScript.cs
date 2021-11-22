using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TalkScript : TalkEvent
{
    int n = 0;
    string[] lines;
    string[] imgLines;

    [SerializeField] Text txtClick;
    Sequence sequence;

    void Start()
    {
        if (LocalGameManager.instance == null)
            SetTable(1);
        else
        {
            //Debug.Log("Day: " + LocalGameManager.instance.toDay);
            SetTable(LocalGameManager.instance.toDay + 1);
        }

        May_Image.SetActive(false);
        Brey_Image.SetActive(false);

        May_Nametag.SetActive(false);
        Brey_Nametag.SetActive(false);

        May_Text.text = "";
        Brey_Text.text = "";

        Time.timeScale = 1;

        sequence = DOTween.Sequence().SetLoops(-1);
        sequence.Join(txtClick.DOFade(0, 2))
                       .Append(txtClick.DOFade(0.5f, 2));
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
            SetImages(n);
            n++;
        }
    }

    public void SetTable(int today)
    {
        // Resources 폴더 내의 EventScripts.csv 파일
        string strTodayEvent = "EventScripts/";
        string strImgDir = "EventImageCSV/";

        if (today == 1)
        {
            strTodayEvent += "Day01_Event";
            strImgDir += "Day01_Event";
        }
        else if (today == 4)
        {
            strTodayEvent += "Day04or08_Event_";
            strTodayEvent += LocalGameManager.instance.randDay04Event;
            strImgDir += "Day04or08_Event_";
            strImgDir += LocalGameManager.instance.randDay04Event;
        }
        else if (today == 8)
        {
            strTodayEvent += "Day04or08_Event_";
            strTodayEvent += LocalGameManager.instance.randDay08Event;
            strImgDir += "Day04or08_Event_";
            strImgDir += LocalGameManager.instance.randDay08Event;
        }
        else if (today == 12)
        {
            //strTodayEvent += "Day12_Event_BreyWin";
            //strTodayEvent += "Day12_Event_MayWin";
        }
        else if (today == 14)
        {
            //strTodayEvent += "Day14_Event_BreyWin";
            //strTodayEvent += "Day14_Event_MayWin";
            //strTodayEvent += "Day14_Event_Draw";
        }
        else
        {
            return;
        }

        //Debug.Log("strTodayEvent: " + strTodayEvent);
        TextAsset text = Resources.Load<TextAsset>(strTodayEvent);
        string content = text.text;
        lines = content.Split('\n');

        TextAsset text2 = Resources.Load<TextAsset>(strImgDir);
        string content2 = text2.text;
        imgLines = content2.Split('\n');
    }

    void TalkSelect(int num)
    {
        string[] column = lines[num].Split(',');

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

    void SetImages(int num)
    {
        string[] column = imgLines[num].Split(',');

        string strPath = "Character_Standing/";

        if (column[0] != "x")
        {
            string temp = strPath + column[0];
            May_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>(temp);
            May_Image.SetActive(true);
        }
        else if(column[0] == "x")
        {
            May_Image.SetActive(false);
        }

        column[1] = column[1].Trim().Replace("\r", string.Empty);
        column[1] = column[1].Trim().Replace("\n", string.Empty);
        column[1] = column[1].Replace(Environment.NewLine, string.Empty);

        if (column[1] != "x")
        {
            string temp = strPath + column[1];

            Brey_Image.GetComponent<Image>().sprite = Resources.Load<Sprite>(temp);
            Brey_Image.SetActive(true);
        }
        else if(column[1] == "x")
        {
            Brey_Image.SetActive(false);
        }
    }

    public void OnClickSkip()
    {
        LoadingSceneController.LoadingInstance.LoadScene("Local_InGame");
    }
}
