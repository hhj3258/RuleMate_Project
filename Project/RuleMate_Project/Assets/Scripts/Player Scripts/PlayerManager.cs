using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public abstract class PlayerManager : MonoBehaviourPun
{
    protected GameObject playerPrefab;
    protected Animator anim;

    protected Rigidbody rigid;
    Vector3 movement;

    [SerializeField] protected InputManager im;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotSpeed = 10f;
    //[SerializeField] float jumpPower = 5f;

    //bool isJumping = false; // 점프 가능 여부

    bool isPickNow = false; // 물체 잡았는지 여부
    Collider objCol = null; // 잡은 물체 저장

    public ObjectMonitor objectMonitor;

    private void Awake()
    {
        playerPrefab = this.gameObject;
        anim = playerPrefab.GetComponent<Animator>();
    }

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

    // 물체 잡기, 놓기 시 오브젝트 세팅

    void ObjSetting(bool setBool)
    {
        if (setBool)
        {
            // 잡기 시 물체 위치 설정
            objCol.transform.localPosition = new Vector3(0, 0f, 0.8f);
        }

        // 리지드바디 물리력 유무
        objCol.GetComponent<Rigidbody>().isKinematic = setBool;

        // 콜라이더 활성 유무
        Collider col = objCol.GetComponent<Collider>();
        if (setBool)
            col.enabled = false;
        else
            col.enabled = true;

        // 물체를 잡았는지 판단
        isPickNow = isPickNow ? false : true;
    }

    // 잡기 or 놓기
    [PunRPC]
    protected void CatchOrRelease()
    {
        FrontRay();

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
                if (objectMonitor)
                    objCol.GetComponent<ObjectInitSetting>().ObjCleaning();

                // 놓을 시 부모 오브젝트를 null
                objCol.transform.SetParent(null);
                ObjSetting(false);
                objCol = null;  // objCol도 비워줌
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
        {
            // 현재 물체를 잡지 않았고 오브젝트 태그가 InterActionObj이면 objCol 세팅
            if (!isPickNow && hit.transform.CompareTag("InterActionObj"))
            {
                objCol = hit.transform.GetComponent<Collider>();
            }
        }
    }

    protected void SetAnim()
    {
        if (im.h != 0 || im.v != 0)
            anim.SetBool("isMove", true);
        else
            anim.SetBool("isMove", false);
    }

    float delayTime = 0f;
    void SetContinuousAnim()
    {
        if (im.keyInterAction)
        {
            anim.SetBool("isInterActive", true);
            delayTime = 0f;
        }
        else
        {
            delayTime += Time.deltaTime;
            if (delayTime >= 0.3f)
                anim.SetBool("isInterActive", false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("ContinuousInterActiveObj"))
        {
            SetContinuousAnim();
        }
    }
}
