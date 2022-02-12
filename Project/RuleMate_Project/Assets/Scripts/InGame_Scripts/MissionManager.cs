using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != null) return;
        DontDestroyOnLoad(gameObject);
    }

    public Slider maySlider;
    public Slider breySlider;

    // 메이 리스트에는 메이가 정리해야 할 오브젝트들
    public List<GameObject> objMayMissionList;
    public List<GameObject> txtMayMissionList;

    // 브레이 리스트에는 브레이가 어지럽혀야 할 오브젝트들
    public List<GameObject> objBreyMissionList;
    public List<GameObject> txtBreyMissionList;

    public List<bool> isCleanList;
}
