using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLogic : MonoBehaviour {

    private Transform target;
    public static CameraLogic Instance;
    private bool hasInit = false;

    private void OnEnable()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        
	}

    public void Init()
    {
        target = GameObject.FindGameObjectWithTag("ControlPlayer").transform;
        hasInit = true;
    }

    // Update is called once per frame
    void Update () {
        TraceTarget();
	}

    void TraceTarget()
    {
        if (hasInit)
        {
            Vector3 pos = target.position;
            pos.z = transform.position.z;
            transform.position = pos;
        }
        
    }
}
