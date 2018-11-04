using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace CollisionBall
{
    public class GenBuffSkillEventArgs : GameEventArgs
    {
        public static readonly int EventId = 100009;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public int BuffType
        {
            get;
            set;
        }


        public GenBuffSkillEventArgs(int type)
        {
            BuffType = type;
        }

        public override void Clear()
        {
            return;
        }
    }
}
