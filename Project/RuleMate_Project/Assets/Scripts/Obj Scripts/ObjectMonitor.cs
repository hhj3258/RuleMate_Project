using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMonitor : MonoBehaviour
{
    LocalGameManager localGameManager;
    public bool isIn = false;
    List<GameObject> objs;

    private void Start()
    {
        objs = new List<GameObject>();
        isIn = false;
        localGameManager = LocalGameManager.instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isTrue = false;
        if (other.tag == "InterActionObj")
        {
            for (int i = 0; i < objs.Count; i++)
            {
                if (other.gameObject == objs[i])
                {
                    isTrue = true;
                    break;
                }
            }

            if (!isTrue)
            {
                objs.Add(other.gameObject);
                //localGameManager.MayPoint();
            }
        }

        if (other.tag == "Player")
            other.GetComponent<PlayerManager>().objectMonitor = this;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "InterActionObj")
            other.GetComponent<ObjectInitSetting>().nowSectionName = gameObject.name;
    }

    private void OnTriggerExit(Collider other)
    {
        if (objs.Contains(other.gameObject))
        {
            objs.Remove(other.gameObject);
           // localGameManager.BreayPoint();
        }

        if (other.tag == "Player")
            other.GetComponent<PlayerManager>().objectMonitor = null;

        if (other.tag == "InterActionObj")
            other.GetComponent<ObjectInitSetting>().nowSectionName = "";
    }
}
