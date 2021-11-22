using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player02 : PlayerManager
{
    void Start()
    {
        rigid = playerPrefab.GetComponent<Rigidbody>();
        anim = playerPrefab.GetComponent<Animator>();
    }

    void Update()
    {
        SetAnim();

        if (im.keyCatchOrRelease)
        {
            //photonView.RPC("CatchOrRelease", RpcTarget.All);
            CatchOrRelease();
        }
    }

    void FixedUpdate()
    {
        Move();
        Turn();
    }

    protected override void SetAnim()
    {
        if (im.h != 0 || im.v != 0)
            anim.SetBool("isMove", true);
        else
            anim.SetBool("isMove", false);
    }
}
