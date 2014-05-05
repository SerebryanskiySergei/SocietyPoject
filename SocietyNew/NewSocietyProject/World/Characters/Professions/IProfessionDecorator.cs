using World.Enviroment;

namespace World.Characters.Professions
{
    public interface IProfessionDecorator
    {
        void TakeProfession(Person person, Habitat settlement);
    }

}
