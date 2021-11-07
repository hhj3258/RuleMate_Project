using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkEvent : MonoBehaviour
{
    public GameObject May_Message;
    public GameObject Brey_Message;

    public GameObject May_NextIcon;
    public GameObject Brey_NextIcon;

    public Text May_Text;
    public Text Brey_Text;

    /*
    Image May_Image;
    Image Brey_Image;
    protected Color May_Color;
    protected Color Brey_Color;
    */

    private void Start()
    {
        //May_Image = May_Message.GetComponent<Image>();
        //May_Color = May_Image.material.color;

        //Brey_Image = Brey_Message.GetComponent<Image>();
        //Brey_Color = Brey_Image.material.color;
    }

    protected void MayTalk(string txt)
    {
        //메이 대화창은 선명, 브레이 대화창은 반투명
        //May_Color.a = 1.0f;
        //Brey_Color.a = 0.5f;

        May_Message.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        Brey_Message.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

        May_NextIcon.SetActive(true);
        Brey_NextIcon.SetActive(false);

        May_Text.text = txt;
    }

    protected void BreyTalk(string txt)
    {
        //메이 대화창은 반투명, 브레이 대화창은 선명
        //May_Color.a = 0.5f;
        //Brey_Color.a = 1.0f;

        May_Message.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        Brey_Message.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        May_NextIcon.SetActive(false);
        Brey_NextIcon.SetActive(true);

        Brey_Text.text = txt;
    }

    protected void Talk(string who, string txt)
    {
        if (who == "메이")
            MayTalk(txt);
        else if(who == "브레이")
            BreyTalk(txt);
        else
        {
            MayTalk(txt);
            BreyTalk(txt);
        }
    }
}
