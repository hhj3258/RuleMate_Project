using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListOverlay : MonoBehaviour
{
    public void OnListMouseEnter()
    {
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void OnListMouseExit()
    {
        transform.GetChild(3).gameObject.SetActive(false);
    }
}
