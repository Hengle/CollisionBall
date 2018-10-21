using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnter : MonoBehaviour {
    public static GameEnter Instance;
    public MapManager m_MapManager;
    public PlayerComponent m_PlayerComponent;
    public Spawner m_SpawnerManager;
    public EventComponent m_EventComponent;
    public UIMainForm m_UIMainForm;
    public UnitManager m_Unitmanger;
    private CameraLogic m_Camera;

    // Use this for initialization
    void Start () {
        Instance = this;
        InitAllComponent();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void InitAllComponent()
    {
        m_MapManager = MapManager.Instance;
        m_MapManager.Init();
        m_Unitmanger = UnitManager.Instance;
        m_Unitmanger.Init();
        m_PlayerComponent = PlayerComponent.Instance;
        m_PlayerComponent.Init();
        m_Camera = CameraLogic.Instance;
        m_Camera.Init();
        m_SpawnerManager = Spawner.Instance;
        m_SpawnerManager.Init();
        m_EventComponent = EventComponent.Instance;
        m_EventComponent.Init();
        m_UIMainForm = UIMainForm.Instance;
        m_UIMainForm.Init();
    }
}
