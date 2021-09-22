using KGPACandidateTest.Data;
using System;
using System.Collections.Generic;

namespace KGPA_Test
{
    public class Node
    {
        private Item _item;
        private List<Node> _children;

        #region Property
        public bool IsRoot => !_item.ParentId.HasValue;
        public bool IsHaveChildren => _children.Count > 0;
        public int ID => _item.Id;
        public int ParentID
        {
            get
            {
                if (_item.ParentId.HasValue) return _item.ParentId.Value;
                else return 0;

            }
        }
        public byte Completed => _item.Completed;
        public string Name => _item.Name;
       
        public DateTime Start => _item.Start;
        public DateTime End => _item.End;
        public List<Node> Children => _children;
        #endregion

        public Node(Item sourceItem, bool isRoot = false)
        {
            _children = new List<Node>();
            _item = sourceItem;
        }

        public void AddChildNode(Node node)
        {
            _children.Add(node);
        }

        public void Sort(int sortType)
        {
            if (_children.Count == 0) return;
            switch (sortType)
            {
                default:
                case 1: { _children.Sort((node1, node2) => node1.Name.CompareTo(node2.Name)); break; }
                case 2: { _children.Sort((node1, node2) => node2.Name.CompareTo(node1.Name)); break; }
                case 3: { _children.Sort((node1, node2) => node1.Start.CompareTo(node2.Start)); break; }
                case 4: { _children.Sort((node1, node2) => node2.Start.CompareTo(node1.Start)); break; }
                case 5: { _children.Sort((node1, node2) => node1.End.CompareTo(node2.End)); break; }
                case 6: { _children.Sort((node1, node2) => node2.End.CompareTo(node1.End)); break; }
            }

            int Compare(Node node1, Node node2)
            {
                return node1.Name.CompareTo(node2.Name);
            }
        }
    }
}
