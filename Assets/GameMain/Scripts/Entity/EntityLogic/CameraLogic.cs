using GameFramework;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace CollisionBall
{
    public class CameraLogic : EntityLogic
    {
        public static CameraLogic Instance;
        private Entity target;

        private void OnEnable()
        {
            Instance = this;
        }

        public Entity Target
        {
            get
            {
                return target;
            }
            set
            {
                target = value;
            }
        }

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            TraceTarget();
        }

        void TraceTarget()
        {
            if (target)
            {
                Vector3 pos = target.CachedTransform.position;
                pos.z = transform.position.z;
                transform.position = pos;
            }

        }
    } 
}
