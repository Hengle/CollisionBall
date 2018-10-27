using GameFramework;
using GameFramework.Event;
using UnityGameFramework.Runtime;
using ProcedureOwner = GameFramework.Fsm.IFsm<GameFramework.Procedure.IProcedureManager>;

namespace CollisionBall
{
    public class ProcedureLaunch : ProcedureBase
    {

        protected override void OnEnter(ProcedureOwner procedureOwner)
        {
            Log.Debug("进入ProcedureLaunch流程");
            base.OnEnter(procedureOwner);
        }

        protected override void OnUpdate(ProcedureOwner procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            procedureOwner.SetData<VarInt>(Constant.ProcedureData.GameMode, (int)GameMode.Survival);
            ChangeState<ProcedureMain>(procedureOwner);
        }
    }
}
