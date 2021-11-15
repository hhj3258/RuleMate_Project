using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour
{
    public Text timeTxt;
    float nowTime;
    InGameManager inGameManager;

    private void Awake()
    {
        inGameManager = FindObjectOfType<InGameManager>();
    }

    void Start()
    {
        CountStart();
    }

    private void Update()
    {
        nowTime -= Time.deltaTime;
        CountStart();
    }

    public void CountStart()
    {
        float realTime = nowTime + 4f;

        if (realTime >= 1f)
            timeTxt.text = (int)realTime + "";
        else
            timeTxt.text = "START !";

        if (realTime <= 0f)
        {
            inGameManager.May.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            inGameManager.Brey.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            GameObject.Find("Timer").GetComponent<Timer>().IsGameStart = true;
            gameObject.SetActive(false);
        }
    }
}
