﻿using System;
using System.Drawing;
using World.Characters.Professions;
using World.Characters.Strategies;
using World.Enviroment;

namespace World.Characters
{
    public class PersonFactory
    {
        private int id;
        private readonly WarriorPrototype _warriorPrototype;
        private readonly TraderPrototype _traderPrototype;
        private readonly RobberPrototype _robberPrototype;
        private readonly PeasantPrototype _peasantPrototype;
        private readonly CraftsmanPrototype _craftsmanPrototype;
        /// <summary>
        /// Получить персонажа случайной проффессии.
        /// </summary>
        /// <param name="settlement"></param>
        /// <returns></returns>
        public Person GetMan()
        {
            Person man = new Person(id);
            byte luck = (byte)RandomContainer.Random.Next(1, 6);
            if (luck == 1)
                man = _warriorPrototype.Clone();
            if (luck == 2)
                man = _traderPrototype.Clone();
            if (luck == 3)
                man = _robberPrototype.Clone();
            if (luck == 4)
                man = _peasantPrototype.Clone();
            if (luck == 5)
                man = _craftsmanPrototype.Clone();
            man.Id = id++;
            return man;
        }
        public PersonFactory(Habitat settlement)
        {
            _warriorPrototype = new WarriorPrototype(settlement);
            _traderPrototype = new TraderPrototype(settlement);
            _robberPrototype = new RobberPrototype(settlement);
            _peasantPrototype = new PeasantPrototype(settlement);
            _craftsmanPrototype = new CraftsmanPrototype(settlement);
            id = 0;
        }
    }

    abstract class PersonPrototype
    {
        protected Person prototype;
        private readonly IProfessionDecorator decorator;
        private Habitat settlemtnt;
        protected static int id = 0;

        protected PersonPrototype(Habitat setllement, IProfessionDecorator decorator)
        {
            this.prototype = new Person(0);
            this.decorator = decorator;
            this.settlemtnt = setllement;
        }

        public Person Clone()
        {
            Person newMan = this.prototype.Clone();
            decorator.TakeProfession(newMan, settlemtnt);
            return newMan;
        }
    }

    class WarriorPrototype : PersonPrototype
    {
        private static WarriorDecorator decorator = new WarriorDecorator();
        public WarriorPrototype(Habitat settlement)
            : base(settlement, decorator)
        {
        }
    }

    class TraderPrototype : PersonPrototype
    {
        private static TraderDecorator decorator = new TraderDecorator();
        public TraderPrototype(Habitat setllement)
            : base(setllement, decorator)
        {
        }
    }

    class RobberPrototype : PersonPrototype
    {
        private static RobberDecorator decorator = new RobberDecorator();
        public RobberPrototype(Habitat setllement)
            : base(setllement, decorator)
        {
            decorator.TakeProfession(prototype, setllement);
        }
    }

    class CraftsmanPrototype : PersonPrototype
    {
        private static CraftsmanDecorator decorator = new CraftsmanDecorator();

        public CraftsmanPrototype(Habitat setllement)
            : base(setllement, decorator)
        {

        }

    }

    class PeasantPrototype : PersonPrototype
    {
        private static PeasantDecorator decorator = new PeasantDecorator();
        public PeasantPrototype(Habitat setllement)
            : base(setllement, decorator)
        {
        }

    }

    /// <summary>
    /// Фундаментальный класс человека.
    /// </summary>
    public class Person : IPersonToStrategy
    {
        int _lvl;           //  Базовые параметры
        int _hp;            //  каждой  личности.
        int _maxHp;         // 
        int _bag;           //
        Equipment _eq;      //  

        public Person Clone()
        {
            return (Person)this.MemberwiseClone();
        }
        /// <summary>
        /// Текущее здоровье.
        /// </summary>
        public int Health
        {
            get { return _hp; }
            set
            {
                if (value <= MaxHealth)
                {
                    _hp = value;
                }
            }
        }
        /// <summary>
        /// Максимальный запас очков здоровья.
        /// </summary>
        private int MaxHealth
        {
            get { return _maxHp; }
            set
            {
                if (value > 0)
                    _maxHp = value;
            }
        }
        /// <summary>
        /// Опыт.
        /// При значении больше 100 автоматически происходит увеличение уровня.
        /// </summary>
        private int Experience { get; set; }
        /// <summary>
        /// Уровень.
        /// При увеличении показатель максимального здороья автоматически увеличивается на 10.
        /// </summary>
        int Level
        {
            get { return _lvl; }
            set
            {
                _lvl = value;
                if (_lvl >= 1)
                {
                    MaxHealth += 25;
                }
            }
        }
        /// <summary>
        /// Скорость передвижения.
        /// </summary>
        public int Speed { get; set; }
        /// <summary>
        /// Обзор.
        /// </summary>
        public int Rewiew { get; set; }
        /// <summary>
        /// Заплечный мешок.
        /// </summary>
        int Bag
        {
            get { return _bag; }
            set
            {
                if (value >= 0)
                    _bag = value;
            }
        }
        /// <summary>
        /// Индикатор состояния персонажа.
        /// </summary>
        private State Status { get; set; }
        /// <summary>
        /// Место нахождения.
        /// </summary>
        Point _location;
        /// <summary>
        /// Профессия персонажа.
        /// </summary>
        public Profession Profession;
        /// <summary>
        /// Текущая стратегия.
        /// </summary>
        public IStrategy Strategy { get; set; }
        /// <summary>
        /// Идентификационный номер.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Последнее решение персонажа.
        /// </summary>
        Decision _decision;
        /// <summary>
        /// Оппонент для действия.
        /// </summary>
        private Person _opponent;


