using System.Linq;
using World;
using World.Characters;
using World.Enviroment;

namespace Engine
{
    /// <summary>
    /// Движок. 
    /// Занимается обработкой действий персонажей игры.
    /// </summary>
    public static class MainEngine
    {
        /// <summary>
        /// Опрос персонажей и вызов функции,которая обработает их действия.
        /// </summary>
        public static void ProcessLogic(Kingdom world)
        {
            foreach (Person man in world.GetDictionaryOfCharacters().Values)
            {
                if (man.GetStatus() != State.Died && man.GetHp()>0)
                {
                    Decision newdecision = man.GetStrategy()
                        .TakeDecision(man, new PersonalEnviroment(man, world), world.GetHabitat());
                    man.SetDecision(newdecision);
                    if (newdecision != null) newdecision.Apply(man);
                }
            }

            var toDelete =
                (from m in world.GetDictionaryOfCharacters().Values where m.GetStatus() == State.Died select m.GetId()).ToArray();

            foreach (var id in toDelete)
            {
                DeleteCharacter(world,id);
            }
            world.GetHabitat().RefreshLocation(world.GetDictionaryOfCharacters());
        }

        /// <summary>
        /// Удалить персонажа.
        /// </summary>
        /// <param name="world">Мир.</param>
        /// <param name="id">ID персонажа для удаления.</param>
        private static void DeleteCharacter(Kingdom world ,int id)
        {
            world.GetDictionaryOfCharacters().Remove(id);
        }
    }
}
