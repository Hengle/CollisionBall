using GameFramework;
using GameFramework.ObjectPool;
using System.Collections.Generic;
using UnityEngine;
using UnityGameFramework.Runtime;
using GameFramework.Entity;
using UnityEngine.UI;

namespace CollisionBall
{
    public class EntityExtensionComponent : GameFrameworkComponent
    {
        private int playerId;
        public int PlayerId
        {
            set;
            get;
        }
    }
}
