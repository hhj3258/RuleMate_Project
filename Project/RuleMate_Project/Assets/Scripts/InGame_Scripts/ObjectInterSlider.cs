using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInterSlider : MonoBehaviour
{
    public Slider slider;

    float progress = 0;
    float progressMIN = 0;
    float progressMAX = 100;

    //public GameObject player;

    //미션중이면~ 나중에 추가
    private bool isMissioned = true;

    // Start is called before the first frame update
    void Start()
    {
        slider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ActiveSlider(isMissioned);

        //slider.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + new Vector3(0, 0.8f, 0));
    }

    protected bool ActiveSlider(bool isMissioned)
    {
        if (isMissioned == true)
        {
            slider.minValue = progressMIN;
            slider.maxValue = progressMAX;
            slider.value = progress;

            if (Input.GetKey(KeyCode.Z))
            {
                progress += 0.25f;
            }
            else
            {
                if (progress >= 0)
                    progress -= 0.25f;
            }

            if (progress > 0)
                slider.gameObject.SetActive(true);
            else
                slider.gameObject.SetActive(false);

            if (progress == 100)
                return true;
            else
                return false;
        }
        else
            return false;
    }
}
