using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainForm : MonoBehaviour {

    private ScoreChangeEvent m_event;
    public Text m_score;

    private ForceChangeEvent m_forceEvent;
    public Text m_force;

    public static UIMainForm Instance;

    private void OnEnable()
    {
        Instance = this;
    }

    public void Init()
    {
        m_event = (ScoreChangeEvent)GameEnter.Instance.m_EventComponent.m_EventManager.Event[EventID.ScoreChangeEvent];
        m_event.AddListener(OnReceiveScoreChange);
        m_forceEvent = (ForceChangeEvent)GameEnter.Instance.m_EventComponent.m_EventManager.Event[EventID.ForceChangeEvent];
        m_forceEvent.AddListener(OnReceiveForceChange);
    }

    public void OnReceiveScoreChange(EventParam param)
    {
        ScoreChangeEventParam score = (ScoreChangeEventParam)param;
        m_score.text = System.Math.Round((float)score.Score,2).ToString();
    }

    public void OnReceiveForceChange(EventParam param)
    {
        ForceChangeEventParam force = (ForceChangeEventParam)param;
        m_force.text = System.Math.Round((float)force.Force, 2).ToString();
    }

	// Use this for initialization
	void Start () {
        //Init();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
