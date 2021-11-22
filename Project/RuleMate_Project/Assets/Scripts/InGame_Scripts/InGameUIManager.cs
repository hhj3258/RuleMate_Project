using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class InGameUIManager : UIManager
{
    [SerializeField] GameObject pausePanel;

    [Header("InGame")]
    [SerializeField] TextMeshProUGUI txtMayPoint;
    [SerializeField] TextMeshProUGUI txtBreayPoint;

    [Header("DayPanel")]
    [SerializeField] GameObject DayPanel;

    [Header("ResultPanel")]
    [SerializeField] GameObject ResultPanel;

    private void Start()
    {
        DayPanel.transform.Find("Nday").GetComponent<Text>().text = LocalGameManager.instance.toDay.ToString()+" DAY";
        Invoke("DayPanelFade", 0.5f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionPanel.activeSelf == true)
            {
                OptionPanel.SetActive(false);
                return;
            }

            OnPauseMenu();
        }
    }

    void OnPauseMenu()
    {
        if (pausePanel.activeSelf)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public override void OnClickMainMenu()
    {
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();

        LoadSceneWithLoading("Main_Lobby_Room");
    }

    public void OnClickResume()
    {
        pausePanel.SetActive(false);
    }

    // Result 패널의 확인 버튼
    public void OnClickNextStage()
    {
        int nextDay = LocalGameManager.instance.toDay+1;
        if (nextDay == 4 || nextDay == 8 || nextDay == 12 || nextDay == 14)
            LoadSceneWithLoading("EventScene");
        else
            LoadSceneWithLoading("Local_InGame");
    }

    public void OnResult()
    {
        ResultPanel.SetActive(true);
    }

    public void DayPanelFade()
    {
        var imgs = DayPanel.GetComponentsInChildren<Image>();
        // 페이드 효과
        foreach (var img in imgs)
            img.DOFade(0f, 3f);

        var txts = DayPanel.GetComponentsInChildren<Text>();
        foreach (var txt in txts)
            txt.DOFade(0f, 3f);

        // 페이드 효과 후 Active => false
        Invoke("DayPanelActive", 3f);
    }

    void DayPanelActive() => DayPanel.SetActive(false);
}
