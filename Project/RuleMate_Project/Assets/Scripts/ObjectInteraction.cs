using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private bool isCorrectObj = true;
    private GameObject[] Tiles;

    // Start is called before the first frame update
    void Start()
    {
        Tiles = GameObject.FindGameObjectsWithTag("Tile");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("입장");
        if (isCorrectObj && col.gameObject.tag == "Player")
        {
            //for(int i=0; i<Tiles.Length; i++)
            Tiles[1].GetComponent<Animation>().Play();
        }
    }
}
