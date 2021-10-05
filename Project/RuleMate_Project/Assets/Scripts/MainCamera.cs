using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public GameObject player;

    //카메라 시작 위치
    public float offsetX = 2.5f;
    public float offsetY = 4.5f;
    public float offsetZ = -4.7f;
    public float followSpeed = 4.2f;

    //카메라 이동 할 위치
    Vector3 cameraPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        //캐릭터 이동한 만큼 카메라 이동 값 저장
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        //카메라 이동
        transform.position = cameraPosition;

        //카메라 딜레이 이동
        //transform.position = Vector3.Lerp(transform.position, cameraPosition, followSpeed * Time.deltaTime);
    }
}
