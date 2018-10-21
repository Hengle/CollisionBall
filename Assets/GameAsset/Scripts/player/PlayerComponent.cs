using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour {

    [SerializeField]
    private int enemy_num = 5;

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Transform player_root;

    public static PlayerComponent Instance;

    private List<Vector2> player_list;
    private List<Transform> player_unit;
    private MapManager _mapManager;

    private void OnEnable()
    {
        Instance = this;
    }

    public void Init()
    {
        _mapManager = GameEnter.Instance.m_MapManager;
        player_list = new List<Vector2>();
        player_unit = new List<Transform>();
        SpawnPlayer();
    }

    public List<Vector2> GetPlayerPosList()
    {
        return player_list;
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

    public void SpawnPlayer()
    {
        int row = _mapManager.Row;
        int column = _mapManager.Column;
        float half_width = _mapManager.GetMaxCorner().x;
        float half_height = _mapManager.GetMaxCorner().y;
        float cell_size = _mapManager.GetCellSize();
        float half_size = cell_size / 2;

        int cur_row = Random.Range(0, row);
        int cur_column = Random.Range(0, column);

        player_list.Add(new Vector2(cur_row, cur_column));
        float pos_x = cur_column * cell_size - half_width + half_size;
        float pos_y = cur_row * cell_size - half_height + half_size;

        GameObject unit = Instantiate(player, new Vector3(pos_x, pos_y, 0), Quaternion.identity, player_root);
        player_unit.Add(unit.transform);

        int enemy_count = 0;
        while (enemy_count < enemy_num)
        {
            cur_row = Random.Range(0, row);
            cur_column = Random.Range(0, column);
            Vector2 new_vec = new Vector2(cur_row, cur_column);
            if (!player_list.Contains(new_vec))
            {
                player_list.Add(new_vec);
                enemy_count++;
                pos_x = cur_column * cell_size - half_width + half_size;
                pos_y = cur_row * cell_size - half_height + half_size;
                unit = Instantiate(enemy, new Vector3(pos_x, pos_y, 0), Quaternion.identity, player_root);
                player_unit.Add(unit.transform);
            }
        }
    }

    public void CalculateCurUnitPos()
    {
        float cell_size = _mapManager.GetCellSize();
        for (int i = 0; i < player_unit.Count; i++)
        {
            Vector3 pos = player_unit[i].position;
            Vector2 cur_vec = CalculateRowAndColumn(new Vector2(pos.x, pos.y));
            player_list[i] = cur_vec;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
