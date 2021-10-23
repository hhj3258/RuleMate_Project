using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkScript : TalkEvent
{
    int n = 0;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TalkSelect(n);
            n++;
        }
    }

    void TalkSelect(int n)
    {
        switch (n)
        {
            case 0:
                Talk(0, "1 : 메이 말하는중1");
                break;
            case 1:
                Talk(1, "2 : 브레이 말하는중1");
                break;
            case 2:
                Talk(0, "3 : 메이 말하는중2");
                break;
            case 3:
                Talk(1, "4 : 브레이 말하는중2");
                break;
            case 4:
                Talk(0, "끝");
                break;
        }
    }
}
