namespace SignalLines.Common
{
    public static class Extensions
    {
         public static bool IsOdd(this int i)
         {
             return i % 2 == 1;
         }

         public static bool IsEven(this int i)
         {
             return !IsOdd(i);
         }
    }
}