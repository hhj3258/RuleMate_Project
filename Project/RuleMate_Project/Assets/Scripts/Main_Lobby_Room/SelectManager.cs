using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Character // 열거형으로 캐릭터들을 정의
{
    Player1,
    Player2,
    None
}

public class SelectManager : MonoBehaviour
{
    // 싱글톤
    public static SelectManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    // 현재 내가 선택한 캐릭터의 열거형 변수를 넘겨줄 것
    public Character currentCharacter;

    public Character localPlayer1;
    public Character localPlayer2;
}
