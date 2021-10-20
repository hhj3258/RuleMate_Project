using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterSelect : MonoBehaviourPunCallbacks
{
    RoomManager networkSelect;
    [SerializeField] Character character;

    private void Start()
    {
        networkSelect = FindObjectOfType<RoomManager>();
    }

    private void OnMouseUpAsButton()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        if (character == Character.Player1)
            networkSelect.isSelects[0] = networkSelect.isSelects[0] ? false : true;
        else if (character == Character.Player2)
            networkSelect.isSelects[1] = networkSelect.isSelects[1] ? false : true;

        networkSelect.ReadyCheck(character);
    }


}
