using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MultiPlayerManager : PlayerManager
{
    void Start()
    {
        rigid = playerPrefab.GetComponent<Rigidbody>();
        //anim = playerPrefab.GetComponent<Animator>();
    }

    void Update()
    {
        if (!photonView.IsMine)
            return;

        //Jump();
        SetAnim();

        if (im.keyInterAction)
        {
            photonView.RPC("CatchOrRelease", RpcTarget.All);
        }
    }

    void FixedUpdate()
    {
        if (!photonView.IsMine)
            return;

        Move();
        Turn();
    }

    //protected override void SetAnim()
    //{
    //    if (im.h != 0 || im.v != 0)
    //        anim.SetBool("isMove", true);
    //    else
    //        anim.SetBool("isMove", false);
    //}
}
