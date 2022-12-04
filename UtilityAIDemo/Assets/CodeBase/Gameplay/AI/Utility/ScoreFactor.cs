namespace CodeBase.Gameplay.AI.Utility
{
    public struct ScoreFactor
    {
        public SkillType SkillType { get; }
        public float Score { get; }

        public ScoreFactor(SkillType skillType, float score)
        {
            SkillType = skillType;
            Score = score;
        }

        public override string ToString() => $"{SkillType} -> {(Score >= 0 ? "+" : "")}{Score}";
    }
}