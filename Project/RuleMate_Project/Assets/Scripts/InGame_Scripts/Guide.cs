using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    public Text txt;
    int cnt = 0;

    public GameObject CountDown;

    private void Start()
    {
        if(LocalGameManager.instance.toDay > 1)
        {
            //CountDown.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && LocalGameManager.instance.toDay == 1)
        {
            cnt++;

            switch (cnt)
            {
                case 1:
                    txt.text = "이동은 WASD, 상호작용은 Z입니다. (브레이는 M) ";
                    break;
                case 2:
                    //CountDown.SetActive(true);
                    gameObject.SetActive(false);
                    break;
            }
        }

    }
}
