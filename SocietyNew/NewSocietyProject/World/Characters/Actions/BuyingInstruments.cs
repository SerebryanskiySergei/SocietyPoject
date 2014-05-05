using World.Enviroment;

namespace World.Characters.Actions
{
    partial class Action
    {

        public static BuyingInstruments BuyingInstruments(Habitat settlement, PersonalEnviroment pEnvir)
        {
            var decision = new BuyingInstruments {NewState = State.Ready, NextAction = ActionType.MoveToCrafthouse};
            foreach (Person p in pEnvir.NearestCharacters)
            {
                if (p.GetProfession()==Profession.Trader)
                {
                    decision.Opponent = p;
                }
            }
            return decision;
        }
    }
}
