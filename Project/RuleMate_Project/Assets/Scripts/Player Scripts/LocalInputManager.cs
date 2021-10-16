using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalInputManager : InputManager
{
    // 로컬 플레이어 2의 키 할당
    override protected void Update()
    {
        h = Input.GetAxisRaw("Horizontal2");
        v = Input.GetAxisRaw("Vertical2");

        keyCatchOrRelease = Input.GetKeyDown(KeyCode.Period);
        keyJump = Input.GetKeyDown(KeyCode.RightShift);
    }
}
