using System;

namespace World
{
    public class RandomContainer
    {
        public static Random Random = new Random(DateTime.Now.Millisecond);
        public static Random UsualRandom = new Random();
    }
}
