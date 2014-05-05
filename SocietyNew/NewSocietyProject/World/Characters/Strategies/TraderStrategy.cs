using World.Enviroment;

namespace World.Characters.Strategies
{
    class TraderStrategy : IStrategy
    {

        public Decision TakeDecision(IPersonToStrategy person, PersonalEnviroment personEnvir, Habitat settlement)
        {
            if (inDangerous(person))
            {
                return new TraderHealing();
            }
            if (person.GetEquipment().HowMoneyInCash() < 10)
                return new ChangeProfession
                {
                    NewState = State.LastActionCompleted,
                    NewProfession = Profession.Peasant
                };
            switch (person.GetStatus())
            {
                case State.Ready:
                    switch (person.GetLastDecision().NextAction)
                    {
                        case ActionType.Free:
                            return Actions.Action.Free();
                    }
                    return person.GetLastDecision();
                case State.LastActionCompleted:
                    return Actions.Action.Trading(settlement, personEnvir);
                case State.Fighting:
                    return Actions.Action.Attacking(person.GetOpponent());
            }
            return Actions.Action.Free();

        }
        /// <summary>
        /// Проверка на опсность для жизни.
        /// В случае опасности стратегия переключается на безопасную.
        /// </summary>
        /// <param name="person">Персонаж.</param>
        /// <returns>В опасности или нет.</returns>
        private bool inDangerous(IPersonToStrategy person)
        {
            return person.GetHp() < 25;
        }
    }


}

