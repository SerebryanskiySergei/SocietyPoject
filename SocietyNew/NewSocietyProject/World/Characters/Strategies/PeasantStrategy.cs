using System.Drawing;
using World.Enviroment;

namespace World.Characters.Strategies
{
    class PeasantStrategy : IStrategy
    {
        public Decision TakeDecision(IPersonToStrategy person, PersonalEnviroment personEnvir, Habitat settlement)
        {
            if (person.GetStatus() == State.Died)
                return null;
            if (person.GetEquipment().HowMoneyInCash() < 10)
            {
                return new ChangeProfession
                {
                    NewProfession = Profession.Robber,
                    NewState = State.LastActionCompleted
                };
            }
            if (inDangerous(person))
            {
                person.ChangeStrategy(new SafeStrategy());
                return person.GetStrategy().TakeDecision(person, personEnvir, settlement);
            }
            switch (person.GetStatus())
            {
                case State.LastActionCompleted:
                    if (CheckEnviroment(person, personEnvir, settlement) == null)
                        return Actions.Action.GoingToFarm(settlement, personEnvir);
                    else
                        return CheckEnviroment(person, personEnvir, settlement);
                case State.Ready:
                    if (CheckEnviroment(person, personEnvir, settlement) == null)
                        switch (person.GetLastDecision().NextAction)
                        {
                            case ActionType.Work:
                                return Actions.Action.Work(settlement, personEnvir);
                            case ActionType.MoveToTrader:
                                return Actions.Action.GoingToTrader(settlement, personEnvir);
                            case ActionType.Sell:
                                return Actions.Action.Sell(settlement, personEnvir);
                            case ActionType.Free:
                                return Actions.Action.Free();
                        }
                    else
                        return CheckEnviroment(person, personEnvir, settlement);
                    return Actions.Action.Free();
                case State.Working:
                    if (CheckEnviroment(person, personEnvir, settlement) == null)
                        return person.GetLastDecision();
                    else
                        return CheckEnviroment(person, personEnvir, settlement);
                case State.Moving:
                    if (CheckEnviroment(person, personEnvir, settlement) == null)
                        return person.GetLastDecision();
                    else
                        return CheckEnviroment(person, personEnvir, settlement);
                case State.Run:
                    return person.GetLastDecision();
                case State.Fighting:
                    return Actions.Action.Attacking(person.GetOpponent());
            }
            return Actions.Action.Free();
        }

        private bool inDangerous(IPersonToStrategy person)
        {
            if (person.GetHp() < 25)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Проверка специально для крестьянина на присутствие разбойников в поле зрения.
        /// </summary>
        /// <param name="person">Персонаж.</param>
        /// <param name="pEnvir">Персонажи  области видимости.</param>
        private Run CheckEnviroment(IPersonToStrategy person, PersonalEnviroment pEnvir, Habitat setllement)
        {
            foreach (IPersonToStrategy p in pEnvir.NearestCharacters)
            {
                if (p.GetProfession() == Profession.Robber)
                {
                    Run run = new Run();
                    run.Destination = new Point(setllement.Castle.Location.X + setllement.Castle.Width/2,
                        setllement.Castle.Y + setllement.Castle.Height/2);
                    foreach (Person man in pEnvir.NearestCharacters)
                    {
                        if (man.GetProfession() == Profession.Warrior)
                        {
                            run.Opponent = man;
                            break;
                        }
                    }
                    run.NewState = State.Run;
                    run.NextAction = ActionType.Free;
                    return run;
                }
            }
            return null;
        }
    }
}
