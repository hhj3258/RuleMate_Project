using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerManager : MonoBehaviour
{
    GameObject playerPrefab;
    Rigidbody rigid;
    Vector3 movement;
    Animator anim;

    float moveSpeed = 5f;
    float rotSpeed = 10f;
    float h, v;

    public void Start()
    {
        playerPrefab = GameObject.Find("TestPlayer1");
        rigid = playerPrefab.GetComponent<Rigidbody>();
        anim = playerPrefab.GetComponent<Animator>();
    }

    protected void Update()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        if (h != 0 || v != 0)
            anim.SetBool("isMove", true);
        else
            anim.SetBool("isMove", false);
    }

    protected void FixedUpdate()
    {
        Move();
        Turn();
    }

    void Move()
    {
        movement.Set(h, 0, v);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        rigid.MovePosition(playerPrefab.transform.position + movement);
    }

    void Turn()
    {
        if (h == 0 && v == 0)
            return;

        Quaternion newRotation = Quaternion.LookRotation(movement);
        rigid.rotation = Quaternion.Slerp(rigid.rotation, newRotation, rotSpeed * Time.deltaTime);
    }
}
