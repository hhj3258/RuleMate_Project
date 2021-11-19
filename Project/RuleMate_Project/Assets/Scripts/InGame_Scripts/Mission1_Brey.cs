using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission1_Brey : MonoBehaviour
{
    //물기제거미션_브레이

    public GameObject waterBottom;
    public GameObject thisMissionMsg;
    public GameObject nextMissionMsg;

    public GameObject thisMission;
    public GameObject NextMission;

    public Slider slider;
    float progress = 0;
    float progressMIN = 0;
    float progressMAX = 100;
    bool isMissioned = false;

    public GameObject[] Tiles;
    private Animation[] ani;

    // Start is called before the first frame update
    void Start()
    {
        waterBottom.SetActive(false);
        thisMissionMsg.SetActive(true);

        slider.minValue = progressMIN;
        slider.maxValue = progressMAX;
        slider.value = progress;
        progress = 0;

        ani = new Animation[Tiles.Length];
        for (int i = 0; i < Tiles.Length; i++)
            ani[i] = Tiles[i].GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = progress;

        if (Input.GetKeyDown(KeyCode.M) && isMissioned)
        {
            progress += 20f;
        }
        else
        {
            if (progress >= 0)
                progress -= 0.10f;
        }

        if (progress >= 100)
        {
            MissionClear();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player02")
        {
            slider.gameObject.SetActive(true);
            isMissioned = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        slider.gameObject.SetActive(false);
        progress = 0;
        isMissioned = false;
    }

    void MissionClear()
    {
        //포인트 올라가기
        //메이 미션 활성화하기
        //활성화 된 메이미션 미션리스트에 추가하기

        //waterBottom.SetActive(true);

        thisMissionMsg.SetActive(false);
        nextMissionMsg.SetActive(true);
        slider.gameObject.SetActive(false);
        progress = 0;

        for (int i = 0; i < Tiles.Length; i++)
        {
            ani[i].Play();
            //Tiles[i].GetComponent<Animation>().Play();
            StartCoroutine(wait1sec());
        }

        NextMission.SetActive(true);
        thisMission.SetActive(false);
    }

    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
