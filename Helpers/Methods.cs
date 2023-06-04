namespace Helpers
{
    public static class Methods
    {
        //using this for all print statements to make text more natural and readable
        public static void Print(String s)
        {
            Console.WriteLine(s);
            System.Threading.Thread.Sleep(1000);
        }
    }
}