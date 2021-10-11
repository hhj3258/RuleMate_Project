using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private Animation ani;
    private bool isTrue = true;

    [SerializeField]
    private string cname;

    // Start is called before the first frame update
    void Start()
    {
        ani = this.GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //맞는 타일인지 확인
    protected void CheckObj(string name)
    {
        Debug.Log("체크" + name +" / " + cname + " / " );
        if (true)
        {
            Flip();
        }
    }

    //뒤집는 애니메이션 플레이
    protected void Flip()
    {
        Debug.Log("회전");
        ani.Play();
    }
}
