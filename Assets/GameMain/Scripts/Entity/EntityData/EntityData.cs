using System;
using UnityEngine;

namespace CollisionBall
{
    [Serializable]
    public abstract class EntityData
    {
        [SerializeField]
        private int m_Id = 0;

        [SerializeField]
        private Vector3 m_Position = Vector3.zero;

        [SerializeField]
        private Quaternion m_Rotation = Quaternion.identity;

        [SerializeField]
        private float m_Score = 0;

        public EntityData(int entityId)
        {
            m_Id = entityId;
        }

        /// <summary>
        /// 实体编号。
        /// </summary>
        public int Id
        {
            get
            {
                return m_Id;
            }
        }

        /// <summary>
        /// 实体位置。
        /// </summary>
        public Vector3 Position
        {
            get
            {
                return m_Position;
            }
            set
            {
                m_Position = value;
            }
        }

        /// <summary>
        /// 实体朝向。
        /// </summary>
        public Quaternion Rotation
        {
            get
            {
                return m_Rotation;
            }
            set
            {
                m_Rotation = value;
            }
        }

        /// <summary>
        /// 实体积分
        /// </summary>
        public float Score
        {
            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }
    }
}
