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
    [SerializeField] TextMeshProUGUI txtDay;

    [Header("ResultPanel")]
    [SerializeField] GameObject ResultPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (OptionPanel.activeSelf == true)
                return;

            if (pausePanel.activeSelf)
                pausePanel.SetActive(false);
            else
                pausePanel.SetActive(true);
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

    public void OnClickNextStage()
    {
        LoadSceneWithLoading("UI_Test");
    }

    public void OnResult()
    {
        ResultPanel.SetActive(true);
    }

    public void DayPanelSetActive()
    {
        DayPanel.SetActive(false);
    }
}
