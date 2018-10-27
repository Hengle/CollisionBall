using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

namespace CollisionBall
{
    public class MainForm : UGuiForm
    {
        [SerializeField]
        private Text m_Force;
        [SerializeField]
        private Text m_Score;

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);
            GameEntry.Event.Subscribe(ForceChangeEventArgs.EventId, OnReceiveForceChange);
            GameEntry.Event.Subscribe(ScoreChangeEventArgs.EventId, OnReceiveScoreChange);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnClose(object userData)
#else
        protected internal override void OnClose(object userData)
#endif
        {
            base.OnClose(userData);
            GameEntry.Event.Unsubscribe(ForceChangeEventArgs.EventId, OnReceiveForceChange);
            GameEntry.Event.Unsubscribe(ScoreChangeEventArgs.EventId, OnReceiveScoreChange);
        }

        public void OnReceiveScoreChange(object sender, GameFramework.Event.GameEventArgs e)
        {
            ScoreChangeEventArgs ne = (ScoreChangeEventArgs)e;
            m_Score.text = System.Math.Round(ne.Score, 2).ToString();
        }

        public void OnReceiveForceChange(object sender,GameFramework.Event.GameEventArgs e)
        {
            ForceChangeEventArgs ne = (ForceChangeEventArgs)e;
            m_Force.text = System.Math.Round(ne.Force, 2).ToString();
        }
    } 
}
