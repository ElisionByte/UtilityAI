using System.Collections.Generic;
using System.Linq;
using CodeBase.Extensions;
using CodeBase.Gameplay.AI.Reporting;
using CodeBase.Gameplay.Battle;
using CodeBase.Gameplay.Heroes;
using CodeBase.Gameplay.HeroRegistry;
using CodeBase.Gameplay.Skills;
using CodeBase.Gameplay.Skills.Targeting;
using CodeBase.Infrastructure.StaticData;

namespace CodeBase.Gameplay.AI.Utility
{
  public class UtilityAI : IArtificialIntelligence
  {
    private readonly IStaticDataService _staticDataService;
    private readonly ITargetPicker _targetPicker;
    private readonly IHeroRegistry _heroRegistry;
    private readonly IAIReporter _aiReporter;
    private readonly ISkillSolver _skillSolver;
    private ICollection<IUtilityFunction> _utilityFunctions;

    public UtilityAI(
      IStaticDataService staticDataService,
      ITargetPicker targetPicker,
      IHeroRegistry heroRegistry,
      IAIReporter aiReporter,
      ISkillSolver skillSolver)
    {
      _staticDataService = staticDataService;
      _targetPicker = targetPicker;
      _heroRegistry = heroRegistry;
      _aiReporter = aiReporter;
      _skillSolver = skillSolver;


      _utilityFunctions = new MobsBrain().LoadBrain();
    }
    
    
    
    public HeroAction MakeBestDecision(IHero readyHero)
    {
      List<ScoredAction> choices = GetScoredHeroActions(readyHero, ReadySkills(readyHero))
        .ToList();

      _aiReporter.ReportDecisionScores(readyHero, choices);
      
      return choices.FindMax(x => x.Score);
    }

    private IEnumerable<ScoredAction> GetScoredHeroActions(IHero readyHero, IEnumerable<BattleSkill> skills)
    {
      foreach (BattleSkill skill in skills)
      foreach (HeroSet heroSet in HeroSetForSkill(skill))
      {
        float? score = CalculateScore(skill, heroSet);
        if (!score.HasValue)
          continue;

        yield return new ScoredAction(readyHero, heroSet.Targets, skill, score.Value);
      }
    }

    private float? CalculateScore(BattleSkill skill, HeroSet heroSet)
    {
      return heroSet.Targets
        .Select(hero => CalculateScore(skill, hero))
        .Where(x => x != null)
        .Sum();
    }

    private float? CalculateScore(BattleSkill skill, IHero hero)
    {
      List<ScoreFactor> scoreFactors = 
        (from utilityFunction in _utilityFunctions
          where utilityFunction.AppliesTo(skill, hero)
          let input = utilityFunction.GetInput(skill, hero, _skillSolver)
          let score = utilityFunction.Score(input, hero)
          select new ScoreFactor(utilityFunction.SkillType, score))
        .ToList();
      
      _aiReporter.ReportDecisionDetails(skill, hero, scoreFactors);

      return scoreFactors.Select(x => x.Score)
        .SumOrNull();
    }

    private IEnumerable<HeroSet> HeroSetForSkill(BattleSkill skill)
    {
      IEnumerable<string> availableTargets =
        _targetPicker.AvailableTargetsFor(skill.CasterId, skill.TargetType);

      if (skill.IsSingleTarget)
      {
        foreach (string target in availableTargets)
          yield return new HeroSet(_heroRegistry.GetHero(target));
        
        yield break;
      }

      yield return new HeroSet(availableTargets.Select(_heroRegistry.GetHero));
    }

    private IEnumerable<BattleSkill> ReadySkills(IHero readyHero)
    {
      return readyHero.State.SkillStates
        .Where(x => x.IsReady)
        .Select(x => new BattleSkill
        {
          CasterId = readyHero.Id,
          TypeId = x.TypeId,
          MaxCooldown = x.MaxCooldown,
          Kind = _staticDataService.HeroSkillFor(x.TypeId, readyHero.TypeId).Kind,
          TargetType = _staticDataService.HeroSkillFor(x.TypeId, readyHero.TypeId).TargetType,
        });
    }
  }
}