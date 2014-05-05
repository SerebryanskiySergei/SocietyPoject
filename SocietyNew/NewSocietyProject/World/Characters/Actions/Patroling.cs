using World.Enviroment;

namespace World.Characters.Actions
{
    partial class Action
    {
        public static MoveToPoint Patroling(Habitat settlement)
        {
            var decision = new MoveToPoint
            {
                Destination =
                    new System.Drawing.Point(RandomContainer.Random.Next(0, settlement.Width),
                        RandomContainer.Random.Next(0, settlement.Height)),
                NewState = State.Moving,
                NextAction = ActionType.Free
            };
            return decision;
        }
    }
}
