using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {
    [SerializeField]
    private Vector2 _min_corner;
    [SerializeField]
    private Vector2 _max_corner;
    [SerializeField]
    private float _target_size = 3;

    public static MapManager Instance;
    private bool[,] map;

    public Vector2 GetMaxCorner()
    {
        return _max_corner;
    }

    public Vector2 GetMinCorner()
    {
        return _min_corner;
    }

    public float GetCellSize()
    {
        return _target_size;
    }

    public int Row
    {
        get;
        set;
    }

    public int Column
    {
        get;
        set;
    }

    public void Init()
    {
        int row = (int)((_max_corner.y - _min_corner.y) / _target_size);
        int column = (int)((_max_corner.x - _min_corner.x) / _target_size);
        Row = row;
        Column = column;
        map = new bool[Row, Column];
        for(int i=0; i < Row; i++)
        {
            for(int j = 0; j < Column; j++)
            {
                map[i, j] = true;
            }
        }
        Debug.Log("MapManger初始化完成"+"row:"+row+"column:"+column);
    }

    public bool GetMapNodeStatus(int row,int column)
    {
        return map[row, column];
    }

    public void SetMapNodeStatus(int row,int column,bool status)
    {
        map[row, column] = status;
    }

    private void OnEnable()
    {
        Instance = this;
    }
}
