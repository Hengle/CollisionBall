using System;
using GameFramework.Event;
using UnityGameFramework.Runtime;

namespace CollisionBall
{
    public class ScoreChangeEventArgs : GameEventArgs
    {
        public static readonly int EventId = 100002;

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public float Score
        {
            get;
            private set;
        }

        public override void Clear()
        {
            Score = 1000.0f;
        }

        public ScoreChangeEventArgs(float score)
        {
            Score = score;
        }
    }
}
