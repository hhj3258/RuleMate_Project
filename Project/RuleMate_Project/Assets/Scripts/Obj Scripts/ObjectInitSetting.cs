using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInitSetting : MonoBehaviour
{
    public string mySectionName;
    public string nowSectionName;
    Vector3 pos;
    Quaternion rot;
    
    void Start()
    {
        name = gameObject.name;
        pos = transform.position;
        rot = transform.rotation;
    }

    public void ObjCleaning()
    {
        if (mySectionName != nowSectionName)
            return;

        transform.position = pos;
        transform.rotation = rot;
    }

}
