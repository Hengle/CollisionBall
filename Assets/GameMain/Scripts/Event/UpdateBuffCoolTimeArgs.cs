using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace CollisionBall
{
    public class UpdateBuffCoolTimeArgs : GameEventArgs
    {
        public static readonly int EventId = 1000011;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public int CurTime
        {
            get;
            set;
        }

        public BuffType BuffType
        {
            get;
            set;
        }

        public UpdateBuffCoolTimeArgs(int curTime,BuffType buffType)
        {
            this.CurTime = curTime;
            this.BuffType = buffType;
        }

        public override void Clear()
        {
            return;
        }
    }
}
