using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 로컬 플레이어 2의 키 할당 스크립트
public class LocalInputManager : InputManager
{
    override protected void Update()
    {
        if (LocalGameManager.instance.isStart)
        {
            h = Input.GetAxisRaw("Horizontal2");
            v = Input.GetAxisRaw("Vertical2");
            keyInterAction = Input.GetKeyDown(KeyCode.Period);
        }
        else
        {
            h = 0;
            v = 0;
        }
    }
}
