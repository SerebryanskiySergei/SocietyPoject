using World.Enviroment;

namespace World.Characters.Strategies
{
    class WarriorStrategy : IStrategy
    {
        public Decision TakeDecision(IPersonToStrategy person, PersonalEnviroment pEnvir, Habitat settlement)
        {
            if (person.GetStatus() == State.Died)
                return null;
            if (person.GetEquipment().HowMoneyInCash() < 10)
                return new ChangeProfession
                {
                    NewState = State.LastActionCompleted,
                    NewProfession = Profession.Craftsman
                };
            if (inDangerous(person) && person.GetStatus()!=State.Fighting)
            {
                person.ChangeStrategy (new SafeStrategy());
                person.SetDecision(person.GetStrategy().TakeDecision((IPersonToStrategy)person,pEnvir,settlement));
                return person.GetLastDecision();
            }
            switch (person.GetStatus())
            {
                case State.LastActionCompleted:
                    if (CheckEnviroment(person, pEnvir) == null)
                        return Actions.Action.Patroling(settlement);
                    else
                        return CheckEnviroment(person, pEnvir);
                case State.Moving:
                    if (CheckEnviroment(person, pEnvir) == null)
                        return person.GetLastDecision();
                    else
                        return CheckEnviroment(person, pEnvir);
                case State.Ready:
                    switch (person.GetLastDecision().NextAction)
                    {
                        case ActionType.Attack:
                            return Actions.Action.Attacking(person.GetOpponent());
                        case ActionType.Free:
                            if (CheckEnviroment(person, pEnvir) != null)
                                return CheckEnviroment(person, pEnvir);
                            return Actions.Action.Free();
                    }
                    return Actions.Action.Free();
                case State.Run:
                    if (CheckEnviroment(person, pEnvir) != null)
                        return CheckEnviroment(person, pEnvir);
                    else
                        return Actions.Action.Free();
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
            if (person.GetHp() < 25)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Проверка специально для война на присутствие разбойников в поле зрения.
        /// </summary>
        /// <param name="person">Персонаж.</param>
        /// <param name="pEnvir">Персонажи  области видимости.</param>
        private Run CheckEnviroment(IPersonToStrategy person, PersonalEnviroment pEnvir)
        {
            foreach (IPersonToStrategy p in pEnvir.NearestCharacters)
            {
                if (p.GetProfession ()==Profession.Robber)
                {
                    var run = new Run {Opponent = (Person) p, NewState = State.Run, NextAction = ActionType.Attack};
                    return run;
                }
            }
            return null;
        }
    }
}
