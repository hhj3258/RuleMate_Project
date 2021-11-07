using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkScript : TalkEvent
{
    int n = 0;
    string[] lines;

    void Start()
    {
        SetTable();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TalkSelect(n);
            n++;
        }
    }

    public void SetTable()
    {
        // Resources 폴더 내의 EventScripts.csv 파일
        TextAsset text = Resources.Load<TextAsset>("EventScripts");
        string content = text.text;
        lines = content.Split('\n');
    }

    void TalkSelect(int n)
    {
        if (n == lines.Length)
        {
            Debug.Log("대화 끝");
            LoadingSceneController.LoadingInstance.LoadScene("Map_Test");
            return;
        }

        string[] column = lines[n].Split(',');
        Talk(column[0], column[1]);
    }

    public void OnClickSkip()
    {
        LoadingSceneController.LoadingInstance.LoadScene("Map_Test");
    }
}
