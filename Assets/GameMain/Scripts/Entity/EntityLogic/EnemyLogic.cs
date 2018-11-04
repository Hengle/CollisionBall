using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace CollisionBall
{
    public class EnemyLogic : Entity
    {
        //public
        public float[] _forceRange = new float[] { 500, 3000 };

        //private
        private float score = 1000;
        private Rigidbody2D _rigidbody;
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
            _rigidbody = transform.GetComponent<Rigidbody2D>();
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnShow(object userData)
#else
        protected internal override void OnShow(object userData)
#endif
        {
            base.OnShow(userData);
            EnemyData data = (EnemyData)userData;
            Score = data.Score;
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
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnDetached(EntityLogic childEntity, object userData)
#else
        protected internal override void OnDetached(EntityLogic childEntity, object userData)
#endif
        {
            base.OnDetached(childEntity, userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
#else
        protected internal override void OnAttachTo(EntityLogic parentEntity, Transform parentTransform, object userData)
#endif
        {
            base.OnAttachTo(parentEntity, parentTransform, userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnDetachFrom(EntityLogic parentEntity, object userData)
#else
        protected internal override void OnDetachFrom(EntityLogic parentEntity, object userData)
#endif
        {
            base.OnDetachFrom(parentEntity, userData);
        }

#if UNITY_2017_3_OR_NEWER
        protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#else
        protected internal override void OnUpdate(float elapseSeconds, float realElapseSeconds)
#endif
        {
            base.OnUpdate(elapseSeconds, realElapseSeconds);
            if (_rigidbody.velocity.magnitude > 0.15)
                return;
            StartMove();
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
                score += collision.transform.GetComponent<TargetLogic>().Score;
                return;
            }
            if (collision.transform.tag == "Player")
            {
                EnemyLogic logic = collision.transform.GetComponent<EnemyLogic>();
                if (logic.HasSheild && this.HasSheild)
                    return;
                if (logic.HasSheild)
                {
                    return;
                }
                else if(this.HasSheild)
                {
                    float enemy = logic.score;
                    float tenpercent_enemy = 0.1f * this.GrabFactor * enemy;
                    score = score + tenpercent_enemy;
                }
                else
                {
                    float enemy = logic.score;
                    float tenpercent_enemy = 0.1f * this.GrabFactor * enemy;
                    float tenpercent_our = 0.1f * logic.GrabFactor * score;
                    score = score - tenpercent_our + tenpercent_enemy;
                }
            }
        }

        private void StartMove()
        {
            SpawnComponent spawner = GameEntry.Spawn;
            UnityGameFramework.Runtime.Entity[] groups = spawner.GetUnitsNearByTarget(this.Entity);
            if (groups.Length == 0)
            {
                Vector2 tempdir = Random.insideUnitCircle.normalized;
                Vector3 dir = new Vector3(tempdir.x, tempdir.y, 0);
                float randomForce = Random.Range(_forceRange[0], _forceRange[1]);
                _rigidbody.AddForce(randomForce * dir);
            }
            else
            {
                float probability = Random.Range(0, 1);
                if (probability < 0.5)
                {
                    Vector2 tempdir = Random.insideUnitCircle.normalized;
                    Vector3 dir = new Vector3(tempdir.x, tempdir.y, 0);
                    float randomForce = Random.Range(_forceRange[0], _forceRange[1]);
                    _rigidbody.AddForce(randomForce * dir);
                }
                else
                {
                    Vector3 dir = spawner.MoveDir(transform.position, groups);
                    float randomForce = Random.Range(_forceRange[0], _forceRange[1]);
                    _rigidbody.AddForce(randomForce * dir);
                }
            }
        }
    }
}
