using World.Enviroment;

namespace World.Characters.Strategies
{
    public interface IStrategy
    {
        Decision TakeDecision(IPersonToStrategy person,PersonalEnviroment personEnvir, Habitat settlement);
    }
}
