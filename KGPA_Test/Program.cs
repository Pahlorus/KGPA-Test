using KGPACandidateTest.Data;
using System;
using System.Collections.Generic;

namespace KGPA_Test
{
    class Program
    {
        static void Main()
        {
            var threeModule = new ThreeModule();
            threeModule.Prepare(new Items());
            threeModule.GetMedian();
            threeModule.ThreesDraw();
            Console.ReadKey();
        }
    }

    public static class Settings
    {
        public static int maxRecurcionCycles = 10000;
        public static ConsoleColor rootColor = ConsoleColor.Red;
        public static ConsoleColor branchesColor = ConsoleColor.DarkCyan;
        public static ConsoleColor endNodeColor = ConsoleColor.DarkMagenta;
    }

    public static class Extensions
    {
        public static bool TryGetItemById(this Items items, int id, out Item item)
        {
            var sourceItems = items.GetItems();
            foreach (var sourceItem in sourceItems)
            {
                if (sourceItem.Id == id)
                {
                    item = sourceItem;
                    return true;
                }
            }
            item = default;
            return false;
        }
    }
}
