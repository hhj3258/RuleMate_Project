using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    //float time;
    //public Text timeTxt;

    public Text txt;
    int cnt = 0;

    public GameObject May;
    public GameObject Brey;
    private Rigidbody MayR;
    private Rigidbody BreyR;

    public GameObject CountDown;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
        MayR = May.gameObject.GetComponent<Rigidbody>();
        BreyR = Brey.gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Timer").GetComponent<Timer>().limitTime = 90;
        MayR.constraints = RigidbodyConstraints.FreezeAll;
        BreyR.constraints = RigidbodyConstraints.FreezeAll;

        if (Input.GetMouseButtonDown(0))
        {
            cnt++;
        }
        switch (cnt)
        {
            case 1: 
                txt.text = "이동은 WASD로 합니다.";
                break;
            case 2:
                //Time.timeScale = 1;
                MayR.constraints = RigidbodyConstraints.None;
                BreyR.constraints = RigidbodyConstraints.None;
                CountDown.SetActive(true);
                gameObject.SetActive(false);
                break;
        }
    }
    
    /*
    IEnumerator CountDown()
    {
        timeTxt.text = "3";
        yield return new WaitForSeconds(1.0f);
        timeTxt.text = "2";
        yield return new WaitForSeconds(1.0f);
        timeTxt.text = "1";
        yield return new WaitForSeconds(1.0f);
        timeTxt.text = "START !";
        yield return new WaitForSeconds(1.0f);
        Time.timeScale = 1;
        MayR.constraints = RigidbodyConstraints.None;
        BreyR.constraints = RigidbodyConstraints.None;
        gameObject.SetActive(false);
    }
    */
}
