using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public GameObject May;
    public GameObject Brey;

    public Timer timer;

    void Start()
    {
        May.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Brey.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        timer = FindObjectOfType<Timer>();
    }

}
