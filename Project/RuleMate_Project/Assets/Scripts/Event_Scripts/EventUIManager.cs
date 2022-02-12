using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using DG.Tweening;

public class EventUIManager : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    //[SerializeField]
    GameObject OptionPanel;

    [Header("DayPanel")]
    [SerializeField] GameObject DayPanel;

    private void Start()
    {
        if(LocalGameManager.instance != null)
            DayPanel.transform.Find("Nday").GetComponent<Text>().text = LocalGameManager.instance.toDay.ToString() + "DAY";
        Invoke("DayPanelFade", 0.5f);

        if (GameObject.Find("OptionUI") != null)
            OptionPanel = GameObject.Find("OptionUI").transform.Find("Option").gameObject;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && DayPanel.activeSelf == false)
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
            //Time.timeScale = 1;
        }
        else
        {
            pausePanel.SetActive(true);
            //Time.timeScale = 0;
        }
    }

    public void OnClickMainMenu()
    {
        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();

        LoadingSceneController.LoadingInstance.LoadScene("Main_Lobby_Room");
    }

    public void OnClickResume()
    {
        OnPauseMenu();
    }

    public void OnClickOption()
    {
        OptionPanel.SetActive(true);
    }

    public void OnClickOptionClose()
    {
        OptionPanel.SetActive(false);
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
