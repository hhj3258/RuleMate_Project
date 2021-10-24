using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] NetWorkMenuManager NetMgr;

    [Header("Local")]
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject LocalRoomPanel;
    [SerializeField] TextMeshProUGUI txtMayPlayer;
    [SerializeField] TextMeshProUGUI txtBreayPlayer;

    public void LoadSceneWithLoading(string sceneName)
    {
        LoadingSceneController.LoadingInstance.LoadScene(sceneName);
    }

    #region 로컬메뉴
    public void OnClickSingle()
    {
        MainMenuPanel.SetActive(false);
        LocalRoomPanel.SetActive(true);

        SelectManager.instance.localPlayer1 = Character.Player1;
        SelectManager.instance.localPlayer2 = Character.Player2;
    }

    public void OnClickLocalGameStart()
    {
        //SceneManager.LoadScene("Map_Test");
        LoadSceneWithLoading("Map_Test");
    }

    public void OnClickLocalMainMenu()
    {
        MainMenuPanel.SetActive(true);
        LocalRoomPanel.SetActive(false);
    }

    public void OnClickChangeCharacter()
    {
        if(SelectManager.instance.localPlayer1 == Character.Player1)
        {
            SelectManager.instance.localPlayer1 = Character.Player2;
            SelectManager.instance.localPlayer2 = Character.Player1;

            txtMayPlayer.text = "P2";
            txtBreayPlayer.text = "P1";
        }
        else
        {
            SelectManager.instance.localPlayer1 = Character.Player1;
            SelectManager.instance.localPlayer2 = Character.Player2;

            txtMayPlayer.text = "P1";
            txtBreayPlayer.text = "P2";
        }
    }

    #endregion

    #region 메인메뉴

    public void OnClickMulti() => NetMgr.Connect();

    public void OnClickExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    #endregion

    #region 로비
    public void OnClickCreateRoom() => NetMgr.CreateRoom();

    public void OnClickJoinRoom() => NetMgr.JoinRandomRoom();

    public virtual void OnClickMainMenu() => NetMgr.Disconnect();

    public void OnClickRoomList(int num) => NetMgr.MyListClick(num);
    #endregion

    #region 룸
    public void OnClickGameStart() => NetMgr.LoadGameScene();

    public void OnClickLobby() => NetMgr.LeaveRoom();

    #endregion

    public void Test()
    {
        NetMgr.LoadGameScene();
    }
}
