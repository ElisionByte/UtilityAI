namespace CodeBase.Demo_V2
{
    //Utility Config Function Contract
    public interface IMobUtilityFunction
    {
        bool When(MobAction action, IMob mob);
        float Input(MobAction action, IMob mob);
        float Score(float input, IMob mob);
        MobSkillKind SkillKind { get; }
    }
}