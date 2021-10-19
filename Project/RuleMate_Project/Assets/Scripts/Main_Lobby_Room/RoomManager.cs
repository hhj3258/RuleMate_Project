using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [HideInInspector] public bool[] isSelects;
    [SerializeField] GameObject[] players;
    [SerializeField] Image[] imgChks;
    [SerializeField] Button btnGameStart;

    private void Start()
    {
        isSelects = new bool[2];
        btnGameStart.interactable = false;
    }

    private void Update()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        if (!isSelects[0] || !isSelects[1])
            btnGameStart.interactable = false;
        else
            btnGameStart.interactable = true;
    }

    public void ReadyCheck(Character character)
    {
        Debug.Log(character);

        if (character == Character.Player1)
        {
            if (isSelects[0])
            {
                SelectManager.instance.currentCharacter = Character.Player1;
                players[1].GetComponent<Collider>().enabled = false;
                // 모두에게 캐릭터 선택했음을 알림
                photonView.RPC("OnClickReady", RpcTarget.All, 0, true);
                // 나 이외의 참가자에게 내가 선택한 캐릭터의 콜라이더를 비활성화하라고 알림
                photonView.RPC("NetworkColSwitch", RpcTarget.Others, 0, false);
            }
            else if (!isSelects[0])
            {
                SelectManager.instance.currentCharacter = Character.None;
                if (!isSelects[1]) players[1].GetComponent<Collider>().enabled = true;
                photonView.RPC("OnClickReady", RpcTarget.All, 0, false);

                if (imgChks[1].gameObject.activeSelf == false)
                    photonView.RPC("NetworkColSwitch", RpcTarget.Others, 0, true);
            }
        }

        if (character == Character.Player2)
        {
            if (isSelects[1])
            {
                SelectManager.instance.currentCharacter = Character.Player2;
                players[0].GetComponent<Collider>().enabled = false;
                photonView.RPC("OnClickReady", RpcTarget.All, 1, true);
                photonView.RPC("NetworkColSwitch", RpcTarget.Others, 1, false);
            }
            else if (!isSelects[1])
            {
                SelectManager.instance.currentCharacter = Character.None;
                if (!isSelects[0]) players[0].GetComponent<Collider>().enabled = true;
                photonView.RPC("OnClickReady", RpcTarget.All, 1, false);

                if (imgChks[0].gameObject.activeSelf == false)
                    photonView.RPC("NetworkColSwitch", RpcTarget.Others, 1, true);
            }
        }

        if (!isSelects[0] && !isSelects[1])
            SelectManager.instance.currentCharacter = Character.None;

        if (isSelects[0] && isSelects[1])
        {
            if (character == Character.Player1)
                players[0].GetComponent<Collider>().enabled = true;
            else if (character == Character.Player2)
                players[1].GetComponent<Collider>().enabled = true;
        }
    }

    // 모두에게 캐릭터 선택했음을 알림
    [PunRPC]
    protected void OnClickReady(int p_num, bool isReady)
    {
        players[p_num].GetComponent<Animator>().SetBool("isSelected", isReady);
        imgChks[p_num].gameObject.SetActive(isReady);

        isSelects[p_num] = isReady;
    }

    [PunRPC]
    void NetworkColSwitch(int p_num, bool isActive)
    {
        players[p_num].GetComponent<Collider>().enabled = isActive;
    }

    void ClearRoom()
    {
        isSelects[0] = false;
        isSelects[1] = false;

        ReadyCheck(Character.Player1);
        ReadyCheck(Character.Player2);
    }

    // 내가 떠날 때
    public override void OnLeftRoom() => ClearRoom();

    // 참가자가 떠날 때
    public override void OnPlayerLeftRoom(Player otherPlayer) => ClearRoom();

    // 참가자가 들어왔을 때
    public override void OnPlayerEnteredRoom(Player newPlayer) => ClearRoom();

    // 내가 들어왔을 때
    public override void OnJoinedRoom() => ClearRoom();
}
