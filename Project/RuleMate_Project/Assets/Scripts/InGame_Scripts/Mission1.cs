using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mission1 : MonoBehaviour
{
    //물기제거_메이

    public GameObject waterBottom;
    public GameObject Messege;
    public GameObject tempMessege;
    //public GameObject missionSection;

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
        waterBottom.SetActive(true);
        Messege.SetActive(true);
        slider.minValue = progressMIN;
        slider.maxValue = progressMAX;
        slider.value = progress;

        ani = new Animation[Tiles.Length];
        for(int i=0; i<Tiles.Length; i++)
        {
            ani[i] = Tiles[i].GetComponent<Animation>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = progress;

        if (Input.GetKeyDown(KeyCode.Z) && isMissioned)
        {
            progress += 20f;
            Debug.Log("눌렀음 " + progress);
        }
        else
        {
            if (progress >= 0)
                progress -= 0.10f;
        }

        if (progress >= 100)
        {
            //포인트올리기 추가
            MissionClear();
            waterBottom.SetActive(false);
            Messege.SetActive(false);
            tempMessege.SetActive(true);
            slider.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            slider.gameObject.SetActive(true);
            isMissioned = true;
        }
        //Debug.Log("들어옴");
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
        //브레이 미션 활성화하기
        //활성화 된 브레이미션 미션리스트에 추가하기

        for (int i = 0; i < Tiles.Length; i++)
        {
            ani[i].Play();
            //Tiles[i].GetComponent<Animation>().Play();
            StartCoroutine(wait1sec());
        }
    }

    IEnumerator wait1sec()
    {
        yield return new WaitForSeconds(0.3f);
    }
}
