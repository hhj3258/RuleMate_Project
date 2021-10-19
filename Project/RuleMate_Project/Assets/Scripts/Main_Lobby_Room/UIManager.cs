using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] NetWorkMenuManager NetMgr;
    
    #region 메인메뉴
    public void OnClickSingle()=>
        SceneManager.LoadScene("MainGameTest");

    public void OnClickMulti()=>NetMgr.Connect();

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
    public void OnClickCreateRoom()=>NetMgr.CreateRoom();

    public void OnClickJoinRoom()=>NetMgr.JoinRandomRoom();

    public void OnClickMainMenu()=>NetMgr.Disconnect();

    public void OnClickRoomList(int num)=>NetMgr.MyListClick(num);
    #endregion

    #region 룸
    public void OnClickGameStart()=>NetMgr.LoadGameScene();

    public void OnClickLobby()=>NetMgr.LeaveRoom();

    #endregion
}
