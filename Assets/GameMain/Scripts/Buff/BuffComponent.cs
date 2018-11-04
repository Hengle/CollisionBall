using GameFramework;
using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Entity;
using GameFramework.Event;
using System;

namespace CollisionBall
{
    public enum BuffType
    {
        NULL = 0,
        Shield,
        Accelerate,
        ImprovedGrab
    }

    public class BuffComponent : GameFrameworkComponent
    {
        public float SheilfBuffCoolTime = 15;
        public float GrabBuffCoolTime = 15;
        public bool IsOnSheilf = false;
        public bool IsOnGrab = false;
        public float deltaTimeForSheilfBuff = 1;
        public float deltaTimeForGrabBuff = 1;

        private void Start()
        {
        }
        public void Init()
        {
            GameEntry.Event.Subscribe(ReleaseBuffSkillEventArgs.EventId, OnReceiveReleaseBuffSkill);
        }

        private void OnReceiveReleaseBuffSkill(object sender, GameEventArgs e)
        {
            ReleaseBuffSkillEventArgs ne = (ReleaseBuffSkillEventArgs)e;
            if((BuffType)ne.BuffType == BuffType.Shield)
            {
                IsOnSheilf = true;
                SheilfBuffCoolTime = 15;
            }else if((BuffType)ne.BuffType == BuffType.ImprovedGrab)
            {
                IsOnGrab = true;
                GrabBuffCoolTime = 15;
            }
        }

        private void Update()
        {
            if (IsOnSheilf)
            {
                SheilfBuffCoolTime -= Time.deltaTime;
                deltaTimeForSheilfBuff -= Time.deltaTime;
                if (deltaTimeForSheilfBuff < 0)
                {
                    deltaTimeForSheilfBuff = 1;
                    GameEntry.Event.Fire(this,new UpdateBuffCoolTimeArgs((int)SheilfBuffCoolTime,BuffType.Shield));
                }
                if (SheilfBuffCoolTime <= 0)
                {
                    IsOnSheilf = false;
                }
            }

            if (IsOnGrab)
            {
                GrabBuffCoolTime -= Time.deltaTime;
                deltaTimeForGrabBuff -= Time.deltaTime;
                if (deltaTimeForGrabBuff < 0)
                {
                    deltaTimeForGrabBuff = 1;
                    GameEntry.Event.Fire(this, new UpdateBuffCoolTimeArgs((int)GrabBuffCoolTime, BuffType.ImprovedGrab));
                }
                if (GrabBuffCoolTime <= 0)
                {
                    IsOnGrab = false;
                }
            }
        }
    }
}
