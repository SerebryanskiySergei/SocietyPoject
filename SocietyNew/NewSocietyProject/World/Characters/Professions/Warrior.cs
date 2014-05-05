using World.Characters.Strategies;
using World.Enviroment;

namespace World.Characters.Professions
{
    class WarriorDecorator:IProfessionDecorator
    {
        public void TakeProfession(Person person, Habitat settlement)
        {
            person.SetEqip(Equipment.Standart.Weapons.Sword, Equipment.Standart.Armors.MediumArmor, 30);
            person.SetLocation(new System.Drawing.Point(RandomContainer.Random.Next(settlement.Width), RandomContainer.Random.Next(settlement.Height)));
            person.ChangeStrategy(new WarriorStrategy());
            person.Rewiew = 90;
            person.Speed = 50;
            person.Profession = Profession.Warrior;
        }
    }
}