        /// <summary>
        /// Конструктор персонажа.
        /// Устанавливает количество опыта = 0, уровень = 0, заплечный мешок пуст,
        /// максимальное количество здоровья = рандом от 90 до 110, текущее здоровье = максимальному колич здороья( т.к. только родился).
        /// Положение на карте, стратегия поведения обзор и скорость устанавливаются в зависимости от професии.
        /// </summary>
        /// <param name="id">Идентификационный номер.</param>
        public Person(int id)
        {
            Experience = 0;
            Level = 0;
            MaxHealth = RandomContainer.Random.Next(90, 110);
            _eq = new Equipment(Equipment.Standart.Weapons.Hands, Equipment.Standart.Armors.NoneArmor, 0);
            Health = MaxHealth;
            Bag = 0;
            this.Id = id;
            Status = State.LastActionCompleted;
            SetDecision(Actions.Action.Free());
        }


        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////  Setters
        //////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Изменение снаряжения.
        /// Использовать при создании персонажа.
        /// </summary>
        /// <param name="newArmor">Новая броня.</param>
        /// <param name="newCash">Новое количество денег. -1 = не изменять.</param>
        /// <param name="newWeapon">Новое оружие.</param>
        public void SetEqip(Equipment.Weapon newWeapon, Equipment.Armor newArmor, int newCash)
        {
            _eq.ChangeWeapon(newWeapon);
            _eq.ChangeArmor(newArmor);
            _eq.NewCash(newCash);
        }
        /// <summary>
        /// Изменить индикатор состояния.
        /// </summary>
        /// <param name="newState">Новый индикатор.</param>
        public void SetStatus(State newState)
        {
            Status = newState;
        }
        /// <summary>
        /// Наполнить мешок.
        /// </summary>
        /// <param name="newbag">Сколько всего лежит в мешке.</param>
        public void SetFillingBag(int newbag)
        {
            Bag = newbag;
        }
        /// <summary>
        /// Задать местонахождение персонажа.
        /// </summary>
        /// <param name="newLocation">Новое положение .</param>
        public void SetLocation(System.Drawing.Point newLocation)
        {
            _location = newLocation;
        }
        /// <summary>
        /// Добавить решении о действии.
        /// </summary>
        /// <param name="newDecision"></param>
        public void SetDecision(Decision newDecision)
        {
            _decision = newDecision;
        }
        /// <summary>
        /// Установить оппонента для последующих действий.
        /// </summary>
        /// <param name="op"></param>
        public void SetOpponent(Person op)
        {
            _opponent = op;
        }
        /// <summary>
        /// Изменить професиию персонажа.
        /// </summary>
        /// <param name="newProf">Новая профессия.</param>
        public void SetProfession(Profession newProf)
        {
            ResetExp();
            switch (newProf)
            {
                case Profession.Warrior:
                    _eq.ChangeWeapon(Equipment.Standart.Weapons.Sword);
                    _eq.ChangeArmor(Equipment.Standart.Armors.MediumArmor);
                    _eq.ChangeCash(10);
                    Strategy = new WarriorStrategy();
                    Rewiew = 90;
                    Speed = 80;
                    Profession = Profession.Warrior;
                    Status = State.LastActionCompleted;
                    break;
                case Profession.Trader:
                    _eq = new Equipment(Equipment.Standart.Weapons.Hands, Equipment.Standart.Armors.NoneArmor, 100);
                    Strategy = new TraderStrategy();
                    Rewiew = 100;
                    Speed = 100;
                    Profession = Profession.Trader;
                    Status = State.Moving;
                    break;
                case Profession.Robber:
                    _eq.NewCash(10);
                    Rewiew = 110;
                    Speed = 125;
                    Status = State.LastActionCompleted;
                    Strategy = new RobberStrategy();
                    Profession = Profession.Robber;
                    break;
                case Profession.Peasant:
                    _eq.ChangeWeapon(Equipment.Standart.Weapons.Hands);
                    _eq.ChangeArmor(Equipment.Standart.Armors.NoneArmor);
                    _eq.NewCash(10);
                    Status = State.LastActionCompleted;
                    Rewiew = 100;
                    Speed = 100;
                    Strategy = new PeasantStrategy();
                    Profession = Profession.Peasant;
                    break;
                case Profession.Craftsman:
                    Speed = 80;
                    _eq.ChangeWeapon(Equipment.Standart.Weapons.Hands);
                    _eq.ChangeArmor(Equipment.Standart.Armors.UsualArmor);
                    _eq.NewCash(10);
                    Rewiew = 100;
                    Status = State.LastActionCompleted;
                    Strategy = new CraftsmanStrategy();
                    Profession = Profession.Craftsman;
                    break;
            }
        }

        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// Getters
        ////////////////////////////////////////////////////////////////////////// 

