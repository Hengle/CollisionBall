using System;
using UnityEngine;

namespace CollisionBall
{
    public class PlayerData : EntityData
    {
        [SerializeField]
        private float m_ForceScale = 5;

        public PlayerData(int entityId) : base(entityId)
        {

        }

        public float ForceScale
        {
            get
            {
                return m_ForceScale;
            }
            set
            {
                m_ForceScale = value;
            }
        }
    }
}
