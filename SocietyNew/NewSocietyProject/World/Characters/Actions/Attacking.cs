namespace World.Characters.Actions
{
    partial class Action
    {
        public static Attack Attacking(IPersonToStrategy opponent)
        {
            var decision = new Attack
            {
                Opponent = (Person) opponent,
                NewState = State.Fighting,
                NextAction = ActionType.Free
            };
            return decision;
        }
    }
}
