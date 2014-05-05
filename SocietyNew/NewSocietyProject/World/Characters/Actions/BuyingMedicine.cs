namespace World.Characters.Actions
{
    partial class Action
    {

        public static BuyingMedicine BuyMedicine(IPersonToStrategy opponent)
        {
            var decsion = new BuyingMedicine
            {
                NewState = State.LastActionCompleted,
                Opponent = (Person) opponent,
                NextAction = ActionType.Free
            };
            return decsion;
        }
    }
}
