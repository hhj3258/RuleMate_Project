using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public Text mayScoreTxt;
    public Text breyScoreTxt;

    int mayScore = 1;
    int breyScore = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mayScoreTxt.text = mayScore.ToString();
        breyScoreTxt.text = breyScore.ToString();
    }

    protected void MayScoreUp()
    {
        mayScore += 1;
    }

    protected void BreyScoreUp()
    {
        breyScore += 1;
    }
}
