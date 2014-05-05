using System.Drawing;
using World.Characters.Strategies;
using World.Enviroment;

namespace World.Characters
{
    public interface IPersonToStrategy
    {
        Person GetOpponent();
        void SetOpponent(Person op);
        int GetHp();
        int GetLvlBonus();
        int GetRewiew();
        Equipment GetEquipment();
        State GetStatus();
        Decision GetLastDecision();
        int GetSpeed();
        Point GetLocation();
        int GetId();
        IStrategy GetStrategy();
        Profession GetProfession();
        void SetDecision(Decision newDecision);
        void ChangeStrategy(IStrategy newstrategy);
    }

    /// <summary>
    /// Идентификаторы состояний.
    /// </summary>
    public enum State
    {
        /// <summary>
        /// Готов к следующим действиям.
        /// </summary>
        Ready,

        /// <summary>
        /// Идет.
        /// </summary>
        Moving,

        /// <summary>
        /// Погиб.
        /// </summary>
        Died,

        /// <summary>
        /// Работает.
        /// </summary>
        Working,

        /// <summary>
        /// Сражается.
        /// </summary>
        Fighting,

        /// <summary>
        /// Последовательность действий завершена.
        /// </summary>
        LastActionCompleted,

        /// <summary>
        /// Бежит.
        /// </summary>
        Run
    }

    /// <summary>
    /// Идентификаторы действий.
    /// </summary>
    public enum ActionType
    {
        /// <summary>
        /// Патрульирование.
        /// </summary>
        Patroling,

        /// <summary>
        /// Атака.
        /// </summary>
        Attack,

        /// <summary>
        /// Идти на ферму.
        /// </summary>
        MoveToFarm,

        /// <summary>
        /// Идти к торговцу.
        /// </summary>
        MoveToTrader,

        /// <summary>
        /// Продвавать.
        /// </summary>
        Sell,

        /// <summary>
        /// Идти в кузницу.
        /// </summary>
        MoveToCrafthouse,

        /// <summary>
        /// Купить необходимые инструменты.
        /// </summary>
        BuyInstruments,

        /// <summary>
        /// Купить лекартсва.
        /// </summary>
        BuyMedicine,

        /// <summary>
        /// Обокрасть.
        /// </summary>
        StealMoney,

        /// <summary>
        /// Ищет торговца.
        /// </summary>
        SearchTrader,

        /// <summary>
        /// Отдых.
        /// </summary>
        Free,

        /// <summary>
        /// Торговать.
        /// </summary>
        Trade,

        /// <summary>
        /// Ковать.
        /// </summary>
        Craft,

        /// <summary>
        /// Работать.
        /// </summary>
        Work
    }
}

