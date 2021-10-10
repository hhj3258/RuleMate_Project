using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PlayerManager : MonoBehaviourPun
{
    [SerializeField] protected GameObject playerPrefab;
    protected Animator anim;

    protected Rigidbody rigid;
    Vector3 movement;

    [SerializeField] protected InputManager im;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotSpeed = 10f;
    [SerializeField] float jumpPower = 5f;

    bool isJumping = false; // 점프 가능 여부

    bool isPickNow = false; // 물체 잡았는지 여부
    Collider objCol = null; // 잡은 물체 저장

    protected void Move()
    {
        movement = new Vector3(im.h, 0, im.v);
        movement = movement.normalized * moveSpeed * Time.deltaTime;
        rigid.MovePosition(playerPrefab.transform.position + movement);
    }

    protected void Turn()
    {
        if (im.h == 0 && im.v == 0)
        {
            return;
        }

        Quaternion newRotation = Quaternion.LookRotation(movement);
        rigid.rotation = Quaternion.Slerp(rigid.rotation, newRotation, rotSpeed * Time.deltaTime);
    }

    protected void Jump()
    {
        // 점프키를 눌렀으며 착지하지 않았다면
        if (im.keyJump && !isJumping)
        {
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isJumping = true;
        }
    }

    // 물체 잡기, 놓기 시 오브젝트 세팅

    void ObjSetting(bool setBool)
    {
        if (setBool)
        {
            // 잡기 시 물체 위치 설정
            objCol.transform.localPosition = new Vector3(0, 0.7f, 0.7f);
        }

        // 리지드바디 물리력 유무
        objCol.GetComponent<Rigidbody>().isKinematic = setBool;

        // 콜라이더 활성 유무
        Collider[] cols = objCol.GetComponents<Collider>();
        foreach (var col in cols)
        {
            if (setBool)
                col.enabled = false;
            else
                col.enabled = true;
        }

        // 물체를 잡았는지 판단
        isPickNow = isPickNow ? false : true;
    }

    // 잡기 or 놓기
    [PunRPC]
    protected void CatchOrRelease()
    {
        FrontRay();
        Debug.Log("chk");
        if (objCol)
        {
            if (!isPickNow)
            {
                // 잡을 시 부모 오브젝트를 플레이어로
                objCol.transform.SetParent(playerPrefab.transform);
                ObjSetting(true);
            }
            else
            {
                // 놓을 시 부모 오브젝트를 null
                objCol.transform.SetParent(null);
                ObjSetting(false);
                // objCol도 비워줌
                objCol = null;
            }
        }
    }

    RaycastHit hit;
    public float rayDistance = 1f;
    // 플레이어 정면에 Ray를 쏴서 상호작용 가능한 오브젝트가 있는지 판별
    protected void FrontRay()
    {
        Transform pt = playerPrefab.transform;
        Debug.DrawRay(pt.position, pt.forward * rayDistance, Color.blue, 0.3f);

        if (Physics.Raycast(pt.position, transform.forward, out hit, rayDistance))
        {// 현재 물체를 잡지 않았고 오브젝트 태그가 InterActionObj이면 objCol 세팅
            if (!isPickNow && hit.transform.CompareTag("InterActionObj"))
            {
                objCol = hit.transform.GetComponent<Collider>();
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isJumping = false;  // 오브젝트에 닿으면 점프 가능
    }

    protected abstract void SetAnim();
}
