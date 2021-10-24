using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class NetWorkMenuManager : MonoBehaviourPunCallbacks
{
    [Header("MainMenu")]
    public GameObject MainMenuPanel;
    public Button btnMulti;
    public InputField NickNameInput;

    [Header("Lobby")]
    public GameObject LobbyPanel;
    public InputField RoomInput;
    public TextMeshProUGUI WelcomeText;
    public TextMeshProUGUI LobbyInfoText;
    public Button[] CellBtn;
    public Button PreviousBtn;
    public Button NextBtn;

    [Header("Room")]
    public GameObject RoomPanel;
    public TextMeshProUGUI[] PlayerNickNameTexts;

    public Text[] ChatText;
    public InputField ChatInput;

    [Header("ETC")]
    public TextMeshProUGUI connectionInfoText;
    public PhotonView PV;

    List<RoomInfo> myList = new List<RoomInfo>();
    int currentPage = 1, maxPage, multiple;

    // 멀티플레이 시에 게임 버전이 같아야 함.
    private readonly string gameVersion = "1";

    #region 서버&로비 연결
    void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        // 같은 방 안의 호스트와 게스트의 싱크를 맞춤
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Update()
    {
        // 서버 연결 상태
        connectionInfoText.text = PhotonNetwork.NetworkClientState.ToString();

        LobbyInfoText.text = (PhotonNetwork.CountOfPlayers - PhotonNetwork.CountOfPlayersInRooms) + " Lobby / " + PhotonNetwork.CountOfPlayers + " OnServer";
    }

    // 마스터서버 접속을 성공하면 자동적으로 메소드가 실행된다.
    public override void OnConnectedToMaster()
    {
        btnMulti.interactable = true;

        PhotonNetwork.JoinLobby();
    }

    // 접속을 시도했지만 실패한 경우나 접속중에 끊어진 경우에 자동으로 실행된다.
    // DisconnectCause cause : 커넥팅이 끊긴 사유
    public override void OnDisconnected(DisconnectCause cause)
    {
        //btnMulti.interactable = false;
        LobbyPanel.SetActive(false);
        RoomPanel.SetActive(false);
        MainMenuPanel.SetActive(true);

        // 재접속 시도
        // 접속에 성공할 때까지 계속 재접속을 시도하게 된다.
        //PhotonNetwork.ConnectUsingSettings();
    }

    // btnMulti 버튼 클릭 이벤트
    public void Connect()
    {
        btnMulti.interactable = false;

        // 서버와 연결이 되어 있다면
        if (PhotonNetwork.IsConnected)
        {
            // 방을 검색하고 있다는 문구 출력
            //connectionInfoText.text = "Connecting to Random Room...";
            // 매치메이킹 서버에서 감지한 랜덤룸에 자동 접속 시도
            // 빈 방이 없을 때는 당연히 실패 -> OnJoinRandomFailed 자동 실행

            //PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            // 접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }

    }

    public override void OnJoinedLobby()
    {
        LobbyPanel.SetActive(true);
        RoomPanel.SetActive(false);
        MainMenuPanel.SetActive(false);

        PhotonNetwork.LocalPlayer.NickName = NickNameInput.text;
        WelcomeText.text = "Welcome! " + PhotonNetwork.LocalPlayer.NickName;
        myList.Clear();
    }

    public void Disconnect() => PhotonNetwork.Disconnect();

    #endregion

    #region 방리스트 갱신
    // ◀버튼 -2 , ▶버튼 -1 , 셀 숫자
    public void MyListClick(int num)
    {
        if (num == -2) --currentPage;
        else if (num == -1) ++currentPage;
        else  // 방 목록 중 하나 클릭
        {
            Debug.Log("name: " + myList[multiple + num].Name);
            PhotonNetwork.JoinRoom(myList[multiple + num].Name);
        }
        MyListRenewal();
    }

    void MyListRenewal()
    {
        // 최대페이지
        maxPage = (myList.Count % CellBtn.Length == 0) ? myList.Count / CellBtn.Length : myList.Count / CellBtn.Length + 1;

        // 이전, 다음버튼
        PreviousBtn.interactable = (currentPage <= 1) ? false : true;
        NextBtn.interactable = (currentPage >= maxPage) ? false : true;

        // 페이지에 맞는 리스트 대입
        multiple = (currentPage - 1) * CellBtn.Length;
        for (int i = 0; i < CellBtn.Length; i++)
        {
            CellBtn[i].interactable = (multiple + i < myList.Count) ? true : false;
            CellBtn[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (multiple + i < myList.Count) ? myList[multiple + i].Name : "";
            CellBtn[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = (multiple + i < myList.Count) ? myList[multiple + i].PlayerCount + "/" + myList[multiple + i].MaxPlayers : "";
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int roomCount = roomList.Count;
        for (int i = 0; i < roomCount; i++)
        {
            if (!roomList[i].RemovedFromList)
            {
                if (!myList.Contains(roomList[i])) myList.Add(roomList[i]);
                else myList[myList.IndexOf(roomList[i])] = roomList[i];
            }
            else if (myList.IndexOf(roomList[i]) != -1) 
                myList.RemoveAt(myList.IndexOf(roomList[i]));
        }
        MyListRenewal();
    }
    #endregion

    #region 룸
    public void CreateRoom()
    {
        string roomName = "";
        if (RoomInput.text == "")
        {
            roomName = "Room" + Random.Range(0, 1000);
        }
        else
        {
            roomName = RoomInput.text;
        }

        PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 });
    }

    public void JoinRandomRoom() => PhotonNetwork.JoinRandomRoom();

    public void LeaveRoom() => PhotonNetwork.LeaveRoom();

    public override void OnCreateRoomFailed(short returnCode, string message) { RoomInput.text = ""; CreateRoom(); }

    // 빈 방 찾기 실패했을 때 자동 실행
    // 다른 이유로 실행되는 경우도 있음
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        // 여기서는 방이 없어서 실패했을 경우가 대부분이므로 방을 새로 만든다.
        RoomInput.text = "";
        CreateRoom();
    }

    // 방에 참가 완료했을 때 자동 실행
    public override void OnJoinedRoom()
    {
        // SceneManager.LoadScene() 을 통해 씬을 이동하면 안됨!
        // 로드 씬을 통해서 씬을 넘어가게 되면, 나 혼자만 넘어가고 다른 참가자들은 넘어가지 않는다.
        // 2명의 플레이어가 로드 씬을 각각 실행하게 되면 제각각 Main 씬으로 넘어가게 된다.
        // 동기화가 안되고 독자적으로 Main 씬이 돌아가게 되는 것.
        // 호스트가 PhotonNetwork.LoadLevel 메소드를 실행하게 되면 참가자들도 같은 씬을 로드하게 된다.
        // 호스트와 참가자들의 Main 씬이 동기화된다.
        RoomPanel.SetActive(true);
        LobbyPanel.SetActive(false);
        MainMenuPanel.SetActive(false);

        RoomPanel.SetActive(true);
        RoomRenewal();

        ChatInput.text = "";
        for (int i = 0; i < ChatText.Length; i++) ChatText[i].text = "";
    }

    // 룸 초기화
    void RoomRenewal()
    {
        PlayerNickNameTexts[0].text = "";
        PlayerNickNameTexts[1].text = "";

        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
            PlayerNickNameTexts[i].text = PhotonNetwork.PlayerList[i].NickName;
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + newPlayer.NickName + "님이 참가하셨습니다</color>");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RoomRenewal();
        ChatRPC("<color=yellow>" + otherPlayer.NickName + "님이 퇴장하셨습니다</color>");
    }

    public void LoadGameScene()
    {
        MultiLoadingSceneController.LoadScene("MultiMainGameTest 1");
    }

    #endregion

    #region 채팅
    public void Send()
    {
        PV.RPC("ChatRPC", RpcTarget.All, PhotonNetwork.NickName + " : " + ChatInput.text);
        ChatInput.text = "";
    }

    [PunRPC]
    void ChatRPC(string msg)
    {
        bool isInput = false;
        for (int i = 0; i < ChatText.Length; i++)
            if (ChatText[i].text == "")
            {
                isInput = true;
                ChatText[i].text = msg;
                break;
            }
        if (!isInput) // 꽉차면 한칸씩 위로 올림
        {
            for (int i = 1; i < ChatText.Length; i++) ChatText[i - 1].text = ChatText[i].text;
            ChatText[ChatText.Length - 1].text = msg;
        }
    }
    #endregion
}
