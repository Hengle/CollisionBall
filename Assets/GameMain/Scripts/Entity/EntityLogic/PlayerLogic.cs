﻿using System;
using GameFramework;
using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;
using UnityEngine.EventSystems;

namespace CollisionBall
{
    public class PlayerLogic : Entity
    {
        [SerializeField]
        private PlayerData m_PlayerData = null;
        private float m_ForceScale = 0;

        private Vector3 _click_point;
        private Vector3 _move_dir;
        private Rigidbody2D _rigidbody;
        private float _force;
        private float score;
        private float _costScale = 5;

        public float Score
        {
            get
            {
                return score;
            }
            set
            {
                score = value;
            }
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnInit(object userData)
#else
        protected internal override void OnInit(object userData)
#endif
        {
            base.OnInit(userData);
            Log.Info("Player.OnInit()");
            _rigidbody = transform.GetComponent<Rigidbody2D>();
            GameEntry.Event.Subscribe(StopSkillEventArgs.EventId, OnReceiveStopSkill);
            GameEntry.Event.Subscribe(GoSkillEventArgs.EventId, OnReceiveGoSkill);
        }

        private void OnReceiveGoSkill(object sender, GameEventArgs e)
        {
            Score -= 20;
            GameEntry.Event.Fire(this, new ScoreChangeEventArgs(Score));
            Vector2 dir = _rigidbody.velocity.normalized;
            _rigidbody.velocity += dir * 5;
        }

        private void OnReceiveStopSkill(object sender, GameEventArgs e)
        {
            Score -= _rigidbody.velocity.magnitude * _costScale;
            GameEntry.Event.Fire(this, new ScoreChangeEventArgs(Score));
            _rigidbody.velocity = Vector2.zero;
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);
            Log.Info("Player.OnShow()");
            m_PlayerData = userData as PlayerData;
            m_ForceScale = m_PlayerData.ForceScale;
            Score = m_PlayerData.Score;
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnHide(object userData)
#else
        protected internal override void OnHide(object userData)
#endif
        {
            base.OnHide(userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
#else
        protected internal override void OnAttached(EntityLogic childEntity, Transform parentTransform, object userData)
#endif
        {
            base.OnAttached(childEntity, parentTransform, userData);
            Log.Info("Player.OnAttached()");
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnDetached(EntityLogic childEntity, object userData)
#else
        protected internal override void OnDetached(EntityLogic childEntity, object userData)
#endif
        {
            base.OnDetached(childEntity, userData);
            Log.Info("Player.OnDetached()");
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
#else
        protected internal override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
#endif
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
            Log.Info("Player.OnAttachTo()");
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnDetachFrom(EntityLogic parentEntity, object userData)
#else
        protected internal override void OnDetachFrom(EntityLogic parentEntity, object userData)
#endif
        {
            base.OnDetachFrom(parentEntity, userData);
            Log.Info("Player.OnDetachFrom()");
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            OnMouseKeepDown(0);
            OnMouseButtonUp(0);
        }

        void OnMouseButtonDown(int index)
        {
            //if(Input.GetMouseButtonDown(0))
        }

        void OnMouseKeepDown(int index)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (_rigidbody.velocity.magnitude > 0.15)
            {
                GameEntry.Event.Fire(this, new ShowArrowsEventArgs(Vector3.zero, Quaternion.identity, false));
                return;
            }
            if (Input.GetMouseButton(index))
            {
                _click_point = Input.mousePosition;
                Vector3 screen_point = Camera.main.WorldToScreenPoint(transform.position);
                _force = (screen_point - _click_point).magnitude * m_ForceScale;
                float costScore = _force / 100;
                GameEntry.Event.Fire(this, new ForceChangeEventArgs(_force));
                GameEntry.Event.Fire(this, new ScoreChangeEventArgs(Score - costScore));


                Quaternion rot = Quaternion.FromToRotation(Vector3.right, (screen_point - _click_point).normalized);
                rot.eulerAngles = new Vector3(0, 0, rot.eulerAngles.z);
                GameEntry.Event.Fire(this, new ShowArrowsEventArgs(screen_point+ (screen_point - _click_point), rot, true));
            }
        }

        void OnMouseButtonUp(int index)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (_rigidbody.velocity.magnitude > 0.15)
            {
                GameEntry.Event.Fire(this, new ShowArrowsEventArgs(Vector3.zero, Quaternion.identity, false));
                return;
            }
            if (Input.GetMouseButtonUp(index))
            {
                _click_point = Input.mousePosition;
                Vector3 screen_point = Camera.main.WorldToScreenPoint(transform.position);
                _move_dir = (screen_point - _click_point).normalized;
                _rigidbody.AddForce(_force * _move_dir);
                GameEntry.Event.Fire(this, new ForceChangeEventArgs(0.0f));
                Score -= _force / 100;
                GameEntry.Event.Fire(this, new ScoreChangeEventArgs(Score));
                GameEntry.Event.Fire(this, new ShowArrowsEventArgs(screen_point, Quaternion.identity, false));
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.tag == "Target")
            {
                UnityGameFramework.Runtime.Entity target = collision.transform.GetComponent<UnityGameFramework.Runtime.Entity>();
                GameEntry.Entity.HideEntity(target);
                SpawnComponent spawner = GameEntry.Spawn;
                spawner.RemoveTarget(new Vector2(collision.transform.position.x, collision.transform.position.y));
                Vector2[] new_born;
                spawner.GenRamdonPositions(1, out new_born);
                spawner.GenNewTaraget(new_born);
                Score += collision.transform.GetComponent<TargetLogic>().Score;
                GameEntry.Event.Fire(this, new ScoreChangeEventArgs(score));
                return;
            }
            if (collision.transform.tag == "Player")
            {
                float enemy = collision.transform.GetComponent<EnemyLogic>().Score;
                float tenpercent_enemy = 0.1f * enemy;
                float tenpercent_our = 0.1f * Score;
                collision.transform.GetComponent<EnemyLogic>().Score = enemy - tenpercent_enemy + tenpercent_our;
                Score = Score - tenpercent_our + tenpercent_enemy;
                GameEntry.Event.Fire(this, new ScoreChangeEventArgs(score));
            }
        }
    }
}