        /// <summary>
        /// Получить количество хп персонажа.
        /// </summary>
        /// <returns>Текущее здоровье.</returns>
        public int GetHp()
        {
            return Health;
        }
        /// <summary>
        /// Получить экипировку персонажа.
        /// </summary>
        /// <returns>Текущая экипировка.</returns>
        public Equipment GetEquipment()
        {
            return _eq;
        }
        /// <summary>
        /// Получить бонус за текущий уровень.
        /// </summary>
        /// <returns></returns>
        public int GetLvlBonus()
        {
            return Level > 0 ? Level : 1;
        }

        /// <summary>
        /// Получить дальность обзора персонажа.
        /// </summary>
        /// <returns>Область видимости.</returns>
        public int GetRewiew()
        {
            return Rewiew;
        }
        /// <summary>
        /// Получить скорость персонажа (Зависит от тяжести сумки).
        /// </summary>
        /// <returns>Показатель скорости.</returns>
        public int GetSpeed()
        {
            return Convert.ToInt32(Speed - Bag * 0.3);
        }
        /// <summary>
        /// Получить  действие.
        /// </summary>
        /// <returns>Действие.</returns>
        public Decision GetLastDecision()
        {
            return _decision;
        }
        /// <summary>
        /// Получить текущий статус персонажа.
        /// </summary>
        /// <returns>Текущий статус.</returns>
        public State GetStatus()
        {
            return Status;
        }
        /// <summary>
        /// Получить, на сколько заполнена сумка(максимум 100 единиц).
        /// </summary>
        /// <returns>Заполненность сумки.</returns>
        public int GetFillingBag()
        {
            return Bag;
        }
        /// <summary>
        /// Получить текущую профессию персонажа.
        /// </summary>
        /// <returns>Профессия на данны момент.</returns>
        public Profession GetProfession()
        {
            return Profession;
        }
        /// <summary>
        /// Получить точку местонахождения персонажа.
        /// </summary>
        /// <returns>Локация персонаа.</returns>
        public System.Drawing.Point GetLocation()
        {
            return _location;
        }
        /// <summary>
        /// Получить персональный идентификатор персонажа.
        /// </summary>
        /// <returns>ID персонажа.</returns>
        public int GetId()
        {
            return Id;
        }
        /// <summary>
        /// Получить текущую стратегию действий персонажа.
        /// </summary>
        /// <returns>Текущая стратегия.</returns>
        public IStrategy GetStrategy()
        {
            return Strategy;
        }
        /// <summary>
        /// Получить ссылку на оппонента для действий.
        /// </summary>
        /// <returns>Оппонент для действия.</returns>
        public Person GetOpponent()
        {
            return _opponent;
        }

        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// Special Actions
        ////////////////////////////////////////////////////////////////////////// 

