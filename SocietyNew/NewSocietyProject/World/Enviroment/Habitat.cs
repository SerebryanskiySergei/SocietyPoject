using System.Collections.Generic;
using System.Drawing;
using World.Characters;

namespace World.Enviroment
{
   /// <summary>
    /// Окружение.
    /// </summary>
    public class Habitat
    {
        public Rectangle Forest;
        public Rectangle Farm;
        public Rectangle CrHouse;
        public Rectangle Castle;
        public TraderPlace[] MarketPlace = new TraderPlace[10];
        public Dictionary<int, Profession> SettlersProfessions { get; set; }
        Dictionary<Point,int> _locationsOfSettlers;
        public int Width;
        public int Height;

        public Habitat(Dictionary<int,Person> settlers, int widht,int height )
        {
            Width = widht;
            Height = height;
            _locationsOfSettlers = new Dictionary<Point,int>();
            SettlersProfessions = new Dictionary<int,Profession>();
            Forest = new Rectangle(0, 0, 170, 170);             //
            Farm = new Rectangle(200, 30, 100, 100);            //      Убрать константы.
            CrHouse = new Rectangle(300, 500, 100, 100);        //
            Castle = new Rectangle(Width-324,0,324,285);        //
            for (int i = 0; i < 10; i++)
                MarketPlace[i] = new TraderPlace(new Rectangle(Castle.X+i*30,Castle.Y+Castle.Size.Height+10,30,30));  
            
        }

       /// <summary>
        /// Обновить местоположение персонажей.
        /// </summary>
        /// <param name="settlers">Словарь ID - IPerson.</param>
        public void RefreshLocation(Dictionary<int,Person> settlers)
        {
            _locationsOfSettlers.Clear();
            foreach (Person p in settlers.Values)
            {
                if (_locationsOfSettlers.ContainsKey(p.GetLocation()))
                {
                    _locationsOfSettlers.Remove(p.GetLocation()); //лист
                }
                _locationsOfSettlers.Add(p.GetLocation(), p.GetId());
            }

        }
        /// <summary>
        /// Получить словарь с координатами персонажей.
        /// </summary>
        /// <returns>Словарь локация - ID персонажа.</returns>
        public Dictionary<Point, int> GetDictionaryOfLocations()
        {
            return _locationsOfSettlers;
        }
        /// <summary>
        /// Найти незанятого торговца.
        /// </summary>
        /// <returns>Локация торговца.</returns>
        public Point FindFullTraderPlace()
        {
            TraderPlace p = MarketPlace[RandomContainer.Random.Next(0,8)];
            while (p.isEmpty)
                p = MarketPlace[RandomContainer.Random.Next(0, 8)];
            Point dest = new Point(p.place.X+p.place.Size.Width/2,p.place.Y+p.place.Size.Height);
            return dest;
        }
        /// <summary>
        /// Найти пустую торговую точку.
        /// </summary>
        /// <returns>Торговая точка.</returns>
        public TraderPlace FindEmptyTraderPlace()
        {
            foreach (TraderPlace p in MarketPlace)
            {
                if (p.isEmpty)
                {
                    return p;
                }
            }
            TraderPlace tp = new TraderPlace(new Rectangle(
                new Point(RandomContainer.Random.Next(Castle.Location.X, Castle.Location.X + Castle.Width),
                    RandomContainer.Random.Next(Castle.Y, Castle.Y + Castle.Height)), new Size(30, 30)));
            return tp;
        }
    }
    /// <summary>
    /// Торгвые точки.
    /// </summary>
    public class TraderPlace
    {
        public Rectangle place;
        public bool isEmpty;

        public TraderPlace(Rectangle place)
        {
            this.place = place;
            isEmpty = true;
        }
    }
}

