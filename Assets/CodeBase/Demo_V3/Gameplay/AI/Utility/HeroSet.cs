using System.Collections.Generic;
using CodeBase.Gameplay.Heroes;

namespace CodeBase.Gameplay.AI.Utility
{
  public class HeroSet
  {
    public IEnumerable<IHero> Targets;

    public HeroSet(IHero hero)
    {
      Targets = new[] {hero};
    }

    public HeroSet(IEnumerable<IHero> heroes)
    {
      Targets = heroes;
    }
  }
}