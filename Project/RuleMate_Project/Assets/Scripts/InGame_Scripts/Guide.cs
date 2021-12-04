using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Guide : MonoBehaviour
{
    [SerializeField] string[] guides;

    public Text txt;
    int idx;

    private void Start()
    {
        idx = 0;

        if (LocalGameManager.instance.toDay > 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnClickGuide()
    {
        if(idx < guides.Length)
            txt.text = guides[idx++];
        else
            gameObject.SetActive(false);
    }
}
