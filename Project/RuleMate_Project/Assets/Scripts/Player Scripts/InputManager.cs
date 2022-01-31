using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    [HideInInspector] public float h; // A || D
    [HideInInspector] public float v; // W || S
    [HideInInspector] public bool keyInterAction; // 상호작용키
    [HideInInspector] public bool keyPause;

    virtual protected void Update()
    {
        keyPause = Input.GetKeyDown(KeyCode.Escape);

        if (LocalGameManager.instance.isStart)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
            keyInterAction = Input.GetKeyDown(KeyCode.X);
        }
        else
        {
            h = 0;
            v = 0;
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
