using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineTest : MonoBehaviour
{
    Outline outline;
    float width = 0f;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        
    }

    // Update is called once per frame
    void Update()
    {
        width = Mathf.PingPong(Time.time * 3, 5);
        outline.OutlineWidth = width;
    }
}
