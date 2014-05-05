using World.Enviroment;

namespace World.Characters.Actions
{
    partial class Action
    {
        /// <summary>
        /// Идет на работу.
        /// </summary>
        /// <param name="settlement">Окружающий мир.</param>
        /// <param name="pEnvir">Окружение персонажа.</param>
        /// <returns></returns>
        public static MoveToPoint GoingToFarm(Habitat settlement,PersonalEnviroment pEnvir)
        {
            var move = new MoveToPoint
            {
                NewState = State.Moving,
                NextAction = ActionType.Work,
                Destination =
                {
                    X = RandomContainer.Random.Next(settlement.Farm.X, settlement.Farm.X + settlement.Farm.Width),
                    Y = RandomContainer.Random.Next(settlement.Farm.Y, settlement.Farm.Y + settlement.Farm.Height)
                }
            };
            return move;
        }

        /// <summary>
        /// Работает.
        /// </summary>
        /// <param name="settlement">Окружающий мир.</param>
        /// <param name="pEnvir">Окружение персонажа.</param>
        /// <returns></returns>
        public static Working Work(Habitat settlement,PersonalEnviroment pEnvir)
        {
            var decision = new Working {NewState = State.Working, NextAction = ActionType.MoveToTrader};
            return decision;
        }

        /// <summary>
        /// Идет продать собранное зерно.
        /// </summary>
        /// <param name="settlement">Мир</param>
        /// <param name="pEnvir">Окружение персонажа.</param>
        /// <returns></returns>
        public static MoveToPoint GoingToTrader(Habitat settlement,PersonalEnviroment pEnvir)
        {
            var move = new MoveToPoint
            {
                Destination = settlement.FindFullTraderPlace(),
                NextAction = ActionType.Sell,
                NewState = State.Moving
            };
            return move;
        }
        /// <summary>
        /// Идти за иструментами.
        /// </summary>
        /// <param name="settlement"></param>
        /// <param name="pEnvir"></param>
        /// <returns></returns>
        public static MoveToPoint GoingForInstruments(Habitat settlement, PersonalEnviroment pEnvir)
        {
            var move = new MoveToPoint
            {
                Destination = settlement.FindFullTraderPlace(),
                NextAction = ActionType.BuyInstruments,
                NewState = State.Moving
            };
            return move;
        }
        /// <summary>
        /// Идет на работу.
        /// </summary>
        /// <param name="settlement">Окружающий мир.</param>
        /// <returns></returns>
        public static MoveToPoint GoingToCrafthouse(Habitat settlement, PersonalEnviroment pEnvir)
        {
            var move = new MoveToPoint
            {
                NewState = State.Moving,
                NextAction = ActionType.Craft,
                Destination =
                {
                    X =
                        RandomContainer.Random.Next(settlement.CrHouse.X,
                            settlement.CrHouse.X + settlement.CrHouse.Width),
                    Y =
                        RandomContainer.Random.Next(settlement.CrHouse.Y,
                            settlement.CrHouse.Y + settlement.CrHouse.Height)
                }
            };
            return move;
        }
        /// <summary>
        /// Работает. 
        /// </summary>
        /// <param name="settlement"></param>
        /// <param name="pEnvir"></param>
        /// <returns></returns>
        public static Working Crafting(Habitat settlement, PersonalEnviroment pEnvir)
        {
            var decision = new Working {NewState = State.Working, NextAction = ActionType.MoveToTrader};
            return decision;
        }
    }
}
