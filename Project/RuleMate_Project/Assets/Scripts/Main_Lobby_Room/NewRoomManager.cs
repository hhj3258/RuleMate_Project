using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class NewRoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] Button btnGameStart;
    [SerializeField] Image MayModel;
    [SerializeField] Image BreyModel;

    [SerializeField] Button btnMayReady;
    [SerializeField] Button btnBreyReady;

    [SerializeField] Image imgP1Char;
    [SerializeField] Image imgP2Char;

    bool isMayReady, isBreyReady;
    bool hostReady, guestReady;

    private void Update()
    {
        if (isMayReady || isBreyReady)
            hostReady = true;
        else
            hostReady = false;

        if (PhotonNetwork.IsMasterClient)
        {
            if (hostReady && guestReady)
                btnGameStart.interactable = true;
            else
                btnGameStart.interactable = false;
        }
    }

    public void OnClickMay()
    {
        if (isBreyReady)
            return;

        isMayReady = isMayReady ? false : true;
        MayModel.gameObject.SetActive(isMayReady);
        photonView.RPC("GuestMay", RpcTarget.Others, isMayReady);

        if (isMayReady)
            SelectManager.instance.currentCharacter = Character.Player1;
        else
            SelectManager.instance.currentCharacter = Character.None;

        SetPlayersCharacter();

        if (!PhotonNetwork.IsMasterClient)
            photonView.RPC("GuestReady", RpcTarget.Others, isMayReady, isBreyReady);
    }

    [PunRPC]
    void GuestMay(bool isReady)
    {
        btnMayReady.interactable = !isReady;

        if (PhotonNetwork.IsMasterClient)
        {
            imgP2Char.gameObject.SetActive(isReady);
            imgP2Char.sprite = Resources.Load<Sprite>("MayHead");
        }
        else
        {
            imgP1Char.gameObject.SetActive(isReady);
            imgP1Char.sprite = Resources.Load<Sprite>("MayHead");
        }
    }

    public void OnClickBrey()
    {
        if (isMayReady)
            return;

        isBreyReady = isBreyReady ? false : true;
        BreyModel.gameObject.SetActive(isBreyReady);
        photonView.RPC("GuestBrey", RpcTarget.Others, isBreyReady);

        if (isBreyReady)
            SelectManager.instance.currentCharacter = Character.Player2;
        else
            SelectManager.instance.currentCharacter = Character.None;

        SetPlayersCharacter();

        if (!PhotonNetwork.IsMasterClient)
            photonView.RPC("GuestReady", RpcTarget.Others, isMayReady, isBreyReady);
    }

    [PunRPC]
    void GuestBrey(bool isReady)
    {
        btnBreyReady.interactable = !isReady;

        if (PhotonNetwork.IsMasterClient)
        {
            imgP2Char.gameObject.SetActive(isReady);
            imgP2Char.sprite = Resources.Load<Sprite>("BreyHead");
        }
        else
        {
            imgP1Char.gameObject.SetActive(isReady);
            imgP1Char.sprite = Resources.Load<Sprite>("BreyHead");
        }
    }

    [PunRPC]
    void GuestReady(bool ready1, bool ready2)
    {
        if (ready1 || ready2)
            guestReady = true;
        else
            guestReady = false;
    }

    void SetPlayersCharacter()
    {
        if(PhotonNetwork.IsMasterClient)
        { 
            if(isMayReady)
            {
                imgP1Char.gameObject.SetActive(true);
                imgP1Char.sprite= Resources.Load<Sprite>("MayHead");
            }
            else if(isBreyReady)
            {
                imgP1Char.gameObject.SetActive(true);
                imgP1Char.sprite = Resources.Load<Sprite>("BreyHead");
            }
            else
            {
                imgP1Char.gameObject.SetActive(false);
            }
        }
        else
        {
            if (isMayReady)
            {
                imgP2Char.gameObject.SetActive(true);
                imgP2Char.sprite = Resources.Load<Sprite>("MayHead");
            }
            else if (isBreyReady)
            {
                imgP2Char.gameObject.SetActive(true);
                imgP2Char.sprite = Resources.Load<Sprite>("BreyHead");
            }
            else
            {
                imgP2Char.gameObject.SetActive(false);
            }
        }
    }

    // 방을 나갈 때, 들어올 때 호출
    void ClearRoom()
    {
        btnGameStart.interactable = false;
        isMayReady = false;
        isBreyReady = false;

        BreyModel.gameObject.SetActive(isBreyReady);
        btnMayReady.interactable = !isBreyReady;

        MayModel.gameObject.SetActive(isMayReady);
        btnBreyReady.interactable = !isMayReady;

        SelectManager.instance.currentCharacter = Character.None;

        guestReady = false;
        hostReady = false;

        imgP1Char.gameObject.SetActive(false);
        imgP2Char.gameObject.SetActive(false);
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
