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
    //[HideInInspector] public bool keyJump;

    virtual protected void Update()
    {
        keyPause = Input.GetKeyDown(KeyCode.Escape);

        if (LocalGameManager.instance.isStart)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            keyCatchOrRelease = Input.GetKeyDown(KeyCode.X);
        }
        
#if UNITY_EDITOR
        // 현재 씬 재시작
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
#endif
    }
}
