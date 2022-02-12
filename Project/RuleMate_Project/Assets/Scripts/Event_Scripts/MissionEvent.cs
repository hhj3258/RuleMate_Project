using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MissionEvent : MonoBehaviour
{
    int[] MissionNumber;
    bool isMission;

    public List<string> May;
    public Text[] MayMission;

    public List<string> Brey;
    public Text[] BreyMission;

    void Start()
    {
        MissionNumber = new int[3];
        isMission = false;
        MissionList();
    }

    void MissionList()
    {
        StreamReader sr = new StreamReader(Application.dataPath + "/" + "Resources" + "/" + "EventImageCSV" + "/" + "Mission.csv");

        bool EndOfFile = false;
        while (!EndOfFile)
        {
            string MissionString = sr.ReadLine();

            if (MissionString == null)
            {
                EndOfFile = true;
                break;
            }
            var MissionValue = MissionString.Split(',');

            for (int i = 0; i < MissionValue.Length; i++)
            {
                if (i % 2 == 0)
                {
                    May.Add(MissionValue[i]);
                }
                else
                {
                    Brey.Add(MissionValue[i]);
                }
            }
        }
        RandomMission();
    }

    void RandomMission()
    {
        for (int i = 0; i < 3; i++)
        {
            MissionNumber[i] = Random.Range(0, May.Count);
        }
        
        if (MissionNumber[0] == MissionNumber[1]) 
        { 
            MissionNumber[1] = Random.Range(0, May.Count); 
        }

        if (MissionNumber[0] == MissionNumber[2])
        { 
            MissionNumber[2] = Random.Range(0, May.Count);
        }

        if (MissionNumber[1] == MissionNumber[2])
        { 
            MissionNumber[2] = Random.Range(0, May.Count); 
        }

        for (int i = 0; i < 3; i++) 
        {
            MayMission[i].text = May[MissionNumber[i]].ToString();
        }
    }
}
