namespace Helpers
{
    public static class Extensions
    {
        // I really should have just made a deck class but I'm too deep now 
        // so I'm just making extensions to the List<T> class ¯\_(ツ)_/¯
        public static void Shuffle<T>(this List<T> list)
        {
            Random rng = new Random();

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void Deal<T>(this List<T> list, List<T> target)
        {

            target.Add(list[0]);
            list.Remove(list[0]);
        }

        public static String List<T>(this List<T> list)
        {
            String returnString = "";
            foreach(T item in list)
            {
                if(item != null)
                {
                    returnString += $"{item.ToString()}\n";
                }
            }
            return returnString;
        }
    }
}