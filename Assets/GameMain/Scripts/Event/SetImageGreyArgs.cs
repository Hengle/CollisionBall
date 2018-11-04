using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace CollisionBall
{
    public class SetImageGrey : GameEventArgs
    {
        public static readonly int EventId = 100008;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public int UIId
        {
            get;
            set;
        }

        public bool BeGrey
        {
            get;
            set;
        }

        public SetImageGrey(int uid,bool beGrey)
        {
            UIId = uid;
            BeGrey = beGrey;
        }

        public override void Clear()
        {
            return;
        }
    }
}
