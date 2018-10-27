using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace CollisionBall
{
    public class ShowArrowsEventArgs : GameEventArgs
    {
        public static readonly int EventId = 100005;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public Vector3 Position
        {
            get;
            private set;
        }

        public Quaternion Rotation
        {
            get;
            private set;
        }

        public bool BeShow
        {
            get;
            protected set;
        }

        public override void Clear()
        {
            return;
        }

        public ShowArrowsEventArgs(Vector3 pos, Quaternion rot, bool beShow)
        {
            Position = pos;
            Rotation = rot;
            BeShow = beShow;
        }
    }
}
