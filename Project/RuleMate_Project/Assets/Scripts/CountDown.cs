using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text timeTxt;

    public GameObject May;
    public GameObject Brey;
    private Rigidbody MayR;
    private Rigidbody BreyR;

    // Start is called before the first frame update
    void Start()
    {
        //Time.timeScale = 0;
        MayR = May.gameObject.GetComponent<Rigidbody>();
        BreyR = Brey.gameObject.GetComponent<Rigidbody>();
        MayR.constraints = RigidbodyConstraints.FreezeAll;
        BreyR.constraints = RigidbodyConstraints.FreezeAll;
        CountStart();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("Timer").GetComponent<Timer>().limitTime = 90;
    }

    protected void CountStart()
    {
        StartCoroutine(CountDown3());
    }

    IEnumerator CountDown3()
    {
        timeTxt.text = "3";
        yield return new WaitForSeconds(1.0f);
        timeTxt.text = "2";
        yield return new WaitForSeconds(1.0f);
        timeTxt.text = "1";
        yield return new WaitForSeconds(1.0f);
        timeTxt.text = "START !";
        yield return new WaitForSeconds(0.5f);
        //Time.timeScale = 1;
        MayR.constraints = RigidbodyConstraints.None;
        BreyR.constraints = RigidbodyConstraints.None;
        gameObject.SetActive(false);
    }
}
