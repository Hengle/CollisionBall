using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;
using UnityEngine.UI;
using GameFramework.Event;
using System;

namespace CollisionBall
{
    public class UISkillNode : MonoBehaviour
    {
        public BuffType Type;
        public Button btn;

        private void Start()
        {
            btn.onClick.AddListener(delegate { DestroySelf(); });
        }

        void DestroySelf()
        {
            Destroy(this.gameObject);
            GameEntry.Event.Fire(this, new ReleaseBuffSkillEventArgs((int)Type));
        }
    }
}
