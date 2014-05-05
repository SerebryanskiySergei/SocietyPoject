using System.Drawing;
using World.Characters.Strategies;
using World.Enviroment;

namespace World.Characters.Professions
{
    class PeasantDecorator: IProfessionDecorator
    {
        public void TakeProfession(Person person, Habitat settlement)
        {
            person.SetEqip(Equipment.Standart.Weapons.Hands, Equipment.Standart.Armors.NoneArmor, 50);
            person.SetLocation(new Point(RandomContainer.Random.Next(settlement.Width),RandomContainer.Random.Next(settlement.Height)));
            person.Rewiew = 100;
            person.Speed = 70;
            person.Strategy = new PeasantStrategy();
            person.Profession = Profession.Peasant;
        }

    }
}
