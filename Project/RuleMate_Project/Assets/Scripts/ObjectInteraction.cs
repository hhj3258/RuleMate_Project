using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    private bool isCorrectObj = true;
    public GameObject[] Tiles;

    private string r2b;
    private bool tf = true;

    // Start is called before the first frame update
    void Start()
    {
        //Tiles = GameObject.FindGameObjectsWithTag("Tile");
        //Debug.Log("tiles num : " + Tiles.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ChangeTile()
    {
        if (tf)
        {
            r2b = "RedtoBlue";
            tf = false;
        }
        else
        {
            r2b = "BluetoRed";
            tf = true;
        }

        if (isCorrectObj)
        {
            for(int i=1; i<Tiles.Length; i++)
                Tiles[i].GetComponent<Animation>().Play(r2b);
        }
    }
}
