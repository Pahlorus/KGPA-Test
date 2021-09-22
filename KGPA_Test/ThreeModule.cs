using KGPACandidateTest.Data;
using System;
using System.Collections.Generic;

namespace KGPA_Test
{
    public class ThreeModule
    {
        private Dictionary<int, Node> _nodes;
        private List<int> _rootIds;
        private Analizer _analizer;
        private Drawer _drawer;

        public ThreeModule()
        {
            _nodes = new Dictionary<int, Node>();
            _rootIds = new List<int>();
            _drawer = new Drawer();
            _analizer = new Analizer(_nodes, _rootIds);
        }

        public void Prepare(Items sourceItems)
        {
            _analizer.AnalizeSource(sourceItems);
            Console.Write("Введите число соответствующее типу сортировки нод на каждом уровне иерархии: " + "\n" +
                " 1-сортировка по имени в алфавитном порядке, " + "\n" +
                " 2-сортировка по имени в обратном алфавитном порядке, " + "\n" +
                " 3-сортировка по дате старта по возрастанию, " + "\n" +
                " 4-сортировка по дате старта по убыванию: " + "\n" +
                " 5-сортировка по дате завершения по возрастанию, " + "\n" +
                " 6-сортировка по дате завершения по убыванию"+ "\n"+
                ": ");

            var isParsed = int.TryParse(Console.ReadLine(), out var result);
            if(isParsed) NodesSort(result);
            else
            {
                Console.WriteLine("Введен не верный символ/символы, сортировка выполнена по умолчанию - по имени");
                Console.WriteLine();
                NodesSort(1);
            }
        }

        public void GetMedian()
        {
            var propertyValues = new List<byte>();
            foreach (var kvp in _nodes)
            {
                propertyValues.Add(kvp.Value.Completed);
            }

            propertyValues.Sort();

            if (propertyValues.Count % 2 == 0)
            {
                var lowMedianIndex = (int)MathF.Floor((propertyValues.Count - 1) / 2);
                var haighMedianIndex = (int)MathF.Ceiling((propertyValues.Count - 1) / 2);
                Console.WriteLine($"Четное количество элементов в выборке, нижняя медиана: {propertyValues[lowMedianIndex]}, верхняя медиана: {propertyValues[haighMedianIndex]}");
                Console.WriteLine();
            }
            else
            {
                var medianIndex = (propertyValues.Count - 1) / 2;
                Console.WriteLine($"Нечетное количество элементов в выборке, медиана: {propertyValues[medianIndex]}");
                Console.WriteLine();
            }
        }

        public void NodesSort(int sortType)
        {
            foreach (var item in _nodes) item.Value.Sort(sortType);
        }

        public void ThreesDraw()
        {
            ShowInfo();
            for (int i = 0; i < _rootIds.Count; i++)
            {
                _drawer.DrawNodes(_nodes[_rootIds[i]]);
                _drawer.DrawSpace();
            }
        }

        private void ShowInfo()
        {
            Console.WriteLine();
            Console.WriteLine($"Найдено древовидных структур: {_rootIds.Count}, количество нод: {_nodes.Count}");
            Console.WriteLine();
        }
    }
}
