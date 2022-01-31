using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissonController : MonoBehaviour
{
    private MissionManager missionMgr;
    Slider missonSlider;
    InputManager im;

    [SerializeField] int missionNumber;
    float sliderProgress;
    string nowPlayerName;

    void Start()
    {
        missionMgr = transform.parent.GetComponent<MissionManager>();
        sliderProgress = 0f;
        nowPlayerName = "";

        // 모든 미션의 초기값은 깨끗이 정리된 상태
        if (missionMgr.objMayMissionList[missionNumber] != null)
            missionMgr.objMayMissionList[missionNumber].SetActive(false);
        if (missionMgr.objBreyMissionList[missionNumber] != null)
            missionMgr.objBreyMissionList[missionNumber].SetActive(true);

        missionMgr.txtMayMissionList[missionNumber].SetActive(false);
        missionMgr.txtBreyMissionList[missionNumber].SetActive(true);
    }

    private void Update()
    {
        if (im == null)
            return;

        missonSlider.value = sliderProgress;

        if (im.keyInterAction && LocalGameManager.instance.isStart)
        {
            sliderProgress += 0.2f;
        }
        else
        {
            if (sliderProgress >= 0)
                sliderProgress -= 0.001f;
        }

        if (sliderProgress >= 1f)
        {
            MissonComplete();
        }
    }

    private void MissonComplete()
    {
        // 메이가 완료했을 때와 브레이가 완료했을 때
        if (nowPlayerName == "May" && missionMgr.isCleanList[missionNumber] == false)
        {
            if (missionMgr.objMayMissionList[missionNumber] != null)
            {
                missionMgr.objMayMissionList[missionNumber].SetActive(false);

                if (missionMgr.objBreyMissionList[missionNumber] != null)
                    missionMgr.objBreyMissionList[missionNumber].SetActive(true);
            }

            missionMgr.isCleanList[missionNumber] = true;
            missionMgr.txtBreyMissionList[missionNumber].SetActive(true);
            missionMgr.txtMayMissionList[missionNumber].SetActive(false);
        }
        else if (nowPlayerName == "Brey" && missionMgr.isCleanList[missionNumber] == true)
        {
            if (missionMgr.objMayMissionList[missionNumber] != null)
            {
                missionMgr.objMayMissionList[missionNumber].SetActive(true);

                if (missionMgr.objBreyMissionList[missionNumber] != null)
                    missionMgr.objBreyMissionList[missionNumber].SetActive(false);
            }
            else
                missionMgr.objBreyMissionList[missionNumber].SetActive(true);

            missionMgr.isCleanList[missionNumber] = false;
            missionMgr.txtBreyMissionList[missionNumber].SetActive(false);
            missionMgr.txtMayMissionList[missionNumber].SetActive(true);
        }

        // 미션 완료했으니 기본값 복원
        MissonCleaner();
    }

    private void MissonCleaner()
    {
        sliderProgress = 0f;

        if (missonSlider != null)
            missonSlider.gameObject.SetActive(false);

        im = null;
        nowPlayerName = "";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (nowPlayerName != "")
            return;

        if (other.name == "May" && missionMgr.isCleanList[missionNumber] == false)
        {
            im = other.GetComponent<InputManager>();
            missonSlider = missionMgr.maySlider;
            nowPlayerName = "May";

            if (missionMgr.isCleanList[missionNumber] == false)
                missonSlider.gameObject.SetActive(true);
        }
        else if (other.name == "Brey" && missionMgr.isCleanList[missionNumber] == true)
        {
            im = other.GetComponent<LocalInputManager>();
            missonSlider = missionMgr.breySlider;
            nowPlayerName = "Brey";

            if (missionMgr.isCleanList[missionNumber] == true)
                missonSlider.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == nowPlayerName)
        {
            MissonCleaner();
            missonSlider = null;
        }
    }
}