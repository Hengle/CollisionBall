using GameFramework;
using GameFramework.DataTable;
using UnityEngine;

namespace CollisionBall
{
    public class SurvivalGame : GameBase
    {
        private float m_ElapseSeconds = 0f;
        private SpawnComponent m_Spawn;
        private MapComponent m_Map;

        public override GameMode GameMode
        {
            get
            {
                return GameMode.Survival;
            }
        }

        public override void Update(float elapseSeconds, float realElapseSeconds)
        {
            base.Update(elapseSeconds, realElapseSeconds);
            
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Map = GameEntry.Map;
            m_Spawn = GameEntry.Spawn;
            //todo 初始化地图
            m_Map.Init();
            m_Spawn.Init();
            //加载玩家实体

        }
    }
}
