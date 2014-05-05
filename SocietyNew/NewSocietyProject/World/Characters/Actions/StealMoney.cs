namespace World.Characters.Actions
{
    partial class Action
    {
        public static StealMoney StealMoney(IPersonToStrategy opponent)
        {
            var decision = new StealMoney
            {
                Opponent = (Person) opponent,
                NewState = State.Ready,
                NextAction = ActionType.Free
            };
            return decision;
        }
        
    }
}
