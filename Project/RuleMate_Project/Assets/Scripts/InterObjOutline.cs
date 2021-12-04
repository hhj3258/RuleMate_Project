using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterObjOutline : MonoBehaviour
{
    Outline outline;
    float width = 0f;
    public float maxWidth = 5;

    // Start is called before the first frame update
    void Start()
    {
        outline = GetComponent<Outline>();
        outline.OutlineWidth = 2;
        outline.OutlineColor = new Color(0.37f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        width = Mathf.PingPong(Time.time * 3, maxWidth);
        outline.OutlineWidth = width;
    }
}
