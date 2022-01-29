using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public Dictionary<string, AudioSource> Songs;
    public string TitleSong;
    public string LobbySong;
    public string InGameSong;
    public string EventSong;
    public string ResultSong;

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != null)
            return;
        DontDestroyOnLoad(gameObject);

        Songs = new Dictionary<string, AudioSource>();

        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();
        foreach (var song in audioSources)
            Songs.Add(song.name, song);

        Songs[TitleSong].Play();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        foreach (var song in Songs)
            song.Value.Stop();

        if (scene.name == "Local_InGame")
            Songs[InGameSong].Play();
        else if(scene.name == "Main_Lobby_Room")
            Songs[TitleSong].Play();
        else if (scene.name == "EventScene")
            Songs[EventSong].Play();
    }

    public void PlayResultSong()
    {
        foreach (var song in Songs)
            song.Value.Stop();

        Songs[ResultSong].Play();
    }
}
