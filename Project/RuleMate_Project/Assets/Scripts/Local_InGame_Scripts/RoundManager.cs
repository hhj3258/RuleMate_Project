using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public GameObject May;
    public GameObject Brey;

    public Timer timer;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
    }
}
