using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class LocalGameManager : MonoBehaviour
{
    float mayPoint, breayPoint;
    
    // 싱글톤
    public static LocalGameManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        mayPoint = 0;
        breayPoint = 0;
    }

    public void MayPoint()
    {
        mayPoint += 1f;
        breayPoint -= 1f;
    }

    public void BreayPoint()
    {
        mayPoint -= 1f;
        breayPoint += 1f;
    }
}
