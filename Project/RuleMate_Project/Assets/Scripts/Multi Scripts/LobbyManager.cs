using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine;

// MonoBehaviourPunCallbacks 는 MonoBehaviour를 상속받고 있다.
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // 멀티플레이 시에 게임 버전이 같아야 함.
    private readonly string gameVersion = "1";

    public Text connectionInfoText;
    public Button joinButton;
    public Text inputfield;

    private void Start()
    {
        PhotonNetwork.GameVersion = gameVersion;
        
        // 마스터서버 접속 시도
        // 지금은 게임버전만 넘겨주지만 이외에도 여러가지를 넘겨줄 수 있다.
        PhotonNetwork.ConnectUsingSettings();

        joinButton.interactable = false;
        connectionInfoText.text = "Connecting To Master Server...";
    }

    // 마스터서버 접속을 성공하면 자동적으로 메소드가 실행된다.
    public override void OnConnectedToMaster()
    {
        joinButton.interactable = true;
        connectionInfoText.text = "Online : Connected to Master Server";
    }

    // 접속을 시도했지만 실패한 경우나 접속중에 끊어진 경우에 자동으로 실행된다.
    // DisconnectCause cause : 커넥팅이 끊긴 사유
    public override void OnDisconnected(DisconnectCause cause)
    {
        joinButton.interactable = false;
        connectionInfoText.text = $"Offline : Connection Disabled {cause.ToString()} - Try reconnecting...";

        // 재접속 시도
        // 접속에 성공할 때까지 계속 재접속을 시도하게 된다.
        PhotonNetwork.ConnectUsingSettings();
    }

    // Join 버튼 클릭 이벤트
    public void Connect()
    {
        joinButton.interactable = false;

        // 서버와 연결이 되어 있다면
        if (PhotonNetwork.IsConnected)
        {
            // 방을 검색하고 있다는 문구 출력
            connectionInfoText.text = "Connecting to Random Room...";
            // 매치메이킹 서버에서 감지한 랜덤룸에 자동 접속 시도
            // 빈 방이 없을 때는 당연히 실패 -> OnJoinRandomFailed 자동 실행

            PhotonNetwork.NickName = inputfield.text;

            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = $"Offline : Connection Disabled - Try reconnecting...";

            // 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
        
    }

    // 빈 방 찾기 실패했을 때 자동 실행
    // 다른 이유로 실행되는 경우도 있음
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        connectionInfoText.text = "There is no empty room, Creating new Room.";
        // 여기서는 방이 없어서 실패했을 경우가 대부분이므로 방을 새로 만든다.
        // 방 이름 = null, 방 옵션 = 최대 플레이어 2명
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 2 });
    }

    // 방에 참가 완료했을 때 자동 실행
    public override void OnJoinedRoom()
    {
        connectionInfoText.text = "Connected with Room";

        // SceneManager.LoadScene() 을 통해 씬을 이동하면 안됨!
        // 로드 씬을 통해서 씬을 넘어가게 되면, 나 혼자만 넘어가고 다른 참가자들은 넘어가지 않는다.
        // 2명의 플레이어가 로드 씬을 각각 실행하게 되면 제각각 Main 씬으로 넘어가게 된다.
        // 동기화가 안되고 독자적으로 Main 씬이 돌아가게 되는 것.
        // 호스트가 PhotonNetwork.LoadLevel 메소드를 실행하게 되면 참가자들도 같은 씬을 로드하게 된다.
        // 호스트와 참가자들의 Main 씬이 동기화된다.
        PhotonNetwork.LoadLevel("MultiMainGameTest");

    }
}