using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionCtrl : MonoBehaviour
{
    public Slider music;
    public Slider effect;
    public Dropdown control;
    public Dropdown resolution;
    public Toggle toggleWindow;
    private bool isWindowedValue = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        //세팅 불러오기
        music.value = PlayerPrefs.GetFloat("MusicValue");
        effect.value = PlayerPrefs.GetFloat("EffectValue");
        control.value = PlayerPrefs.GetInt("ControlValue");
        resolution.value = PlayerPrefs.GetInt("ResolutionValue");
        // 창모드 세팅 불러오기
        if(PlayerPrefs.GetInt("isWindowed") == 1)
            isWindowedValue = true;
        else
            isWindowedValue = false;
        
        toggleWindow.isOn = isWindowedValue;
        Screen.fullScreen = !toggleWindow.isOn;
    }

    public void OnClickToggleButton()
    {
        Screen.fullScreen = !toggleWindow.isOn;
        isWindowedValue = toggleWindow.isOn;
    }

    public void OnClickExitButton()
    {        
        //세팅 저장
        PlayerPrefs.SetFloat("MusicValue", music.value);
        PlayerPrefs.SetFloat("EffectValue", effect.value);
        PlayerPrefs.SetInt("ControlValue", control.value);
        PlayerPrefs.SetInt("ResolutionValue", resolution.value);
        // 창모드 세팅 저장
        if(isWindowedValue == true)
            PlayerPrefs.SetInt("isWindowed", 1);
        else
            PlayerPrefs.SetInt("isWindowed", 0);

        // 옵션창 active = false
        GameObject option = transform.GetChild(0).gameObject;
        option.SetActive(false);
    }

    // 해상도 세팅 - 드롭다운 이벤트
    public void OnChangedResolution()
    {
        switch (resolution.value)
        {
            case 0:
                Screen.SetResolution(2560, 1440, !toggleWindow.isOn);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, !toggleWindow.isOn);
                break;
            case 2:
                Screen.SetResolution(1600, 900, !toggleWindow.isOn);
                break;
            case 3:
                Screen.SetResolution(1280, 720, !toggleWindow.isOn);
                break;
        }

        PlayerPrefs.SetInt("ResolutionValue", resolution.value);
    }

    // 사운드 슬라이더 이벤트
    public void OnChangedSoundValue()
    {
        foreach (var song in SoundManager.instance.Songs)
        {
            if (song.Key == "Puzzle_32")
                song.Value.volume = music.value * 0.4f;
            else
                song.Value.volume = music.value;
        }
    }
}
