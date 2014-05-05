using World.Characters.Strategies;
using World.Enviroment;

namespace World.Characters.Professions
{
    class CraftsmanDecorator:IProfessionDecorator
    {
        public void TakeProfession(Person person, Habitat settlement)
        {
            person.Speed = 50;
            person.SetEqip(Equipment.Standart.Weapons.Hands, Equipment.Standart.Armors.UsualArmor, 75);
            person.SetLocation(new System.Drawing.Point(RandomContainer.Random.Next(settlement.Width), RandomContainer.Random.Next(settlement.Height)));
            person.Rewiew = 100;
            person.Strategy = new CraftsmanStrategy();
            person.Profession = Profession.Craftsman;
        }

    }
}
