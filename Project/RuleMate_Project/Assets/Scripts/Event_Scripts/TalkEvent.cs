using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkEvent : MonoBehaviour
{
    //public GameObject May_Message;
    //public GameObject Brey_Message;
    public GameObject May_Nametag;
    public GameObject Brey_Nametag;

    public Text May_Text;
    public Text Brey_Text;
    public GameObject MayTxtObj;
    public GameObject BreyTxtObj;

    public GameObject May_Image;
    public GameObject Brey_Image;
    protected Color May_Color;
    protected Color Brey_Color;

    private void Start()
    {
        //May_Image = May_Message.GetComponent<Image>();
        //May_Color = May_Image.material.color;
        //May_Color = May_Image.color;

        //Brey_Image = Brey_Message.GetComponent<Image>();
        //Brey_Color = Brey_Image.material.color;
        //Brey_Color = Brey_Image.color;
    }

    protected void MayTalk(string txt)
    {
        //메이 이미지는 선명, 브레이 이미지는 반투명
        //May_Color.a = 1.0f;
        //Brey_Color.a = 0.5f;
        //May_Image.color = May_Color;
        //Brey_Image.color = Brey_Color;

        May_Image.SetActive(true);
        Brey_Image.SetActive(false);

        //May_Message.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        //Brey_Message.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);

        May_Nametag.SetActive(true);
        Brey_Nametag.SetActive(false);
        MayTxtObj.SetActive(true);
        BreyTxtObj.SetActive(false);
        May_Text.text = txt;
    }

    protected void BreyTalk(string txt)
    {
        //메이 이미지는 반투명, 브레이 이미지는 선명
        //May_Color.a = 0.5f;
        //Brey_Color.a = 1.0f;
        //May_Image.color = May_Color;
        //Brey_Image.color = Brey_Color;

        May_Image.SetActive(false);
        Brey_Image.SetActive(true);

        //May_Message.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        //Brey_Message.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);

        May_Nametag.SetActive(false);
        Brey_Nametag.SetActive(true);
        MayTxtObj.SetActive(false);
        BreyTxtObj.SetActive(true);
        Brey_Text.text = txt;
    }

    protected void MayBreyTalk(string txt)
    {
        //May_Color.a = 1.0f;
        //Brey_Color.a = 1.0f;
        //May_Image.color = May_Color;
        //Brey_Image.color = Brey_Color;

        May_Image.SetActive(true);
        Brey_Image.SetActive(true);

        May_Nametag.SetActive(true);
        Brey_Nametag.SetActive(true);
        MayTxtObj.SetActive(true);
        BreyTxtObj.SetActive(false);
        May_Text.text = txt;
    }

    protected void Talk(string who, string txt)
    {
        // 대사 종류
        // 메이, 브레이, 메이브레이, 친구들, 집주인, 연출
        if (who == "메이")
            MayTalk(txt);
        else if (who == "브레이")
            BreyTalk(txt);
        else if (who == "메이브레이")
        {
            MayBreyTalk(txt);
        }
        else if (who == "친구들")
        {
            BreyTalk(txt);
        }
        else if (who == "집주인")
        {
            BreyTalk(txt);
        }
        else if (who == "연출")
        {
            BreyTalk(txt);
        }
    }
}
