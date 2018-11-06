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
        [SerializeField]
        GameObject SkillRoot;
        [SerializeField]
        GameObject SkillNode;
        [SerializeField]
        GameObject BuffRoot;
        [SerializeField]
        GameObject BuffNode;
        [SerializeField]
        Sprite[] sprArray;
        int MaxBuffSkillCnt = 4;
        GameObject SheilfNode;
        GameObject GrabNode;

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
            GameEntry.Event.Subscribe(SetImageGrey.EventId, OnReceiveSetImageGreyEvent);
            GameEntry.Event.Subscribe(GenBuffSkillEventArgs.EventId, OnReceiveGenBuffEvent);
            GameEntry.Event.Subscribe(ReleaseBuffSkillEventArgs.EventId, OnReceiveReleaseBuffSkill);
            GameEntry.Event.Subscribe(UpdateBuffCoolTimeArgs.EventId, OnReceiveUpdateBuffCoolTime);
            //sprArray = Resources.LoadAll<Sprite>("Image/buff");

            m_stopBtn.onClick.AddListener(delegate { StopBtnOnClick(); });
            m_go_Btn.onClick.AddListener(delegate { GoBtnOnClick(); });
            Log.Info("注册点击事件");
        }

        private void OnReceiveUpdateBuffCoolTime(object sender, GameEventArgs e)
        {
            UpdateBuffCoolTimeArgs ne = (UpdateBuffCoolTimeArgs)e;
            if(ne.BuffType == BuffType.Shield)
            {
                SheilfNode.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ((int)ne.CurTime).ToString() + "s";
                if (ne.CurTime <= 0)
                {
                    Destroy(SheilfNode);
                    SheilfNode = null;
                }
            }
            if (ne.BuffType == BuffType.ImprovedGrab)
            {
                if (GrabNode)
                {
                    GrabNode.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = ((int)ne.CurTime).ToString() + "s";
                    if (ne.CurTime <= 0)
                    {
                        Destroy(GrabNode);
                        GrabNode = null;
                    }
                }
               
            }

            if(SheilfNode == null && GrabNode == null)
            {
                UnityGameFramework.Runtime.Entity player = GameEntry.Entity.GetEntity(GameEntry.EntityExtension.PlayerId);
                player.GetComponent<SpriteRenderer>().sprite = ((PlayerLogic)player.Logic).MainSprite;
            }
        }

        private void OnReceiveReleaseBuffSkill(object sender, GameEventArgs e)
        {
            ReleaseBuffSkillEventArgs ne = (ReleaseBuffSkillEventArgs)e;
            if (ne.BuffType == (int)BuffType.Shield)
            {
                if (SheilfNode == null)
                {
                    SheilfNode = Instantiate<GameObject>(BuffNode, BuffRoot.transform);
                    SheilfNode.transform.GetChild(0).GetComponent<Text>().text = "护盾";
                }
            }else if(ne.BuffType == (int)BuffType.ImprovedGrab)
            {
                Log.Info("释放强化技能");
                if (GrabNode == null)
                {
                    GrabNode = Instantiate<GameObject>(BuffNode, BuffRoot.transform);
                    GrabNode.transform.GetChild(0).GetComponent<Text>().text = "强化";
                }
            }
        }

        private void OnReceiveGenBuffEvent(object sender, GameEventArgs e)
        {
            if (SkillRoot.transform.childCount >= 4)
                Destroy(SkillRoot.transform.GetChild(0).gameObject);
            GenBuffSkillEventArgs ne = (GenBuffSkillEventArgs)e;
            BuffType type = (BuffType)ne.BuffType;
            GameObject obj = Instantiate<GameObject>(SkillNode, SkillRoot.transform);
            obj.GetComponent<UISkillNode>().Type = type;
            obj.GetComponent<Image>().sprite = sprArray[(int)type - 1];
        }

        private void OnReceiveSetImageGreyEvent(object sender, GameEventArgs e)
        {
            SetImageGrey ne = (SetImageGrey)e;
            int uId = ne.UIId;
            bool beGrey = ne.BeGrey;
            if(uId == 0)
            {
                GameEntry.UIExtension.SetImageGrey(m_stopBtn.transform.GetComponent<Image>(), beGrey);
            }else if(uId == 1)
            {
                GameEntry.UIExtension.SetImageGrey(m_go_Btn.transform.GetComponent<Image>(), beGrey);
            }
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
