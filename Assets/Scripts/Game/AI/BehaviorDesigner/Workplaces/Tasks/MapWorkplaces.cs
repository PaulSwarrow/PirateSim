using System;
using BehaviorDesigner.Runtime.Tasks;
using Game.Actors;
using Game.Actors.Character.Interactions;
using Game.Actors.Workplaces;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using Game.AI.BehaviorDesigner.Variables;
using Lib.UnityQuickTools.Enums;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Game.AI.BehaviorDesigner.Workplaces.Tasks
{
    [TaskCategory(Categories.WorkPlaces)]
    public class MapWorkplaces : Action
    {
        public SharedCharacterActor forActor;
        [RequiredField]
        public SharedWorkPlaceMap map;
        public WorkPlaceTag filter;
        public EnumComparison filterMode;

        public override TaskStatus OnUpdate()
        {
            map.Value.Clear();
            foreach (var entry in ActorTracker<WorkPlace>.All)
            {
                if (Check(entry))
                {
                    map.Value.Add(new SharedDictionary<WorkPlace, float>.Entry
                    {
                        key = entry,
                        value = 0 //TODO scan value?
                    });
                }
            }


            return TaskStatus.Success;
        }

        private bool Check(WorkPlace workPlace)
        {
            return workPlace.Check(filter, filterMode, forActor.Value);

        }
    }
}