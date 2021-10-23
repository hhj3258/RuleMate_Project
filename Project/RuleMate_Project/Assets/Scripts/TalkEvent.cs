using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkEvent : MonoBehaviour
{
    public GameObject May_Message;
    public GameObject Brey_Message;
    public Text May_Text;
    public Text Brey_Text;

    Image May_Image;
    Image Brey_Image;
    protected Color May_Color;
    protected Color Brey_Color;

    private void Start()
    {
        May_Image = May_Message.GetComponent<Image>();
        May_Color = May_Image.color;

        Brey_Image = Brey_Message.GetComponent<Image>();
        Brey_Color = Brey_Image.color;
    }

    protected void MayTalk(string txt)
    {
        //메이 대화창은 선명, 브레이 대화창은 반투명
        May_Color.a = 1.0f;
        Brey_Color.a = 0.5f;

        May_Text.text = txt;
    }

    protected void BreyTalk(string txt)
    {
        //메이 대화창은 반투명, 브레이 대화창은 선명
        May_Color.a = 0.5f;
        Brey_Color.a = 1.0f;

        Brey_Text.text = txt;
    }

    protected void Talk(int isMay, string txt)
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isMay == 0)
                MayTalk(txt);
            else
                BreyTalk(txt);
        }
    }
}
