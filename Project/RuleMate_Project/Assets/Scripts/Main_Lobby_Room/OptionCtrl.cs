using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCtrl : MonoBehaviour
{
    public Slider music;
    public Slider effect;
    public Dropdown control;
    public Dropdown resolution;

    //private int horizontal;
    //private int vertical;
    //public Toggle toggle;

    public static bool isWindowed;
    private string isWindowedValue;

    

    // Start is called before the first frame update
    void Start()
    {
        //세팅 불러오기
        music.value = PlayerPrefs.GetFloat("MusicValue");
        effect.value = PlayerPrefs.GetFloat("EffectValue");
        control.value = PlayerPrefs.GetInt("ControlValue");
        resolution.value = PlayerPrefs.GetInt("ResolutionValue");
        isWindowedValue = PlayerPrefs.GetString("isWindowed");
        isWindowed = System.Convert.ToBoolean(isWindowedValue);

        if (isWindowedValue == null)
        {
            Screen.fullScreen = false;
            isWindowed = true;
        }
    }

   public void ClockToggleButton()
    {
        isWindowed = isWindowed ? false : true;
        Debug.Log(isWindowed);
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
                Screen.SetResolution(2560, 1440, isWindowed);
                break;
            case 1:
                Screen.SetResolution(1920, 1080, isWindowed);
                break;
            case 2:
                Screen.SetResolution(1680, 1050, isWindowed);
                break;
            case 3:
                Screen.SetResolution(1600, 900, isWindowed);
                break;
            case 4:
                Screen.SetResolution(1440, 900, isWindowed);
                break;
            case 5:
                Screen.SetResolution(1280, 1024, isWindowed);
                break;
            case 6:
                Screen.SetResolution(1280, 800, isWindowed);
                break;
            case 7:
                Screen.SetResolution(1280, 720, isWindowed);
                break;
            case 8:
                Screen.SetResolution(1152, 864, isWindowed);
                break;
            case 9:
                Screen.SetResolution(800, 600, isWindowed);
                break;
        }

        //세팅 저장
        PlayerPrefs.SetFloat("MusicValue", music.value);
        PlayerPrefs.SetFloat("EffectValue", effect.value);
        PlayerPrefs.SetInt("ControlValue", control.value);
        PlayerPrefs.SetInt("ResolutionValue", resolution.value);
        PlayerPrefs.SetString("isWindowed", isWindowed.ToString());
    }
}
