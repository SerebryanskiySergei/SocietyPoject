using System;
using System.Collections.Generic;
using World.Characters;

namespace World.Enviroment
{
    public class PersonalEnviroment
    {
        /// <summary>
        /// The nearest characters
        /// </summary>
        public List<Person> NearestCharacters;

        /// <summary>
        /// Создание окружение персонажа на основе его дальности обзора.
        /// </summary>
        /// <param name="person">Персонаж.</param>
        /// <param name="world">Королевство.</param>
        public PersonalEnviroment(Person person, Kingdom world)
        {
            NearestCharacters = new List<Person>();
            foreach (var place in world.GetHabitat().GetDictionaryOfLocations().Keys)
            {
                if (
                    Math.Sqrt(Math.Pow(place.X - person.GetLocation().X, 2) +
                              Math.Pow(place.Y - person.GetLocation().Y, 2)) <= person.GetRewiew())
                    NearestCharacters.Add(
                        world.GetDictionaryOfCharacters()[world.GetHabitat().GetDictionaryOfLocations()[place]]);
            }
        }
    }
}
