namespace CodeBase.Demo_V2
{
    //Action (mob skill & result action from agent)
    public sealed class MobAction
    {
        public MobSkillType ActionType { get; }
        public MobSkillKind ActionKind { get; }
        public float Value { get; }

        public MobAction(MobSkillType actionType, MobSkillKind actionKind, float value)
        {
            ActionType = actionType;
            Value = value;
            ActionKind = actionKind;
        }
    }
}