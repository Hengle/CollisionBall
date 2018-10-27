using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityEngine.UI;
using GameFramework.Event;
using System;

namespace CollisionBall
{
    public class MainForm : UGuiForm
    {
        [SerializeField]
        private Text m_Force;
        [SerializeField]
        private Text m_Score;
        [SerializeField]
        Button m_stopBtn;
        [SerializeField]
        Button m_go_Btn;
        [SerializeField]
        Transform arrows;

#if UNITY_2017_3_OR_NEWER
        protected override void OnOpen(object userData)
#else
        protected internal override void OnOpen(object userData)
#endif
        {
            base.OnOpen(userData);
            GameEntry.Event.Subscribe(ForceChangeEventArgs.EventId, OnReceiveForceChange);
            GameEntry.Event.Subscribe(ScoreChangeEventArgs.EventId, OnReceiveScoreChange);
            GameEntry.Event.Subscribe(ShowArrowsEventArgs.EventId, OnReceiveShowArrowEvent);

            m_stopBtn.onClick.AddListener(delegate { StopBtnOnClick(); });
            m_go_Btn.onClick.AddListener(delegate { GoBtnOnClick(); });
            Log.Info("注册点击事件");
        }

        private void OnReceiveShowArrowEvent(object sender, GameEventArgs e)
        {
            ShowArrowsEventArgs ne = (ShowArrowsEventArgs)e;
            Vector3 pos = ne.Position;
            Quaternion rot = ne.Rotation;
            bool beShow = ne.BeShow;

            arrows.position = pos;
            arrows.rotation = rot;
            arrows.gameObject.SetActive(beShow);
               
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
        void StopBtnOnClick()
        {
            Log.Info("发送StopSkillEventArgs");
            GameEntry.Event.Fire(this, new StopSkillEventArgs());
        }
        void GoBtnOnClick()
        {
            Log.Info("发送GoSkillEventArgs");
            GameEntry.Event.Fire(this, new GoSkillEventArgs());
        }
    } 
}
