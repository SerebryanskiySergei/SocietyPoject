using World.Enviroment;

namespace World.Characters.Strategies
{
    class CraftsmanStrategy : IStrategy
    {
        public Decision TakeDecision(IPersonToStrategy person, PersonalEnviroment personEnvir, Habitat settlement)
        {
            if (person.GetStatus() == State.Died)
                return null;
            if (person.GetEquipment().HowMoneyInCash() < 10)
                return new ChangeProfession
                {
                    NewState = State.LastActionCompleted,
                    NewProfession = Profession.Warrior
                };
            if (InDangerous(person) && person.GetStatus()!=State.Fighting)
            {
                person.ChangeStrategy(new SafeStrategy());
                return person.GetStrategy().TakeDecision(person, personEnvir, settlement);
            }
            switch (person.GetStatus())
            {
                case State.LastActionCompleted:
                    if (CheckEnviroment(person, personEnvir) == null)
                        return Actions.Action.GoingForInstruments(settlement, personEnvir);
                    else
                        return CheckEnviroment(person, personEnvir);
                case State.Moving:
                    if (CheckEnviroment(person, personEnvir) == null)
                        return person.GetLastDecision();
                    else
                        return CheckEnviroment(person, personEnvir);
                case State.Fighting:
                    return Actions.Action.Attacking(person.GetOpponent());
                case State.Ready:
                    if (CheckEnviroment(person, personEnvir) == null)
                    {
                        switch (person.GetLastDecision().NextAction)
                        {
                            case ActionType.MoveToCrafthouse:
                                    return Actions.Action.GoingToCrafthouse(settlement, personEnvir);
                            case ActionType.Craft:
                                    return Actions.Action.Crafting(settlement, personEnvir);
                            case ActionType.MoveToTrader:
                                    return Actions.Action.GoingToTrader(settlement, personEnvir);
                            case ActionType.Sell:
                                    return Actions.Action.Sell(settlement, personEnvir);
                            case ActionType.BuyInstruments:
                                    return Actions.Action.BuyingInstruments(settlement, personEnvir);
                            case ActionType.Free:
                                    return Actions.Action.Free();

                        }
                        return Actions.Action.Free();
                    }
                    else
                        return CheckEnviroment(person, personEnvir);
                case State.Run:
                    if (CheckEnviroment(person,personEnvir)!=null)
                        return CheckEnviroment(person, personEnvir);
                    else
                        return Actions.Action.Free();
                case State.Working:
                    if (CheckEnviroment(person, personEnvir) == null)
                        return person.GetLastDecision();
                    else
                        return CheckEnviroment(person, personEnvir);

            }
            return Actions.Action.Free();
        }
        /// <summary>
        /// Проверка на опсность для жизни.
        /// В случае опасности стратегия переключается на безопасную.
        /// </summary>
        /// <param name="person">Персонаж.</param>
        /// <returns>В опасности или нет.</returns>
        private static bool InDangerous(IPersonToStrategy person)
        {
            if (person.GetHp() < 25)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Проверка специально для ремесленника на присутствие разбойников в поле зрения.
        /// </summary>
        /// <param name="person">Персонаж.</param>
        /// <param name="pEnvir">Персонажи  области видимости.</param>
        private static Run CheckEnviroment(IPersonToStrategy person, PersonalEnviroment pEnvir)
        {
            foreach (IPersonToStrategy p in pEnvir.NearestCharacters)
            {
                if (p.GetProfession() == Profession.Robber)
                {
                    var run = new Run {Opponent = (Person) p};
                    person.SetOpponent((Person)p);
                    run.NewState = State.Run;
                    run.NextAction = ActionType.Attack;
                    return run;
                }
            }
            return null;
        }

    }
}
