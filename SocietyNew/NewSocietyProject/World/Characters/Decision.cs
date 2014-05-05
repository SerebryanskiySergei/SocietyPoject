﻿using System;
using System.Drawing;

namespace World.Characters
{
    /// <summary>
    /// Класс решения персонажа о следующем действии.
    /// </summary>
    public abstract class Decision
    {
        /// <summary>
        /// Новый статус персонажа.
        /// </summary>
        public State NewState;
        /// <summary>
        /// Идентификатор следующего действия.
        /// </summary>
        public ActionType NextAction;
        /// <summary>
        /// Метод - осуществялем намеченные действия.
        /// </summary>
        /// <param name="person"></param>
        public abstract void Apply(Person person);
    }
    /// <summary>
    /// Идем в определенную точку на карте.
    /// </summary>
    class MoveToPoint : Decision
    {
        public Point Destination;

        public override void Apply(Person person)
        {
            if ((Destination.X - person.GetLocation().X) != 0 && (Destination.Y - person.GetLocation().Y)!=0)
            {
                double alpha =
                    Math.Atan((Destination.Y - person.GetLocation().Y)/(Destination.X - person.GetLocation().X));
                double speedX = person.GetSpeed()*0.05*Math.Cos(alpha);
                double speedY = person.GetSpeed()*0.05*Math.Sin(alpha);
                if (Math.Sqrt(Math.Pow((person.GetLocation().X - Destination.X), 2) +
                              Math.Pow((person.GetLocation().Y - Destination.Y), 2)) >
                    Math.Sqrt(Math.Pow((speedX), 2) + Math.Pow((speedY), 2)))
                {
                    if (person.GetLocation().X < Destination.X)
                    {
                        person.SetLocation(new Point(person.GetLocation().X + (int) speedX,
                            person.GetLocation().Y + (int) speedY));
                        person.SetStatus(State.Moving);
                    }
                    else
                    {
                        person.SetLocation(new Point(person.GetLocation().X - (int) speedX,
                            person.GetLocation().Y - (int) speedY));
                        person.SetStatus(State.Moving);
                    }
                }
                else
                {
                    person.SetLocation(Destination);
                    person.SetStatus(State.Ready);
                }
            }
            else
            {
                person.SetLocation(Destination);
                person.SetStatus(State.Ready);
            }
        }
    }
    /// <summary>
    /// Работаем.
    /// </summary>
    class Working : Decision
    {
        public override void Apply(Person person)
        {
            if (person.GetFillingBag() >= 100)
            {
                person.AddExp(100);
                person.SetStatus(State.Ready);
            }
            else
            {
                person.SetStatus(State.Working);
                person.AddInBag((int) (RandomContainer.Random.Next(7, 15) + person.GetLvlBonus()*2));
            }
        }
    }
    /// <summary>
    /// Продаем содержимое заплечного мешка.
    /// </summary>
    class Selling : Decision
    {
        public Person Opponent;
        public override void Apply(Person person)
        {
            int money = Opponent.TakeMoney(person.GetFillingBag());
            person.AddMoney(money);
            Opponent.AddExp(100);
            person.SetFillingBag(0);
            person.SetStatus(State.Ready);
        }
    }
    /// <summary>
    /// Покупаем лекарства.
    /// </summary>
    class BuyingMedicine : Decision
    {
        public Person Opponent = null;
        public override void Apply(Person person)
        {
            if (Opponent != null)
            {
                if (person.GetEquipment().HowMoneyInCash()>=25)
                {
                    Opponent.AddMoney(person.TakeMoney(25));
                    person.ChangeHp(50);
                    person.SetStatus(State.LastActionCompleted);
                }
                else
                {
                    person.SetStatus(State.LastActionCompleted);
                }
                
            }
            else
            {
                person.SetStatus(State.Ready);
            }
        }
    }
    /// <summary>
    /// Покупаем инструменты для ремесла.
    /// </summary>
    class BuyingInstruments : Decision
    {
        public Person Opponent;
        public override void Apply(Person person)
        {
            if (person.TakeMoney(50) == 50)
            {
                Opponent.AddMoney(50);
                person.AddExp(50);
                person.SetStatus(NewState);
            }
        }
    }
    /// <summary>
    /// Бежим в точку на карте / к персонажу .
    /// </summary>
    class Run : Decision
    {
        public Person Opponent = null;
        public Point Destination;
        public override void Apply(Person person)
        {
            if (Opponent == null)
            {
                double speedX, speedY;
                if ((Destination.X - person.GetLocation().X) != 0 && (Destination.Y - person.GetLocation().Y) != 0)
                {
                    double alpha =
                        Math.Atan((Destination.Y - person.GetLocation().Y)/(Destination.X - person.GetLocation().X));
                    speedX = person.GetHp() > 35
                        ? person.GetSpeed()*0.07*Math.Cos(alpha)
                        : person.GetSpeed()*0.02*Math.Cos(alpha);
                    speedY = person.GetHp() > 35
                        ? person.GetSpeed()*0.07*Math.Sin(alpha)
                        : person.GetSpeed()*0.02*Math.Sin(alpha);
                }
                else
                {
                    if ((Destination.X - person.GetLocation().X) == 0)
                    {
                        speedY = person.GetHp() > 35 ? person.GetSpeed()*0.15 : person.GetSpeed()*0.1;
                        speedX = 0;
                    }
                    else
                    {
                        speedX = person.GetHp() > 35 ? person.GetSpeed()*0.15 : person.GetSpeed()*0.1;
                        speedY = 0;
                    }
                }
                if (
                    Math.Sqrt(Math.Pow((person.GetLocation().X - Destination.X), 2) +
                              Math.Pow((person.GetLocation().Y - Destination.Y), 2)) >
                    Math.Sqrt(Math.Pow((speedX), 2) + Math.Pow((speedY), 2)))
                {
                    if (person.GetLocation().X < Destination.X)
                    {
                        person.SetLocation(new Point(person.GetLocation().X + (int) speedX,
                            person.GetLocation().Y + (int) speedY));
                        person.SetStatus(State.Run);
                    }
                    else
                    {
                        person.SetLocation(new Point(person.GetLocation().X - (int) speedX,
                            person.GetLocation().Y - (int) speedY));
                        person.SetStatus(State.Run);
                    }
                }
                else
                {
                    person.SetLocation(Destination);
                    person.SetStatus(State.Ready);
                }
            }
            else // opponent != null
            {
                double speedX, speedY;
                if ((Opponent.GetLocation().X - person.GetLocation().X) != 0 &&
                    (Opponent.GetLocation().Y - person.GetLocation().Y) != 0)
                {
                    double alpha =
                        Math.Atan((Opponent.GetLocation().Y - person.GetLocation().Y)/
                                  (Opponent.GetLocation().X - person.GetLocation().X));
                    speedX = person.GetHp() > 35
                        ? person.GetSpeed()*0.07*Math.Cos(alpha)
                        : person.GetSpeed()*0.02*Math.Cos(alpha);
                    speedY = person.GetHp() > 35
                        ? person.GetSpeed()*0.07*Math.Sin(alpha)
                        : person.GetSpeed()*0.02*Math.Sin(alpha);
                }
                else
                {
                    if ((Destination.X - person.GetLocation().X) == 0)
                    {
                        speedY = person.GetHp() > 35 ? person.GetSpeed() * 0.15 : person.GetSpeed() * 0.1;
                        speedX = 0;
                    }
                    else
                    {
                        speedX = person.GetHp() > 35 ? person.GetSpeed() * 0.15 : person.GetSpeed() * 0.1;
                        speedY = 0;
                    }
                }
                if (
                        Math.Sqrt(Math.Pow((person.GetLocation().X - Opponent.GetLocation().X), 2) +
                                  Math.Pow((person.GetLocation().Y - Opponent.GetLocation().Y), 2)) >
                        Math.Sqrt(Math.Pow((speedX), 2) + Math.Pow((speedY), 2)))
                    {
                        if (person.GetLocation().X < Opponent.GetLocation().X)
                        {
                            person.SetLocation(new Point(person.GetLocation().X + (int) speedX,
                                person.GetLocation().Y + (int) speedY));
                            person.SetStatus(State.Run);
                        }
                        else
                        {
                            person.SetLocation(new Point(person.GetLocation().X - (int) speedX,
                                person.GetLocation().Y - (int) speedY));
                            person.SetStatus(State.Run);
                        }
                    }
                    else
                    {
                        person.SetLocation(Opponent.GetLocation());
                        person.SetStatus(State.Ready);
                        if (NextAction == ActionType.Attack)
                        {
                            Opponent.SetStatus(State.Fighting);
                            person.SetStatus(State.Fighting);
                            Opponent.SetOpponent(person);
                            person.SetOpponent(Opponent);
                        }
                        if (NextAction == ActionType.StealMoney)
                        {
                            Opponent.SetStatus(State.Ready);
                            person.SetStatus(State.Ready);
                            person.SetOpponent(Opponent);
                        }
                    }
                
                
            }
        }
    }
    /// <summary>
    /// Сражаемся с персонажем.
    /// </summary>
    class Attack : Decision
    {
        public Person Opponent;
        public override void Apply(Person person)
        {
            if (Opponent != null)
            {
                if (Opponent.GetHp() < person.GetEquipment().GetDamage()*person.GetLvlBonus() -
                    Opponent.GetEquipment().GetStandUp()*Opponent.GetLvlBonus())
                {
                    Opponent.Health = 0;
                    Opponent.SetStatus(State.Died);
                    person.AddExp(100);
                    if (person.GetProfession() == Profession.Robber)
                    {
                        person.AddMoney(Opponent.TakeAllMoney());
                        if (person.GetEquipment().EquipCompare(Opponent.GetEquipment().GetWeapon()))
                            person.GetEquipment().ChangeWeapon(Opponent.GetEquipment().GetWeapon());
                        if (person.GetEquipment().EquipCompare(Opponent.GetEquipment().GetArmor()))
                            person.GetEquipment().ChangeArmor(Opponent.GetEquipment().GetArmor());
                    }
                    person.SetStatus(State.LastActionCompleted);
                    person.SetOpponent(null);
                    Opponent.SetOpponent(null);
                }
                else
                {
                    Opponent.ChangeHp(-(person.GetEquipment().GetDamage()*person.GetLvlBonus() -
                                        Opponent.GetEquipment().GetStandUp()*Opponent.GetLvlBonus()));
                    person.GetEquipment().WeaponUsed();
                    Opponent.GetEquipment().ArmorUsed();
                }
            }

        }
    }
    /// <summary>
    /// Торговец лечится.
    /// </summary>
    class TraderHealing : Decision
    {
        public TraderHealing()
        {
            NewState = State.Ready;
            NextAction = ActionType.Free;
        }

        public override void Apply(Person person)
        {
            person.ChangeHp(50);
            person.AddMoney(-25);
            person.SetStatus(State.Ready);
        }
    }
    /// <summary>
    /// Изменить профессию.
    /// </summary>
    class ChangeProfession : Decision
    {
        public Profession NewProfession;

        public override void Apply(Person person)
        {
            person.SetStatus(NewState);
            person.SetProfession(NewProfession);
            person.SetStatus(State.LastActionCompleted);
            person.SetDecision(null);
        }
    }
    /// <summary>
    /// Украсть деньги у персонажа.
    /// </summary>
    class StealMoney : Decision
    {
        public Person Opponent;

        public override void Apply(Person person)
        {
            person.AddMoney(Opponent.TakeAllMoney());
            person.AddExp(100);
            person.SetStatus(State.LastActionCompleted);
        }
    }
    /// <summary>
    /// Торгуем.
    /// </summary>
    class Trade : Decision
    {

        public override void Apply(Person person)
        {
            person.SetStatus(NewState);
        }
    }
    /// <summary>
    /// Бездействуем / отдыхаем.
    /// </summary>
    class NoneAction : Decision
    {

        public override void Apply(Person person)
        {
            person.SetStatus(NewState);
        }
    }
}

