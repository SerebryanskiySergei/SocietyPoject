using System.Drawing;
using World.Enviroment;

namespace World.Characters.Strategies
{
    class SafeStrategy : IStrategy
    {
        public Decision TakeDecision(IPersonToStrategy person, PersonalEnviroment pEnvir, Habitat settlement)
        {
            if (person.GetStatus() == State.Died)
                return null;
            if (person.GetHp() > 25)
            {
                person.SetDecision(Actions.Action.Free());
                switch (person.GetProfession())
                {
                    case Profession.Craftsman:
                        person.ChangeStrategy(new CraftsmanStrategy());
                        return person.GetLastDecision();
                    case Profession.Peasant:
                        person.ChangeStrategy(new PeasantStrategy());
                        return person.GetLastDecision();
                    case Profession.Robber:
                        person.ChangeStrategy(new RobberStrategy());
                        return person.GetLastDecision();
                    case Profession.Trader:
                        person.ChangeStrategy(new TraderStrategy());
                        return person.GetLastDecision();
                    case Profession.Warrior:
                        person.ChangeStrategy(new WarriorStrategy());
                        return person.GetLastDecision();
                }
            }
            if (person.GetStatus() == State.Ready)
            {
                if (person.GetLastDecision().NextAction == ActionType.BuyMedicine)
                {
                    BuyingMedicine decision = new BuyingMedicine();
                    decision.NewState = State.Ready;
                    foreach (IPersonToStrategy p in pEnvir.NearestCharacters)
                    {
                        if (p.GetProfession() == Profession.Trader)
                            decision.Opponent = (Person) p;
                    }
                    decision.NextAction = ActionType.Free;
                    return decision;
                }
            }
            foreach (IPersonToStrategy p in pEnvir.NearestCharacters)
            {
                if (p.GetProfession() == Profession.Trader)
                {
                    Run decision = new Run();
                    decision.NewState = State.Run;
                    decision.NextAction = ActionType.BuyMedicine;
                    decision.Destination = p.GetLocation();
                    return decision;
                }
            }
            if (person.GetLocation() == settlement.MarketPlace[0].place.Location)
            {
                var decision = new BuyingMedicine {NewState = State.Ready};
                foreach (IPersonToStrategy p in pEnvir.NearestCharacters)
                {
                    if (p.GetProfession() == Profession.Trader)
                        decision.Opponent = (Person)p;
                }
                decision.NextAction = ActionType.Free;
                return decision;
            }
            var run = new Run();
            foreach (IPersonToStrategy p in pEnvir.NearestCharacters)
            {
                if (p.GetProfession() == Profession.Trader)
                {
                    run.Destination = p.GetLocation();
                    run.NewState = State.Run;
                    run.NextAction = ActionType.BuyMedicine;
                    return run;
                }
            }
            run.Destination = new Point(settlement.MarketPlace[0].place.X, settlement.MarketPlace[0].place.Y);
            return run;
        }

    }
}
