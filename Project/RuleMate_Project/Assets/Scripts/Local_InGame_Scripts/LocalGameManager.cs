using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LocalGameManager : MonoBehaviour
{
    float mayPoint, breayPoint;
    public int toDay;

    public int randDay04Event;
    public int randDay08Event;

    // 싱글톤
    public static LocalGameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);

        toDay = 1;
    }

    private void Start()
    {
        mayPoint = 0;
        breayPoint = 0;
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
        if (scene.name == "Local_InGame")
        {
            toDay++;
        }

        if (scene.name == "Main_Lobby_Room")
        {
            Time.timeScale = 1;

            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(this.gameObject);
        }
    }

    public void MayPoint()
    {
        mayPoint += 1f;
        breayPoint -= 1f;
    }

    public void BreayPoint()
    {
        mayPoint -= 1f;
        breayPoint += 1f;
    }


}
