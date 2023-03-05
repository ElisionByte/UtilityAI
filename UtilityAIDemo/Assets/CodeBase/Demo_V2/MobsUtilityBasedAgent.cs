using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Demo_V2
{
    //Utility-Based Agent
    public sealed class MobsUtilityBasedAgent
    {
        private ICollection<IMobUtilityFunction> _utilityFunctions = default;

        public void Initialise(BaseMobBrain brain)
        {
            _utilityFunctions = brain.LoadBrain();
        }

        public MobAction ProceedBestDecision(IMob caster, IMob target)
        {
            Dictionary<MobAction, float> actionAndScore = new Dictionary<MobAction, float>();

            //Iterate through all actions mob have
            foreach (MobAction action in caster.Actions)
            {
                //Collect sum of actions for example single damage can be happy for 50 points
                //but area damage can be happy for 150 points
                float actionsScoreSum =
                (
                    from utilityFunction in _utilityFunctions
                        //Check with When Sensor here just validate action type
                    where utilityFunction.When(action, target)
                    //Check with Input Sensor can be sum of hp opponents or just your dmg
                    let input = utilityFunction.Input(action, target)
                    //Check with Score Sensor (just score calculation)
                    select utilityFunction.Score(input, target)
                )
                .Sum();

                //Handle it Action-Score
                actionAndScore.Add(action, actionsScoreSum);

                Debug.Log($"{caster.Type}->{target.Type} : {action.ActionType} : {action.ActionKind} : {actionsScoreSum}");
            }

            //Finding best scored action with Aggregate
            MobAction bestAction = actionAndScore.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;

            Debug.Log($"Best for {caster.Type}->{target.Type} : {bestAction.ActionType} : {bestAction.ActionKind} | {bestAction.Value}");

            return bestAction;
        }
    }
}