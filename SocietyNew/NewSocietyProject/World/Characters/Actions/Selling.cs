using World.Enviroment;

namespace World.Characters.Actions
{
    partial class Action
    {
        public static Selling Sell(Habitat settlement, PersonalEnviroment pEnvir)
        {
            var decision = new  Selling ();
            foreach (IPersonToStrategy p in pEnvir.NearestCharacters)
            {
                if (p.GetProfession() == Profession.Trader)
                {
                    decision.Opponent = (Person)p;
                }
            }
            decision.NewState = State.Ready;
            decision.NextAction = ActionType.Free;
            return decision;
        }
    }
}
