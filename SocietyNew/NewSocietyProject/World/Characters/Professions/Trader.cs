using System.Drawing;
using World.Characters.Strategies;
using World.Enviroment;

namespace World.Characters.Professions
{
    class TraderDecorator:IProfessionDecorator
    {
        public void TakeProfession(Person person,Habitat settlement)
        {
            person.SetEqip(Equipment.Standart.Weapons.Hands, Equipment.Standart.Armors.NoneArmor, 100);
            TraderPlace freeTraderPlace = settlement.FindEmptyTraderPlace();
            person.SetLocation(new Point(freeTraderPlace.place.X + freeTraderPlace.place.Width / 2,freeTraderPlace.place.Y + freeTraderPlace.place.Height / 2));
            freeTraderPlace.isEmpty = false;
            person.ChangeStrategy(new TraderStrategy());
            person.Rewiew = 100;
            person.Speed = 70;
            person.Profession= Profession.Trader;
        }
    }
}
