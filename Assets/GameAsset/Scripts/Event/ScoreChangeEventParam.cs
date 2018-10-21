using UnityEngine;
using System.Collections;

public class ScoreChangeEventParam : EventParam
{
    public float Score = 0;
    public ScoreChangeEventParam(float score)
    {
        Score = score;
    }
}
