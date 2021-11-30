using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    /*
    public GameObject mayScoreObj;
    public GameObject breyScoreObj;
    TextMeshProUGUI mayScoreTextMesh;
    TextMeshProUGUI breyScoreTextMesh;
    */
    public Text mayScoreTxt;
    public Text breyScoreTxt;

    protected int mayScore = 0;
    protected int breyScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        mayScore = 0;
        breyScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void MayScoreUp()
    {
        mayScore++;
        mayScoreTxt.text = mayScore + "";
    }

    protected void BreyScoreUp()
    {
        breyScore++;
        breyScoreTxt.text = breyScore + "";
    }
}
