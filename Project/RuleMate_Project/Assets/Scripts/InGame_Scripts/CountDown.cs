using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CountDown : MonoBehaviour
{
    public Text timeTxt;
    float nowTime;
    int realTime;
    int cnt;
    Sequence mySequence;

    private void OnEnable()
    {
        cnt = 3;

        transform.localScale = Vector3.zero;
        transform.rotation = new Quaternion(0f, 0f, 180f, 0f);
        mySequence = DOTween.Sequence().SetLoops(4);
        mySequence.Join(transform.DORotate(new Vector3(0f, 0f, -360f), 1f, RotateMode.FastBeyond360))
                            .Join(transform.DOScale(1, 1f).SetEase(Ease.OutExpo));
    }

    private void Update()
    {
        nowTime -= Time.deltaTime;
        realTime = (int)nowTime + 3;

        CountStart(realTime);
    }

    public void CountStart(int t)
    {
        if (t != cnt)
            return;

        if (t >= 1)
            timeTxt.text = t + "";
        else if (t == 0)
            timeTxt.text = "START !";
        
        cnt--;

        if (t < 0)
        {
            LocalGameManager.instance.isStart = true;
            GameObject.Find("Timer").GetComponent<Timer>().IsGameStart = true;
            gameObject.SetActive(false);
        }
    }
}
