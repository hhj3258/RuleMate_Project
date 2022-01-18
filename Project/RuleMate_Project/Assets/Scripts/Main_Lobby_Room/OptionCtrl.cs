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

    //public static bool isWindowed;
    private bool isWindowedValue;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        //세팅 불러오기
        music.value = PlayerPrefs.GetFloat("MusicValue");
        effect.value = PlayerPrefs.GetFloat("EffectValue");
        control.value = PlayerPrefs.GetInt("ControlValue");
        resolution.value = PlayerPrefs.GetInt("ResolutionValue");
        Debug.Log("1" + PlayerPrefs.GetInt("isWindowed"));
        isWindowedValue = System.Convert.ToBoolean(PlayerPrefs.GetInt("isWindowed"));
        //isWindowed = System.Convert.ToBoolean(isWindowedValue);
        toggleWindow.isOn = isWindowedValue;

        Debug.Log(isWindowedValue);
        /*if (isWindowedValue == null)
        {
            Screen.fullScreen = false;
            isWindowed = true;
        }*/
    }
    private void Update()
    {
        Debug.Log(toggleWindow.isOn);
        Debug.Log(isWindowedValue);
    }

    public void ClockToggleButton()
    {
        isWindowedValue = isWindowedValue ? false : true;
    }

    public void ClickExitButton()
    {
        //컨트롤 세팅
        switch (control.value)
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
        //해상도 세팅
        switch (resolution.value)
        {
            case 0:
                Screen.SetResolution(2560, 1440, isWindowedValue);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, isWindowedValue);
                break;
            case 2:
                Screen.SetResolution(1680, 1050, isWindowedValue);
                break;
            case 3:
                Screen.SetResolution(1600, 900, isWindowedValue);
                break;
            case 4:
                Screen.SetResolution(1440, 900, isWindowedValue);
                break;
            case 5:
                Screen.SetResolution(1280, 1024, isWindowedValue);
                break;
            case 6:
                Screen.SetResolution(1280, 800, isWindowedValue);
                break;
            case 7:
                Screen.SetResolution(1280, 720, isWindowedValue);
                break;
            case 8:
                Screen.SetResolution(1152, 864, isWindowedValue);
                break;
            case 9:
                Screen.SetResolution(800, 600, isWindowedValue);
                break;
        }

        //세팅 저장
        PlayerPrefs.SetFloat("MusicValue", music.value);
        PlayerPrefs.SetFloat("EffectValue", effect.value);
        PlayerPrefs.SetInt("ControlValue", control.value);
        PlayerPrefs.SetInt("ResolutionValue", resolution.value);
        PlayerPrefs.SetInt("isWindowed", System.Convert.ToInt32(isWindowedValue));
    }

    public void OnClickOptionClose()
    {
        GameObject option = transform.GetChild(0).gameObject;
        option.SetActive(false);
    }
}
