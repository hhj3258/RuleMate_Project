using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    protected void MayTalk(string txt)
    {
        //May_Image.SetActive(true);
        //Brey_Image.SetActive(false);

        May_Nametag.SetActive(true);
        Brey_Nametag.SetActive(false);
        MayTxtObj.SetActive(true);
        BreyTxtObj.SetActive(false);

        DOTween.Kill(May_Text);
        May_Text.text = "";
        May_Text.DOText(txt, (float)txt.Length / 20f);
    }

    protected void BreyTalk(string txt)
    {
        //May_Image.SetActive(false);
        //Brey_Image.SetActive(true);

        May_Nametag.SetActive(false);
        Brey_Nametag.SetActive(true);
        MayTxtObj.SetActive(false);
        BreyTxtObj.SetActive(true);

        DOTween.Kill(Brey_Text);
        Brey_Text.text = "";
        Brey_Text.DOText(txt, (float)txt.Length/20f);
    }

    protected void MayBreyTalk(string txt)
    {
        //May_Image.SetActive(true);
        //Brey_Image.SetActive(true);

        May_Nametag.SetActive(true);
        Brey_Nametag.SetActive(true);
        MayTxtObj.SetActive(true);
        BreyTxtObj.SetActive(false);

        DOTween.Kill(May_Text);
        May_Text.text = "";
        May_Text.DOText(txt, (float)txt.Length/20f);
    }

    protected void ExtraTalk(string txt)
    {
        May_Nametag.SetActive(false);
        Brey_Nametag.SetActive(false);
        MayTxtObj.SetActive(true);
        BreyTxtObj.SetActive(false);

        DOTween.Kill(May_Text);
        May_Text.text = "";
        May_Text.DOText(txt, (float)txt.Length / 20f);
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
            ExtraTalk(txt);
        }
        else if (who == "집주인")
        {
            ExtraTalk(txt);
        }
        else if (who == "연출")
        {
            ExtraTalk(txt);
        }
    }
}
