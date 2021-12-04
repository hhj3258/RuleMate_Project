using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LocalGameManager : MonoBehaviour
{
    public int mayScore;
    public int breyScore;
    public int toDay;

    public int randDay04Event;
    public int randDay08Event;

    public bool isStart;

    public int MayMental;
    public int BreyMental;

    // 싱글톤
    public static LocalGameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);

        toDay = 1;
        isStart = false;
    }

    private void Start()
    {
        mayScore = 0;
        breyScore = 0;
        MayMental = 50;
        BreyMental = 50;
        SceneManager.sceneLoaded += OnSceneLoaded;

        RandomEventScripts();
    }

    void RandomEventScripts()
    {
        randDay04Event = Random.Range(1, 9);
        randDay08Event = Random.Range(1, 9);
        if (randDay04Event == randDay08Event)
        {
            if (randDay04Event == 8)
                randDay08Event = 7;
            else if (randDay04Event == 1)
                randDay08Event = 2;
            else
                randDay08Event = randDay04Event + 1;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        toDay++;

        if (scene.name == "Local_InGame")
        {
            isStart = false;
        }

        if (scene.name == "Main_Lobby_Room")
        {
            Time.timeScale = 1;

            if (this != null && this.gameObject != null)
            {
                Destroy(this.gameObject);
                Destroy(this);
            }

            SceneManager.sceneLoaded -= OnSceneLoaded;

        }
    }
}
