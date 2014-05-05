using World.Enviroment;

namespace World.Characters.Actions
{
    partial class Action
    {
        /// <summary>
        /// Стоять и торговать.
        /// </summary>
        /// <param name="settlement"></param>
        /// <param name="pEnvir"></param>
        /// <returns></returns>
        public static Trade Trading(Habitat settlement, PersonalEnviroment pEnvir)
        {
            var decision = new Trade {NewState = State.Ready, NextAction = ActionType.Free};
            return decision;
        }
    }
}
