using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private float limitTime = 90f;

    private float minute;
    private float second;

    public Text timer;

    //나중에 쓸꺼
    //private bool timeOver = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        limitTime -= Time.deltaTime;
        minute = (limitTime % 3600) / 60;
        second = (limitTime % 3600) % 60;

        timer.text = ((int)minute + " : " + (int)second).ToString();

        //if (limitTime <= 0)
            //timeOver = true;
    }
}
