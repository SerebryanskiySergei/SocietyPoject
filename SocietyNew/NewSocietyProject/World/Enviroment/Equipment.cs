namespace World.Enviroment
{
    public class Equipment
    {
        Weapon _personalWeapon;
        Armor _personalArmor;
        Cash _personalCash;

        /// <summary>
        /// Контейнер всех объектов экипировки.
        /// </summary>
        public static class Standart
        {
            public static class Weapons
            {
                /// <summary>
                /// Стандартное оружие. Кинжал.
                /// </summary>
                public static Weapon Dagger = new Weapon(1);
                /// <summary>
                /// Стандартное оружие. Меч.
                /// </summary>
                public static Weapon Sword = new Weapon(2);
                /// <summary>
                /// Стандартное оружие. Топор.
                /// </summary>
                public static Weapon Axe = new Weapon(3);
                /// <summary>
                /// Нет оружия. Сражаемся руками.
                /// </summary>
                public static Weapon Hands = new Weapon(0);
            }
            public static class Armors
            {
                /// <summary>
                /// Нет брони.
                /// </summary>
                public static Armor NoneArmor = new Armor(0);
                /// <summary>
                /// Легкая броня.
                /// </summary>
                public static Armor UsualArmor = new Armor(1);
                /// <summary>
                /// Броня среднего класса.
                /// </summary>
                public static Armor MediumArmor = new Armor(2);
                /// <summary>
                /// Очень хорошая броня. 
                /// </summary>
                public static Armor HardArmor = new Armor(3);
            }
        }
        /// <summary>
        /// Оружие. Степени качества от 0 до 3.
        /// </summary>
        public class Weapon
        {
            int _damage;
            int _state;
            public int Damage
            {
                get { return _damage; }
                set
                {
                    if (value > 0)
                        _damage = value;
                }
            }
            public int Estatement
            {
                get { return _state; }
                set { if (value >= 0) _state = value; }
            }
            public Weapon(int degree)
            {
                switch (degree)
                {
                    case 0:

                        Damage = 5;
                        Estatement = 100;
                        break;
                    case 1:
                        Damage = 10;
                        Estatement = 10;
                        break;
                    case 2:
                        Damage = 15;
                        Estatement = 15;
                        break;
                    case 3:
                        Damage = 17;
                        Estatement = 15;
                        break;
                }
            }
            public Weapon(Weapon newWeapon)
            {
                Damage = newWeapon.Damage;
                Estatement = newWeapon.Estatement;
            }
            public bool Compare(Weapon w2)
            {
                if (this.Damage < w2.Damage)
                    return true;
                else
                {
                    if (this.Damage == w2.Damage && this.Estatement < w2.Estatement)
                        return true;
                    else
                        return false;
                }
            }
        }
        /// <summary>
        /// Броня. Степень качества от 0 до 3.
        /// </summary>
        public class Armor
        {
            int _standUp;
            int _state;

            public int StandUp
            {
                get { return _standUp; }
                set { if (value > 0) _standUp = value; }
            }
            public int Estatement
            {
                get { return _state; }
                set { if (value >= 0)_state = value; }
            }
            public Armor(int degree)
            {
                switch (degree)
                {
                    case 0:
                        StandUp = 0;
                        Estatement = 0;
                        break;
                    case 1:
                        StandUp = 1;
                        Estatement = 10;
                        break;
                    case 2:
                        StandUp = 2;
                        Estatement = 20;
                        break;
                    case 3:
                        StandUp = 3;
                        Estatement = 30;
                        break;
                }
            }
            public bool Compare(Armor arm)
            {
                if (this.StandUp < arm.StandUp)
                    return true;
                else
                    if (this.StandUp == arm.StandUp && this.Estatement < arm.Estatement)
                        return true;
                return false;
            }

        }
        /// <summary>
        /// Наличные.
        /// </summary>
        public class Cash
        {
            int count;
            /// <summary>
            /// Наличные.
            /// </summary>
            /// <param name="n">Количество монет.</param>
            public Cash(int n)
            {
                count = n;
            }
            public void Add(int n)
            {
                count += n;
            }
            public int Get(int n)
            {
                if (count - n >= 0)
                {
                    count -= n;
                    return n;
                }
                else
                {
                    return 0;
                }
            }
            public int Quantity()
            {
                return count;
            }
        }
        /// <summary>
        /// Создание экипировки с нуля. 
        /// Использовать при создании персонажа.
        /// </summary>
        /// <param name="w">Новое оружие.</param>
        /// <param name="a">Новая броня.</param>
        /// <param name="money">Количество монет.</param>
        public Equipment(Weapon w, Armor a,int money)
        {
            _personalWeapon = w;
            _personalArmor = a;
            _personalCash = new Cash(money);
        }

        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// Setters
        ////////////////////////////////////////////////////////////////////////// 

        /// <summary>
        /// Смена оружия.
        /// </summary>
        /// <param name="newWp">Новое оружие.</param>
        public void ChangeWeapon(Weapon newWp)
        {
            _personalWeapon = newWp;
        }
        /// <summary>
        /// Смена брони.
        /// </summary>
        /// <param name="newAr">Новая броня.</param>
        public void ChangeArmor(Armor newAr)
        {
            _personalArmor = newAr;
        }
        /// <summary>
        /// Установка наличных.
        /// </summary>
        /// <param name="cash">Новое количество монет.</param>
        public void NewCash(int cash)
        {
            _personalCash = new Cash(cash);
        }
        /// <summary>
        /// Увеличение/уменьшение количества монет.
        /// </summary>
        /// <param name="n">Изменение (+/-).</param>
        public void ChangeCash(int n)
        {
            if (n > 0)
                _personalCash.Add(n);
            else
                _personalCash.Get(-n);
        }

        ////////////////////////////////////////////////////////////////////////// 
        ////////////////////////////////////////////////////////////////////////// Getters
        ////////////////////////////////////////////////////////////////////////// 
        
        /// <summary>
        /// Получить копию оружия персонажа.
        /// </summary>
        /// <returns>Оружие этого человека.</returns>
        public Weapon GetWeapon()
        {
            return _personalWeapon;
        }
        /// <summary>
        /// Получить копию брони персонажа.
        /// </summary>
        /// <returns>Броня персонажа.</returns>
        public Armor GetArmor()
        {
            return _personalArmor;
        }
        /// <summary>
        /// Узнать сколько монет у персонажа.
        /// </summary>
        /// <returns>Количество монет на данный момент.</returns>
        public int HowMoneyInCash()
        {
            return _personalCash.Quantity();
        }
        /// <summary>
        /// Получить урон от оружия персонажа.
        /// </summary>
        /// <returns></returns>
        public int GetDamage()
        {
            return _personalWeapon.Damage;
        }
        /// <summary>
        /// Получить стойкость брони.
        /// </summary>
        /// <returns></returns>
        public int GetStandUp()
        {
            return _personalArmor.StandUp;
        }
        /// <summary>
        /// Ухудшить состояние оружия ввиду его использования.
        /// </summary>
        public void WeaponUsed()
        {
            _personalWeapon.Estatement--;
        }
        /// <summary>
        /// Ухудшить состояние брони ввиду ее повреждения от ударов.
        /// </summary>
        public void ArmorUsed()
        {
            _personalArmor.Estatement--;
        }
        //////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////// Compares
        //////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// Сравнения своего оружия и оружия человека.
        /// </summary>
        /// <param name="weaponToCompare">Сравниваемое оружие.</param>
        /// <returns></returns>
        public bool EquipCompare(Weapon weaponToCompare)
        {
            return _personalWeapon.Compare(weaponToCompare);
        }

        /// <summary>
        /// Сравниваем свою броню и найденную.
        /// </summary>
        /// <param name="armorToCompare">Броня для сравнения.</param>
        /// <returns></returns>
        public bool EquipCompare(Armor armorToCompare)
        {
            return _personalArmor.Compare(armorToCompare);
        }
    }
}
