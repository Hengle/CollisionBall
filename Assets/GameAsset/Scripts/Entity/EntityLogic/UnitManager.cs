using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {
    public static UnitManager Instance;
    private int unit_id = 10000;
    [SerializeField]
    private float targetRange = 4;

    private void OnEnable()
    {
        Instance = this;
    }

    public void Init()
    {

    }

    public int GenerateUnitID()
    {
        return unit_id++;
    }

    public GameObject[] GetUnitsNearByTarget(GameObject target)
    {
        Vector3 targetPos = target.transform.position;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        List<GameObject> list = new List<GameObject>();

        GameObject cplayer = GameObject.FindGameObjectWithTag("ControlPlayer");
        Vector3 temp = cplayer.transform.position;
        if (Vector3.Distance(temp, targetPos) <= targetRange)
            list.Add(cplayer);

        for (int i = 0; i < targets.Length; i++)
        {
            temp = targets[i].transform.position;
            if (Vector3.Distance(temp, targetPos) <= targetRange)
                list.Add(targets[i]);
        }

        for(int i = 0; i < players.Length; i++)
        {
            if (players[i] != target)
            {
                temp = players[i].transform.position;
                if (Vector3.Distance(temp, targetPos) <= targetRange)
                    list.Add(players[i]);
            }
        }

        return list.ToArray();
    }

    public Vector3 MoveDir(Vector3 Origin,GameObject[] target)
    {
        int length = target.Length;
        int index = Random.Range(0, length);
        Vector3 dir = target[index].transform.position - Origin;
        return dir;
    } 

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
