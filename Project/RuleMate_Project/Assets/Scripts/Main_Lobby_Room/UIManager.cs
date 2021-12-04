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

    [Header("Title")]
    [SerializeField] GameObject TitlePanel;
    [SerializeField] GameObject txtPressKey;

    [Header("Option")]
    [SerializeField] protected GameObject OptionPanel;

    [Header("Local")]
    [SerializeField] GameObject MainMenuPanel;
    [SerializeField] GameObject LocalRoomPanel;
    [SerializeField] Text txtMayPlayer;
    [SerializeField] Text txtBreayPlayer;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
    }

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
        // 페이드 효과
        foreach (var img in imgs)
            img.DOFade(0f, 1.7f);

        // 애니메이션 재생 스탑
        txtPressKey.GetComponent<Animator>().enabled = false;
        // 텍스트는 따로 처리
        txtPressKey.GetComponent<Text>().DOFade(0f, 1.7f);

        // 페이드 효과 후 Active => false
        Invoke("TitleActive", 1.7f);
    }

    void TitleActive() => TitlePanel.SetActive(false);

    #endregion

    #region 로컬메뉴
    public void OnClickSingle()
    {
        MainMenuPanel.SetActive(false);
        LocalRoomPanel.SetActive(true);

        //SelectManager.instance.localPlayer1 = Character.Player1;
        //SelectManager.instance.localPlayer2 = Character.Player2;
    }

    public void OnClickLocalGameStart()
    {
        LoadSceneWithLoading("EventScene");
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

            txtMayPlayer.text = "2P";
            txtBreayPlayer.text = "1P";
        }
        else
        {
            SelectManager.instance.localPlayer1 = Character.Player1;
            SelectManager.instance.localPlayer2 = Character.Player2;

            txtMayPlayer.text = "1P";
            txtBreayPlayer.text = "2P";
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
