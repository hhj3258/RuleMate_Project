using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionCtrl : MonoBehaviour
{
    public Slider music;
    public Slider effect;

    // Start is called before the first frame update
    void Start()
    {
        music.value = PlayerPrefs.GetFloat("MusicValue");
        effect.value = PlayerPrefs.GetFloat("EffectValue");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickExitButton()
    {
        PlayerPrefs.SetFloat("MusicValue", music.value);
        PlayerPrefs.SetFloat("EffectValue", effect.value);
    }
}
