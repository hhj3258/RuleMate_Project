using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] NetWorkMenuManager NetMgr;

    [SerializeField] GameObject TitlePanel;

    [Header("Option")]
    [SerializeField] protected GameObject OptionPanel;

    [Header("Local")]
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject LocalRoomPanel;
    [SerializeField] TextMeshProUGUI txtMayPlayer;
    [SerializeField] TextMeshProUGUI txtBreayPlayer;

    public void LoadSceneWithLoading(string sceneName)
    {
        LoadingSceneController.LoadingInstance.LoadScene(sceneName);
    }

    public void OnClickOption()
    {
        OptionPanel.SetActive(true);
    }

    public void OnClickOptionClose()
    {
        OptionPanel.SetActive(false);
    }

    #region 타이틀메뉴

    // 타이틀 화면 클릭 시 페이드 효과
    public void OnClickScreen()
    {
        var imgs = TitlePanel.GetComponentsInChildren<Image>();
        foreach (var img in imgs)
            img.DOFade(0f, 1.7f);

        // 텍스트는 따로 처리
        TitlePanel.GetComponentInChildren<Text>().DOFade(0f, 1.7f);

        // 페이드 효과 후 Active => false
        Invoke("TitleActive", 1.8f);
    }

    void TitleActive() => TitlePanel.SetActive(false);

    #endregion

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
        if (SelectManager.instance.localPlayer1 == Character.Player1)
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

}
