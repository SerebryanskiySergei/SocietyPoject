using System.Drawing;
using World.Characters.Strategies;
using World.Enviroment;

namespace World.Characters.Professions
{
    class RobberDecorator:IProfessionDecorator
    {
        public void TakeProfession(Person person, Habitat settlement)
        {
            person.SetEqip(Equipment.Standart.Weapons.Dagger, Equipment.Standart.Armors.UsualArmor, 30);
            person.SetLocation(new Point(RandomContainer.Random.Next(0, settlement.Width / 2),RandomContainer.Random.Next(0, settlement.Height / 2)));
            person.Rewiew = 110;
            person.Speed = 95;
            person.Strategy = new RobberStrategy();
            person.Profession = Profession.Robber;
        }

    }
}
