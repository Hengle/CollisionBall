using System;
using UnityEngine;

namespace CollisionBall
{
    public class EnemyData : EntityData
    {
        [SerializeField]
        private float m_MaxForce = 1500.0f;
        [SerializeField]
        private float m_MixForce = 300.0f;
        public EnemyData(int entityId) : base(entityId)
        {
        }

        public float MaxForce
        {
            get
            {
                return m_MaxForce;
            }
            set
            {
                m_MaxForce = value;
            }
        }

        public float MixForce
        {
            get
            {
                return m_MixForce;
            }
            set
            {
                m_MixForce = value;
            }
        }
    }
}
