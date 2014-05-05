using System.Collections.Generic;
using World.Characters;
using World.Enviroment;

namespace World
{
    /// <summary>
    /// Государство . Весь мир собственно.
    /// </summary>
    public class Kingdom
    {
        /// <summary>
        /// Окружающая среда (поля, лес, замок, рынок...).
        /// </summary>
        readonly Habitat _settlement;
        /// <summary>
        /// Словарь персонажей(ключ = ID, значение = IPerson).
        /// </summary>
        private readonly Dictionary<int, Person> _settlers = new Dictionary<int, Person>();
        /// <summary>
        /// Создаем королество.
        /// </summary>
        /// <param name="countOfSettlers"></param>
        /// <param name="drawAreaWidht"></param>
        /// <param name="drawAreaHeight"></param>
        public Kingdom(int countOfSettlers, int drawAreaWidht, int drawAreaHeight)
        {
            
            _settlement = new Habitat(_settlers, drawAreaWidht, drawAreaHeight);
            PersonFactory factory = new PersonFactory(_settlement);
            for (int i = 0; i <= countOfSettlers; i++)
            {
                Person man = factory.GetMan();
                _settlers.Add(i, man);
            }
            _settlement.RefreshLocation(_settlers);
        }
        /// <summary>
        /// Получиьт ссылку на персонажа с заданным ID.
        /// </summary>
        /// <param name="id">Иднтификационный номер искомого персонажа.</param>
        /// <returns>Песронаж с ID равным заданному.</returns>
        public Person GetCharacter(int id)
        {
            return _settlers[id];
        }
        /// <summary>
        /// Получить словарь ID-IPerson всех персонажей мира.
        /// </summary>
        /// <returns>Словарь.</returns>
        public Dictionary<int, Person> GetDictionaryOfCharacters()
        {
            return _settlers;
        }
        /// <summary>
        /// Получить контейнер окружающей среды.
        /// </summary>
        /// <returns></returns>
        public Habitat GetHabitat()
        {
            return _settlement;
        }
    }
    
}
