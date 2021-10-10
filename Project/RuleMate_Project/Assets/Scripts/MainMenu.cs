using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void OnClickSingle()
    {
        SceneManager.LoadScene("MainGameTest");
    }

    public void OnClickMulti()
    {
        SceneManager.LoadScene("Online Lobby");
    }
}
