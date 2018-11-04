using System;
using UnityEngine;

namespace CollisionBall
{
    public class TargetData : EntityData
    {
        public BuffType Type;
        public TargetData(int entityId) : base(entityId)
        {
        }
    }
}
