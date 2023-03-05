using System.Collections.Generic;

namespace CodeBase.Demo_V2
{
    //Mob contract & Precept for agent
    public interface IMob
    {
        float MaxHp { get; }
        float Hp { get; set; }

        IEnumerable<MobAction> Actions { get; }
        MobType Type { get; }

        void Initialise();
        void ReceiveAction(MobAction action);
    }
}