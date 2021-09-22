using KGPACandidateTest.Data;
using System;
using System.Collections.Generic;

namespace KGPA_Test
{
    public class Analizer
    {
        Dictionary<int, Node> _nodes;
        List<int> _rootIds;

        public Analizer(Dictionary<int, Node> nodes, List<int> rootIds)
        {
            _nodes = nodes;
            _rootIds = rootIds;
        }

        public void AnalizeSource(Items sourceItems)
        {
            try
            {
                foreach (var item in sourceItems.GetItems())
                {
                    if (item.ParentId.HasValue) AnalizeSimpleItem(item, sourceItems);
                    else
                    {
                        var rootId = item.Id;
                        if (!_nodes.ContainsKey(rootId))
                        {
                            _nodes.Add(rootId, new Node(item));
                            _rootIds.Add(rootId);
                        }
                        else Console.WriteLine($"[ANALIZER]: Дубликат, родительский элемент с ID {rootId}");
                    }
                }
            }
            catch (Exception c) { Console.WriteLine(c.Message); }
        }

        private void AnalizeSimpleItem(Item item, Items sourceItems)
        {
            bool isHave;
            Node parentNode;
            var nodeId = item.Id;
            var parentNodeId = item.ParentId.Value;
            var newNode = new Node(item);

            isHave = _nodes.TryGetValue(parentNodeId, out parentNode);

            if (isHave) parentNode.AddChildNode(newNode);
            else
            {
                isHave = sourceItems.TryGetItemById(parentNodeId, out var parentSourceItem);
                if (isHave)
                {
                    parentNode = new Node(parentSourceItem);
                    parentNode.AddChildNode(newNode);
                }
                else Console.WriteLine($"[ANALIZER]: Не найден родительский элемент с ID {parentNodeId}");
            }
            if (!_nodes.ContainsKey(nodeId)) _nodes.Add(nodeId, newNode);
        }
    }
}
