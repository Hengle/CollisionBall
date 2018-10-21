using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField]
    private GameObject _target;
    [SerializeField]
    private GameObject _target_root;
    [SerializeField]
    private int _max_target;

    private List<Vector2> born_list;
    private MapManager _mapManager;
    private PlayerComponent _playercomponent;

    public static Spawner Instance;

    private void OnEnable()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {

	}

    public void Init()
    {
        _mapManager = GameEnter.Instance.m_MapManager;
        _playercomponent = GameEnter.Instance.m_PlayerComponent;
        born_list = new List<Vector2>();
        Vector2[] born_points;
        GenRamdonPositions(_max_target, out born_points);
        GenNewTaraget(born_points);
        Debug.Log("TargetSpawner初始化完成");
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void GenRamdonPositions(int num,out Vector2[] born_points)
    {
        List<Vector2> temp = new List<Vector2>();

        float half_width = _mapManager.GetMaxCorner().x;
        float half_height = _mapManager.GetMaxCorner().y;
        float _target_size = _mapManager.GetCellSize();
        float half_size = _target_size / 2;
        int row = _mapManager.Row;
        int column = _mapManager.Column;

        _playercomponent.CalculateCurUnitPos();
        List<Vector2> player_list = _playercomponent.GetPlayerPosList();

        while (temp.Count < num)
        {
            int row_value = Random.Range(0, row);
            int column_value = Random.Range(0, column);
            Vector2 r_c = new Vector2(row_value, column_value);
            float born_x = column_value * _target_size - half_width + Random.Range(0, 2*half_size);
            float born_y = row_value * _target_size - half_height + Random.Range(0, 2 * half_size);
            Vector2 new_born = new Vector2(born_x, born_y);
            if (_mapManager.GetMapNodeStatus(row_value,column_value) && (!player_list.Contains(r_c)))
            {
                born_list.Add(new_born);
                temp.Add(new_born);
                _mapManager.SetMapNodeStatus(row_value, column_value, false);
            }
        }
        born_points = temp.ToArray();
    }

    public void RemoveTarget(Vector2 position)
    {
        born_list.Remove(position);
        Vector2 r_c = CalculateRowAndColumn(position);
        _mapManager.SetMapNodeStatus((int)r_c.x, (int)r_c.y, true);
    }

    public Vector2 CalculateRowAndColumn(Vector2 position)
    {
        Vector2 min_corner = _mapManager.GetMinCorner();
        float cell_size = _mapManager.GetCellSize();
        int row = (int)((position.y - min_corner.y) / cell_size);
        int column = (int)((position.x - min_corner.x) / cell_size);
        Vector2 pos = new Vector2(row, column);
        return pos;
    }

    public void GenNewTaraget(Vector2[] born_point)
    {
        int num = born_point.Length;
        for(int i = 0; i < num; i++)
        {
            Instantiate(_target,new Vector3(born_point[i].x,born_point[i].y,0),Quaternion.identity,_target_root.transform);
        }
    }
}
