using GameFramework;
using GameFramework.DataTable;
using System;
using UnityGameFramework.Runtime;

namespace CollisionBall
{
    public static class EntityExtension
    {
        // 关于 EntityId 的约定：
        // 0 为无效
        // 正值用于和服务器通信的实体（如玩家角色、NPC、怪等，服务器只产生正值）
        // 负值用于本地生成的临时实体（如特效、FakeObject等）
        private static int s_SerialId = 0;

        public static Entity GetGameEntity(this EntityComponent entityComponent, int entityId)
        {
            UnityGameFramework.Runtime.Entity entity = entityComponent.GetEntity(entityId);
            if (entity == null)
            {
                return null;
            }

            return (Entity)entity.Logic;
        }

        public static void HideEntity(this EntityComponent entityComponent, Entity entity)
        {
            entityComponent.HideEntity(entity);
        }

        public static void AttachEntity(this EntityComponent entityComponent, Entity entity, int ownerId, string parentTransformPath = null, object userData = null)
        {
            entityComponent.AttachEntity(entity, ownerId, parentTransformPath, userData);
        }

        public static void ShowPlayer(this EntityComponent entityComponent,string name, PlayerData data)
        {
            entityComponent.ShowEntity(typeof(PlayerLogic), name, "Player", 100, data);
        }

        public static void ShowEnemy(this EntityComponent entityComponent, string name, EntityData data)
        {
            entityComponent.ShowEntity(typeof(EnemyLogic), name, "Enemy", 110, data);
        }

        public static void ShowTarget(this EntityComponent entityComponent, string name, EntityData data)
        {
            entityComponent.ShowEntity(typeof(TargetLogic), name, "Target", 120, data);
        }

        private static void ShowEntity(this EntityComponent entityComponent, Type logicType,string name, string entityGroup, int priority, EntityData data)
        {
            if (data == null)
            {
                Log.Warning("Data is invalid.");
                return;
            }

            entityComponent.ShowEntity(data.Id, logicType, AssetUtility.GetEntityAsset(name), entityGroup, priority, data);
        }

        public static int GenerateSerialId(this EntityComponent entityComponent)
        {
            return --s_SerialId;
        }
    }
}
