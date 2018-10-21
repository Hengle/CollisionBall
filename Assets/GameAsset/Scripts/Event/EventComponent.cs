using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventComponent : MonoBehaviour {
    public static EventComponent Instance;
    public EventManager m_EventManager;

    private void OnEnable()
    {
        Instance = this;
    }

    public void Init()
    {
        m_EventManager = new EventManager();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
