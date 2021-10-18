using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMouseCursor : MonoBehaviour
{
    //마우스 포인터로 사용할 텍스처를 입력받습니다.
    public Texture2D cursorTexture;
    //텍스처의 중심을 마우스 좌표로 할 것인지 체크박스로 입력받습니다.
    public bool hotSpotIsCenter = false;
    //텍스처의 어느부분을 마우스의 좌표로 할 것인지 텍스처의
    //좌표를 입력받습니다.
    public Vector2 adjustHotSpot = Vector2.zero;
    //내부에서 사용할 필드를 선업합니다.
    private Vector2 hotSpot;

    public void Start()
    {
        //코루틴을 사용합니다. TargetCursor()함수를 호출합니다.
        StartCoroutine("MyCursor");
    }

    //MyCursor()라는 이름의 코루틴이 시작됩니다.
    IEnumerator MyCursor()
    {
        //모든 렌더링이 완료될 때까지 대기할테니 렌더링이 완료되면
        //깨워 달라고 유니티 엔진에 게 부탁하고 대기합니다.
        yield return new WaitForEndOfFrame();

        //텍스처의 중심을 마우스의 좌표로 사용하는 경우
        //텍스처의 폭과 높이의 1/2을 hot Spot 좌표로 입력합니다.
        if (hotSpotIsCenter)
        {
            hotSpot.x = cursorTexture.width / 2;
            hotSpot.y = cursorTexture.height / 2;
        }
        else
        {
            //중심을 사용하지 않을 경우 Adjust Hot Spot으로 입력 받은
            //것을 사용합니다.
            hotSpot = adjustHotSpot;
        }
        //이제 새로운 마우스 커서를 화면에 표시합니다.
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }
}
