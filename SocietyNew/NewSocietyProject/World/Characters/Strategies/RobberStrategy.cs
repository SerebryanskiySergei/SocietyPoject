using System.Drawing;
using World.Enviroment;

namespace World.Characters.Strategies
{
    class RobberStrategy : IStrategy
    {
        public Decision TakeDecision(IPersonToStrategy person,PersonalEnviroment personEnvir, Habitat settlement)
        {
            if (person.GetStatus() == State.Died)
                return null;
            switch (person.GetStatus())
            {
               case State.LastActionCompleted:
                    if (CheckEnviroment(person, personEnvir) == null)
                        return Actions.Action.Patroling(settlement);
                    else
                        return CheckEnviroment(person, personEnvir);
                case State.Moving:
                    if (CheckEnviroment(person, personEnvir) == null)
                        return person.GetLastDecision();
                    else
                        return CheckEnviroment(person, personEnvir);
                case State.Fighting:
                {
                    if (person.GetLocation()==person.GetOpponent().GetLocation())
                        return Actions.Action.Attacking(person.GetOpponent());
                    else
                        return CheckEnviroment(person, personEnvir);
                }
                case State.Ready:
                    if (CheckEnviroment(person, personEnvir) == null)
                    {
                        switch (person.GetLastDecision().NextAction)
                        {
                            case ActionType.Attack:
                                return Actions.Action.Attacking(person.GetOpponent());
                            case ActionType.StealMoney:
                                return Actions.Action.StealMoney(person.GetOpponent());
                            case ActionType.Free:
                                return Actions.Action.Free();
                        }
                    }
                    else
                        return CheckEnviroment(person, personEnvir);
                    return null;
                case State.Run:
                    if (personEnvir.NearestCharacters.Contains(person.GetOpponent()))
                        return person.GetLastDecision();
                    else
                        return Actions.Action.Free();
            }
            return Actions.Action.Free();
        }
        /// <summary>
        /// Проверка на опсность для жизни.
        /// В случае опасности стратегия переключается на безопасную.
        /// </summary>
        /// <param name="person">Персонаж.</param>
        /// <returns>В опасности или нет.</returns>
        private bool InDangerous(IPersonToStrategy person)
        {
            if (person.GetHp() < 25)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Проверка специально для разбойника на присутствие воинов в поле зрения.
        /// </summary>
        /// <param name="person">Персонаж.</param>
        /// <param name="pEnvir">Персонажи  области видимости.</param>
        private Run CheckEnviroment(IPersonToStrategy person, PersonalEnviroment pEnvir)
        {
            foreach (IPersonToStrategy p in pEnvir.NearestCharacters)
            {
                if (p.GetProfession() == Profession.Warrior)
                {
                    Run run = new Run();
                    run.Destination= new Point(-p.GetLocation().X,-p.GetLocation().Y);
                    run.NewState = State.Run;
                    run.NextAction = ActionType.Attack;
                    return run;
                }
                if (p.GetProfession() == Profession.Peasant &&
                    (person.GetEquipment().EquipCompare(p.GetEquipment().GetWeapon()) ||
                     person.GetEquipment().EquipCompare(p.GetEquipment().GetArmor()) ||
                    p.GetEquipment().HowMoneyInCash() > 0))
                {
                    if (person.GetLocation() == p.GetLocation())
                    {
                        return null;
                    }
                    else
                    {
                        var run = new Run
                        {
                            Opponent = (Person) p,
                            NewState = State.Run,
                            NextAction = ActionType.StealMoney
                        };
                        return run;
                    }
                }
                if (p.GetProfession() == Profession.Craftsman && (person.GetEquipment().EquipCompare(p.GetEquipment().GetWeapon()) ||
                     person.GetEquipment().EquipCompare(p.GetEquipment().GetArmor()) ||
                    p.GetEquipment().HowMoneyInCash() > 0))
                {
                    var run = new Run {Opponent = (Person) p, NewState = State.Run, NextAction = ActionType.Attack};
                    return run;
                }
            }
            return null;
        }
    }
}
