using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TreeSharp
{
    public class Tree
    {
        private enum Direction
        {
            NOWHERE,
            LEFT,
            RIGHT
        };

        public Node Root { get; set; }

        public Tree(Node Root = null)
        {
            this.Root = Root;
        }

        public static Tree CreateByBracketForm(String BracketForm)
        {
            Console.Clear();

            Node Root = new Node();
            Tree CurrentTree = new Tree(Root);
            Node CurrentNode = CurrentTree.Root;

            CreateByBracketsRecursive(CurrentTree.Root, BracketForm);
            return CurrentTree;
        }

        private static void CreateByBracketsRecursive(Node SubtreeRoot, String BracketsForm)
        {
            Direction ProcessingNode = Direction.LEFT;
            Node CurrentNode = SubtreeRoot;
            Int32 BracketsCount = 0, IndexStart = 0, IndexEnd = 0;
            Char CurrentSymbol;
            Boolean BracketsAreEmpty = true;

            for (Int32 i = 0; i < BracketsForm.Length; i++)
            {
                CurrentSymbol = BracketsForm[i];

                
                switch(CurrentSymbol)
                {
                    case '(':
                        {
                            if (BracketsCount == 1)
                                IndexStart = i;

                            if(BracketsCount == 1) BracketsAreEmpty = true;
                            BracketsCount++;
                            break;
                        }
                    case ')':
                        {
                            BracketsCount--;
                            if (BracketsCount == 1)
                            {
                                IndexEnd = i;
                                if (!BracketsAreEmpty)
                                {
                                    Node NewNode = new Node();
                                    switch (ProcessingNode)
                                    {
                                        case Direction.LEFT:
                                            {
                                                CurrentNode.Left = NewNode;
                                                CreateByBracketsRecursive(CurrentNode.Left, BracketsForm.Substring(IndexStart, IndexEnd - IndexStart));
                                                break;
                                            }
                                        case Direction.RIGHT:
                                            {
                                                CurrentNode.Right = NewNode;
                                                CreateByBracketsRecursive(CurrentNode.Right, BracketsForm.Substring(IndexStart, IndexEnd - IndexStart));
                                                break;
                                            }
                                        case Direction.NOWHERE: throw new OutOfTreeException();
                                    }
                                }
                                ProcessingNode = ProcessingNode == Direction.LEFT ? Direction.RIGHT : Direction.NOWHERE;
                            }

                            break;
                        }
                    default:
                        {
                            if (Char.IsLetterOrDigit(CurrentSymbol) && BracketsCount == 1)
                            {
                                CurrentNode.Key = CurrentSymbol;
                            }

                            if(BracketsCount == 2)
                                BracketsAreEmpty = false;
                            break;
                        }
                }
            }
        }

        public void DrawTree()
        {
            DrawTreeRecursive(Root, Console.WindowWidth / 2, 0, Console.WindowWidth);
        }

        private static void DrawTreeRecursive(Node ParentNode, Int32 StartPointX, Int32 ParentLevel, Int32 AvailableArea)
        {
            Console.SetCursorPosition(StartPointX, ParentLevel);

            Console.ForegroundColor = ConsoleColor.White;
            if (ParentNode.Key != '*')
                Console.Write(ParentNode.Key.ToString());


            Console.ForegroundColor = ConsoleColor.DarkGreen;
            if (ParentNode.Left != null)
                if (ParentNode.Left.Key != '*')
                {
                    for (Int32 i = AvailableArea / 4; i > 0; i--) { Console.SetCursorPosition(StartPointX - i, ParentLevel + 1); Console.Write('_'); }
                    Console.SetCursorPosition(StartPointX - AvailableArea / 4, ParentLevel + 2);
                    Console.Write('|');
                    DrawTreeRecursive(ParentNode.Left, StartPointX - AvailableArea / 4, ParentLevel + 3, AvailableArea / 2);
                }

            if (ParentNode.Right != null)
                if (ParentNode.Right.Key != '*')
                {
                    for (Int32 i = 0; i < AvailableArea / 4; i++) { Console.SetCursorPosition(StartPointX + i, ParentLevel + 1);  Console.Write('_'); }
                    Console.SetCursorPosition(StartPointX + AvailableArea / 4, ParentLevel + 2);
                    Console.Write('|');
                    DrawTreeRecursive(ParentNode.Right, StartPointX + AvailableArea / 4, ParentLevel + 3, AvailableArea / 2);
                }
        }

        public void PreorderedTraversing()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Прямой обход: ");
            Console.ForegroundColor = ConsoleColor.White;
            PreorderedTraversingRecursive(Root);
        }

        private static void PreorderedTraversingRecursive(Node RootNode)
        {
            Console.Write(RootNode.Key);

            if (RootNode.Left != null)
                PreorderedTraversingRecursive(RootNode.Left);

            if (RootNode.Right != null)
                PreorderedTraversingRecursive(RootNode.Right);
        }

        public void InorderedTraversing()
        {
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("Инверсный обход: ");
            Console.ForegroundColor = ConsoleColor.White;
            InorderedTraversingRecursive(Root);
        }

        private void InorderedTraversingRecursive(Node RootNode)
        {
            if (RootNode.Left != null)
                InorderedTraversingRecursive(RootNode.Left);

            if (RootNode.Right != null)
                InorderedTraversingRecursive(RootNode.Right);
            Console.Write(RootNode.Key);
        }
    }

    public class OutOfTreeException : OutOfMemoryException { }
}