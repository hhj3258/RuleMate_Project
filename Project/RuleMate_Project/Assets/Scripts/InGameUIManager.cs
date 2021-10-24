using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class InGameUIManager : UIManager
{
    [SerializeField] GameObject pausePanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
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

    public void OnClickOptions()
    {

    }

    public void OnClickResume()
    {
        pausePanel.SetActive(false);
    }
}
