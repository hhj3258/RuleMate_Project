using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    public Text txt;
    int cnt = 0;

    public GameObject CountDown;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            cnt++;
        }
        switch (cnt)
        {
            case 1: 
                txt.text = "이동은 WASD로 합니다.";
                break;
            case 2:
                CountDown.SetActive(true);
                gameObject.SetActive(false);
                break;
        }
    }
}
