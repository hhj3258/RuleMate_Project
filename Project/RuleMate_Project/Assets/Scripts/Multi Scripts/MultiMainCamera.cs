using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiMainCamera : MainCamera
{
    private void Start()
    {
        player = GameManager.Instance.playerPrefab;
    }
}
