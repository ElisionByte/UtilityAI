namespace CodeBase.Gameplay.AI.Utility
{
    public abstract class BaseBrain
    {
        public abstract Convolutions LoadBrain();
    }

    public abstract class BaseMobBrain
    {
        public abstract MobConvolutions LoadBrain();
    }
}