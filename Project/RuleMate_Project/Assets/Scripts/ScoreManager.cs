using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    [Header("미션 종류")]
    [SerializeField] List<GameObject> May_Missions;
    [SerializeField] List<GameObject> Brey_Missions;

    [Header("미션 토글")]
    [SerializeField] Toggle toilet;
    [SerializeField] Toggle washDish;
    [SerializeField] Toggle trash;
    [SerializeField] Toggle TV;
    [SerializeField] Toggle cloth;
    [SerializeField] Toggle livingTrash;
    [SerializeField] Dictionary<string, Toggle> test;

    [Header("승패 아이콘")]
    [SerializeField] GameObject[] May_WinLose;
    [SerializeField] GameObject[] Brey_WinLose;

    [Header("메이 슬라이더 & 점수")]
    [SerializeField] Slider May_ScoreSlider;
    [SerializeField] Slider May_MentalSlider;
    [SerializeField] Text May_ScoreText;
    [SerializeField] Text May_MentalText;

    [Header("브레이 슬라이더 & 점수")]
    [SerializeField] Slider Brey_ScoreSlider;
    [SerializeField] Slider Brey_MentalSlider;
    [SerializeField] Text Brey_ScoreText;
    [SerializeField] Text Brey_MentalText;

    int MayScore =0;
    int BreyScore=0;

    Sequence mySequence;

    void OnEnable()
    {
        foreach (var obj in May_Missions)
        {
            if (obj.activeSelf == false)
                MayScore++;
        }

        foreach (var obj in Brey_Missions)
        {
            if (obj.activeSelf == false)
                BreyScore++;
        }

        mySequence = DOTween.Sequence();

        May_MentalSlider.value = LocalGameManager.instance.MayMental / 100f;
        Brey_MentalSlider.value = LocalGameManager.instance.BreyMental / 100f;

        WinCalc();
        ScoreCalc();
        MentalUI();
        ToggleUI();
    }
    
    void ToggleUI()
    {
        // isOn 이 true 이면 주황색
        // May_Missions 는 메이가 해결을 못했다면 active = true 임
        toilet.isOn = May_Missions[0].activeSelf;
        washDish.isOn = May_Missions[1].activeSelf;
        trash.isOn = May_Missions[2].activeSelf;
        TV.isOn = May_Missions[3].activeSelf;
        cloth.isOn = May_Missions[4].activeSelf;
        livingTrash.isOn = May_Missions[5].activeSelf;

        toilet.transform.localScale = Vector3.zero;
        washDish.transform.localScale = Vector3.zero;
        trash.transform.localScale = Vector3.zero;
        TV.transform.localScale = Vector3.zero;
        cloth.transform.localScale = Vector3.zero;
        livingTrash.transform.localScale = Vector3.zero;

        float duration = 0.3f;
        mySequence.Append(toilet.transform.DOScale(1, duration).SetEase(Ease.OutBack))
            .Append(washDish.transform.DOScale(1, duration).SetEase(Ease.OutBack))
            .Append(trash.transform.DOScale(1, duration).SetEase(Ease.OutBack))
            .Append(TV.transform.DOScale(1, duration).SetEase(Ease.OutBack))
            .Append(cloth.transform.DOScale(1, duration).SetEase(Ease.OutBack))
            .Append(livingTrash.transform.DOScale(1, duration).SetEase(Ease.OutBack));
    }

    void MentalUI()
    {
        May_MentalSlider.DOValue(LocalGameManager.instance.MayMental / 100f, 1f);
        May_MentalText.text = LocalGameManager.instance.MayMental.ToString();

        Brey_MentalSlider.DOValue(LocalGameManager.instance.BreyMental / 100f, 1f);
        Brey_MentalText.text = LocalGameManager.instance.BreyMental.ToString();
    }

    void ScoreCalc()
    {
        float May_sliderValue = 0;
        float Brey_sliderValue = 0;

        May_sliderValue = MayScore * (100f / May_Missions.Count);
        mySequence.Join(May_ScoreSlider.DOValue(May_sliderValue / 100f, 1f));
        May_ScoreText.text = Mathf.Round(May_sliderValue) +"/100";

        Brey_sliderValue = 100 - Mathf.Round(May_sliderValue);
        mySequence.Join(Brey_ScoreSlider.DOValue(1 - May_sliderValue / 100f, 1f));
        Brey_ScoreText.text = Brey_sliderValue + "/100";
    }

    void WinCalc()
    {
        if (MayScore > BreyScore)
        {
            May_WinLose[0].SetActive(true);
            May_WinLose[1].SetActive(false);

            Brey_WinLose[0].SetActive(false);
            Brey_WinLose[1].SetActive(true);

            LocalGameManager.instance.MayMental += 15;
            LocalGameManager.instance.BreyMental -= 15;

            LocalGameManager.instance.mayScore += 1;
        }
        else if (MayScore < BreyScore)
        {
            May_WinLose[0].SetActive(false);
            May_WinLose[1].SetActive(true);

            Brey_WinLose[0].SetActive(true);
            Brey_WinLose[1].SetActive(false);

            LocalGameManager.instance.MayMental -= 15;
            LocalGameManager.instance.BreyMental += 15;

            LocalGameManager.instance.breyScore += 1;
        }
    }
}
