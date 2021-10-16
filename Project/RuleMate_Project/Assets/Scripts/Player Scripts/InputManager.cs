using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public float h; // A || D
    [HideInInspector] public float v; // W || S
    [HideInInspector] public bool keyCatchOrRelease; // 잡기 || 놓기
    [HideInInspector] public bool keyPause;
    [HideInInspector] public bool keyJump;

    virtual protected void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        keyCatchOrRelease = Input.GetKeyDown(KeyCode.X);
        keyPause = Input.GetKeyDown(KeyCode.Escape);
        keyJump = Input.GetKeyDown(KeyCode.Space);

        ////////////// 임시 키 ////////////////////
        // 현재 씬 재시작
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        ///////////////////////////////////////////
    }
}
