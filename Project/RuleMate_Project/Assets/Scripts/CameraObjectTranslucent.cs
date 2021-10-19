using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObjectTranslucent : MonoBehaviour
{
    Renderer ObstacleRenderer;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(transform.position, player.transform.position);
        Vector3 Direction = (player.transform.position - transform.position).normalized;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, Direction, out hit, Distance))
        {
            //Renderer 가져오기
            ObstacleRenderer = hit.transform.GetComponentInChildren<Renderer>();
            if (ObstacleRenderer != null)
            {
                //Metrial 반투명
                Material Mat = ObstacleRenderer.material;
                Color matColor = Mat.color;
                matColor.a = 0.35f;
                Mat.color = matColor;
            }
        }
    }
}