        public Decision TakeDecision(PersonalEnviroment personalEnviroment, Habitat settlement)
        {
            return Strategy.TakeDecision(this, personalEnviroment, settlement);
        }
        /// <summary>
        /// Сброс опыта и уровня.
        /// </summary>
        public void ResetExp()
        {
            Experience = 0;
            Level = 0;
        }
        /// <summary>
        /// Изменение количества здоровья на определенную величину.
        /// </summary>
        /// <param name="delta">Сколько прибавить ( - если убавить) .</param>
        public void ChangeHp(int delta)
        {
            if (Health + delta <= 0)
                Health = 0;
            else
            {
                Health += delta;
            }
        }
        /// <summary>
        /// Изменения снаряжения.
        /// Может получить новое оружие либо новую броню.
        /// </summary>
        /// <param name="newWeapon">Новое оружие.</param>
        public void ChangeEqip(Equipment.Weapon newWeapon)
        {
            if (newWeapon != null) _eq.ChangeWeapon(newWeapon);
        }
        /// <summary>
        /// Изменения снаряжения.
        /// </summary>
        /// <param name="newArmor">Новая броня.</param>
        public void ChangeEqip(Equipment.Armor newArmor)
        {
            if (newArmor != null) _eq.ChangeArmor(newArmor);
        }
        /// <summary>
        /// Добавление суммы к наличным.
        /// </summary>
        /// <param name="delta">Сколько прибавить монет.</param>
        public void AddMoney(int delta)
        {
            _eq.ChangeCash(delta);
        }
        /// <summary>
        /// Взять сумму из кошелька.
        /// </summary>
        /// <param name="delta">Сколько нужно взять монет.</param>
        /// <returns>Изъятые монеты.</returns>
        public int TakeMoney(int delta)
        {
            if (delta <= _eq.HowMoneyInCash())
            {
                _eq.ChangeCash(-delta);
                return delta;
            }
            else
                return 0;
        }
        /// <summary>
        /// Изъять все деньги из кошелька.
        /// </summary>
        /// <returns>Количество монет всего в кошельке(было).</returns>
        public int TakeAllMoney()
        {
            int n = _eq.HowMoneyInCash();
            _eq.ChangeCash(-n);
            return n;
        }
        /// <summary>
        /// Добавить что либо в сумку.
        /// </summary>
        /// <param name="adding">Сколько формальных единиц добавить.</param>
        public void AddInBag(int adding)
        {
            Bag += adding;
        }
        /// <summary>
        /// Изменить стратегию.
        /// </summary>
        /// <param name="newstrategy"></param>
        public void ChangeStrategy(IStrategy newstrategy)
        {
            Strategy = newstrategy;
        }
        /// <summary>
        /// Увеличить опыт персонажа.
        /// Плюшки от уровня добавляется автомотически в заисимости профессии.
        /// </summary>
        /// <param name="x"></param>    
        public void AddExp(int x)
        {
            if (Experience + x > 1000)
            {
                Level++;
                switch (Profession)
                {
                    case Profession.Trader:
                        if (Level == 5)
                        {
                            _eq.ChangeArmor(Equipment.Standart.Armors.UsualArmor);
                            Speed -= 1;
                        }
                        if (Level == 8)
                        {
                            _eq.ChangeArmor(Equipment.Standart.Armors.MediumArmor);
                            Speed -= 1;
                        }
                        AddMoney(1000);
                        break;
                    case Profession.Warrior:
                        if (Level == 7 && _eq.EquipCompare(Equipment.Standart.Weapons.Axe))
                        {
                            _eq.ChangeWeapon(Equipment.Standart.Weapons.Axe);
                            Speed -= 2;
                        }
                        if (Level == 5 && _eq.EquipCompare(Equipment.Standart.Armors.HardArmor))
                        {
                            _eq.ChangeArmor(Equipment.Standart.Armors.HardArmor);
                            Speed -= 2;
                        }
                        MaxHealth += 20;
                        break;
                    case Profession.Robber:
                        if (Level == 7 && _eq.EquipCompare(Equipment.Standart.Weapons.Sword))
                        {
                            Speed -= 2;
                            _eq.ChangeWeapon(Equipment.Standart.Weapons.Sword);
                        }
                        if (Level == 5 && _eq.EquipCompare(Equipment.Standart.Armors.MediumArmor))
                        {
                            _eq.ChangeArmor(Equipment.Standart.Armors.MediumArmor);
                            Speed -= 2;
                        }
                        Speed += 1;
                        MaxHealth += 5;
                        break;
                    case Profession.Peasant:
                        if (Level == 5 && _eq.EquipCompare(Equipment.Standart.Armors.UsualArmor))
                        {
                            _eq.ChangeArmor(Equipment.Standart.Armors.UsualArmor);
                            Speed -= 1;
                        }
                        if (Level == 7 && _eq.EquipCompare(Equipment.Standart.Weapons.Dagger))
                        {
                            _eq.ChangeWeapon(Equipment.Standart.Weapons.Dagger);
                            Speed -= 1;
                        }
                        MaxHealth += 3;
                        break;
                    case Profession.Craftsman:
                        if (Level == 5 && _eq.EquipCompare(Equipment.Standart.Armors.MediumArmor))
                        {
                            _eq.ChangeArmor(Equipment.Standart.Armors.MediumArmor);
                            Speed -= 1;
                        }
                        if (Level == 7 && _eq.EquipCompare(Equipment.Standart.Weapons.Sword))
                        {
                            _eq.ChangeWeapon(Equipment.Standart.Weapons.Sword);
                            Speed -= 1;
                        }
                        break;
                }
                Experience = Experience + x - 1000;
            }
            Experience += x;
        }

        //////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// 
    }
    /// <summary>
    /// Контейнер названий профессий.
    /// </summary>
    public enum Profession
    {
        Trader, Craftsman, Robber, Peasant, Warrior
    }
}
