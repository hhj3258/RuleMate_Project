using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    // 싱글톤
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    private static GameManager instance;

    public Transform[] spawnPositions;
    public GameObject playerPrefab;

    // 플레이어 1,2가 각각 실행
    private void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];

        // 입력으로 들어온 프리팹을 나 자신의 세상에서 먼저 생성 -> 로컬 플레이어
        // 그다음, 접속된 다른 플레이어의 세상에도 '리모트 플레이어'로써 나의 레플리카를 생성
        // 생성하려는 프리팹은 Resources 폴더에 있어야 함.
        playerPrefab = PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, spawnPosition.rotation);
    }

    // 나 자신이 방에서 떠났을 때 자동 실행
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }
}
