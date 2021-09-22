using System;
using System.Collections.Generic;
using System.Text;

namespace KGPA_Test
{
    public class Drawer
    {
        const string graphcross = "├── ";
        const string graphCorner = "└── ";
        const string graphVerticalLine = "│   ";
        const string graphSpace = "    ";

        public void DrawNodes(Node branchNode, string prevGraph = "  ", int recurcionCycles = 0)
        {
            if (recurcionCycles > Settings.maxRecurcionCycles)
            {
                Console.WriteLine($"[DRAWER]: Превышен лимт вложенных рекурсий");
                return;
            }

            if (branchNode.IsRoot) DrawNodeData(branchNode);

            var children = branchNode.Children;
            for (int i = 0; i < children.Count; i++)
            {
                var node = children[i];

                if (i == children.Count - 1)
                {
                    DrawForkElement(prevGraph, graphCorner);
                    DrawNodeData(node);
                    if (node.IsHaveChildren) DrawNodes(node, prevGraph + graphSpace, recurcionCycles + 1);
                }
                else
                {
                    DrawForkElement(prevGraph, graphcross);
                    DrawNodeData(node);
                    if (node.IsHaveChildren) DrawNodes(node, prevGraph + graphVerticalLine, recurcionCycles + 1);
                }
            }
        }

        public void DrawSpace()
        {
            Console.WriteLine();
        }

        private void DrawNodeData(Node node)
        {
            if (node.IsRoot) Console.Write("  ");
            ConsoleColor color = node.IsRoot ? Settings.rootColor :
                node.IsHaveChildren ? Settings.branchesColor : Settings.endNodeColor;
            Console.ForegroundColor = color;
            var parentID = node.ParentID == 0 ? "Root" : node.ParentID.ToString();

            Console.Write($"Name:{node.Name}, ID: {node.ID},  ParentID:{parentID},  Start:{node.Start:d}, End: {node.End:d}");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        private void DrawParameter(string parameter)
        {
            var cursorLeft = Console.CursorLeft;
            Console.WriteLine(parameter);
            var cursorTop = Console.CursorTop;
            Console.SetCursorPosition(cursorLeft, cursorTop);
        }

        private void DrawForkElement(string prevGraph, string element)
        {
            Console.WriteLine(prevGraph + graphVerticalLine);
            Console.WriteLine(prevGraph + graphVerticalLine);
            Console.Write(prevGraph + element);
        }
    }
}
