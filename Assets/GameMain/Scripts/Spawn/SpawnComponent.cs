using GameFramework;
using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Entity;

namespace CollisionBall
{
    public class SpawnComponent : GameFrameworkComponent
    {
        [SerializeField]
        private int _max_target = 15;
        [SerializeField]
        private int _max_enemy = 5;
        [SerializeField]
        private CameraLogic camera;

        private List<Vector2> born_list;
        private List<Vector2> cell_list;
        private float targetRange = 4;
        private MapComponent m_Map;

        // Use this for initialization
        void Start()
        {

        }

        public void Init()
        {
            GameEntry.Event.Subscribe(UnityGameFramework.Runtime.ShowEntitySuccessEventArgs.EventId, OnShowEntitySucess);
            //todo 创建player and enemy
            born_list = new List<Vector2>();
            cell_list = new List<Vector2>();
            Vector2[] born_points;
            m_Map = GameEntry.Map;
            ShowPlayerAndEnity();
            GenRamdonPositions(_max_target, out born_points);
            GenNewTaraget(born_points);
            Debug.Log("TargetSpawner初始化完成");
        }

        public void ShowPlayerAndEnity()
        {
            
            int row = m_Map.Row;
            int column = m_Map.Column;
            float half_width = m_Map.GetMaxCorner().x;
            float half_height = m_Map.GetMaxCorner().y;
            float cell_size = m_Map.GetCellSize();
            float half_size = cell_size / 2;
            int cur_row = Random.Range(0, row);
            int cur_column = Random.Range(0, column);
            cell_list.Add(new Vector2(cur_row, cur_column));

            float pos_x = cur_column * cell_size - half_width + half_size;
            float pos_y = cur_row * cell_size - half_height + half_size;
            int playerId = GameEntry.Entity.GenerateSerialId();

            //todo 创建玩家
            GameEntry.Entity.ShowPlayer("Player", new PlayerData(playerId)
            {
                Position = new Vector3(pos_x, pos_y, 0),
                Rotation =Quaternion.identity,
                Score =1000
            });

            //todo 穿件敌人
            int numOfEnemy = 0;
            while (numOfEnemy < _max_enemy)
            {
                cur_row = Random.Range(0, row);
                cur_column = Random.Range(0, column);
                Vector2 new_vec = new Vector2(cur_row, cur_column);
                if (!cell_list.Contains(new_vec))
                {
                    cell_list.Add(new_vec);
                    numOfEnemy++;
                    pos_x = cur_column * cell_size - half_width + half_size;
                    pos_y = cur_row * cell_size - half_height + half_size;
                    int enemyId = GameEntry.Entity.GenerateSerialId();
                    GameEntry.Entity.ShowEnemy("Enemy", new EnemyData(enemyId)
                    {
                        Position = new Vector3(pos_x, pos_y, 0),
                        Rotation = Quaternion.identity,
                        Score = 1000
                    });
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void GeneratePlayer()
        {

        }

        public List<Vector2> CalculateCurUnitPos()
        {
            List<Vector2> entityList = new List<Vector2>();
            IEntityGroup playerGroup = GameEntry.Entity.GetEntityGroup("Player");
            IEntityGroup enemyGroup = GameEntry.Entity.GetEntityGroup("Enemy");
            IEntity[] players = playerGroup.GetAllEntities();
            IEntity[] enemys = enemyGroup.GetAllEntities();
            foreach(UnityGameFramework.Runtime.Entity entity in players)
            {
                Entity entityLogic = (Entity)entity.Logic;
                Vector3 pos = entityLogic.CachedTransform.position;
                Vector2 cur_cellpos = CalculateRowAndColumn(new Vector2(pos.x, pos.y));
                entityList.Add(cur_cellpos);
            }
            foreach(UnityGameFramework.Runtime.Entity entity in enemys)
            {
                Entity entityLogic = (Entity)entity.Logic;
                Vector3 pos = entityLogic.CachedTransform.position;
                Vector2 cur_cellpos = CalculateRowAndColumn(new Vector2(pos.x, pos.y));
                entityList.Add(cur_cellpos);
            }
            return entityList;
        }

        public void GenRamdonPositions(int num, out Vector2[] born_points)
        {
            List<Vector2> temp = new List<Vector2>();

            float half_width = m_Map.GetMaxCorner().x;
            float half_height = m_Map.GetMaxCorner().y;
            float _target_size = m_Map.GetCellSize();
            float half_size = _target_size / 2;
            int row = m_Map.Row;
            int column = m_Map.Column;

            List<Vector2> player_list = CalculateCurUnitPos();

            while (temp.Count < num)
            {
                int row_value = Random.Range(0, row);
                int column_value = Random.Range(0, column);
                Vector2 r_c = new Vector2(row_value, column_value);
                float born_x = column_value * _target_size - half_width + Random.Range(0, 2 * half_size);
                float born_y = row_value * _target_size - half_height + Random.Range(0, 2 * half_size);
                Vector2 new_born = new Vector2(born_x, born_y);
                if (m_Map.GetMapNodeStatus(row_value, column_value) && (!player_list.Contains(r_c)))
                {
                    born_list.Add(new_born);
                    temp.Add(new_born);
                    m_Map.SetMapNodeStatus(row_value, column_value, false);
                }
            }
            born_points = temp.ToArray();
        }

        public void RemoveTarget(Vector2 position)
        {
            born_list.Remove(position);
            Vector2 r_c = CalculateRowAndColumn(position);
            m_Map.SetMapNodeStatus((int)r_c.x, (int)r_c.y, true);
        }

        public Vector2 CalculateRowAndColumn(Vector2 position)
        {
            Vector2 min_corner = GameEntry.Map.GetMinCorner();
            float cell_size = GameEntry.Map.GetCellSize();
            int row = (int)((position.y - min_corner.y) / cell_size);
            int column = (int)((position.x - min_corner.x) / cell_size);
            Vector2 pos = new Vector2(row, column);
            return pos;
        }

        public void GenNewTaraget(Vector2[] born_point)
        {
            int num = born_point.Length;
            for (int i = 0; i < num; i++)
            {
                int targetId = GameEntry.Entity.GenerateSerialId();
                GameEntry.Entity.ShowTarget("Target", new TargetData(targetId) {
                    Position = new Vector3(born_point[i].x, born_point[i].y, 0),
                    Rotation = Quaternion.identity,
                    Score = 50
                });
                //Instantiate(_target, new Vector3(born_point[i].x, born_point[i].y, 0), Quaternion.identity, _target_root.transform);
            }
        }

        public void OnShowEntitySucess(object sender, GameFramework.Event.GameEventArgs e)
        {
            UnityGameFramework.Runtime.ShowEntitySuccessEventArgs ne = (UnityGameFramework.Runtime.ShowEntitySuccessEventArgs)e;
            if (ne.Entity.transform.gameObject.tag == "ControlPlayer")
            {
                camera.Target = (Entity)ne.Entity.Logic;
            }
        }

        public UnityGameFramework.Runtime.Entity[] GetUnitsNearByTarget(UnityGameFramework.Runtime.Entity target)
        {
            UnityGameFramework.Runtime.Entity[] allEntity = GameEntry.Entity.GetAllLoadedEntities();
            List<UnityGameFramework.Runtime.Entity> entityList = new List<UnityGameFramework.Runtime.Entity>();
            foreach(UnityGameFramework.Runtime.Entity entity in allEntity)
            {
                if(entity.Id != target.Id)
                {
                    if (Vector3.Distance(entity.transform.position, target.transform.position) <= targetRange)
                    {
                        entityList.Add(entity);
                    }
                }
            }
            return entityList.ToArray();
        }

        public Vector3 MoveDir(Vector3 Origin, UnityGameFramework.Runtime.Entity[] target)
        {
            int length = target.Length;
            int index = Random.Range(0, length);
            Vector3 dir = target[index].transform.position - Origin;
            return dir;
        }
    } 
}
