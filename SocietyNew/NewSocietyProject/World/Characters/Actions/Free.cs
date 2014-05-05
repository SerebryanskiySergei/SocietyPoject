namespace World.Characters.Actions
{
    partial class Action
    {
        public static Decision Free()
        {
            var decision = new NoneAction {NewState = State.LastActionCompleted, NextAction = ActionType.Free};
            return decision;
        }
    }
}
