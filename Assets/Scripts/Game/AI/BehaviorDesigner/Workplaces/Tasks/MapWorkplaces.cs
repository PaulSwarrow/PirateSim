using System;
using BehaviorDesigner.Runtime.Tasks;
using Game.Actors;
using Game.Actors.Character.Interactions;
using Game.AI.BehaviorDesigner.Tasks.Abstract;
using Game.AI.BehaviorDesigner.Variables;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Game.AI.BehaviorDesigner.Workplaces.Tasks
{
    [TaskCategory(Categories.WorkPlaces)]
    public class MapWorkplaces : Action
    {
        public enum Filter
        {
            none,
            empty
        }

        public SharedCharacterActor forActor;
        [RequiredField]
        public SharedWorkPlaceMap map;
        public Filter filter;

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
            //TODO better solution?
            switch (filter)
            {
                case Filter.none: return true;
                case Filter.empty:
                {
                    return !(workPlace.Occupied && workPlace.Visitor != forActor.Value);
                }
                default: throw new Exception("Unknown filter");
            }
        }
    }
}