using System;
using System.Threading;
using TreeSharp;

namespace TreeSharpTest
{
    class Program
    {
        public static String BracketForm;

        static void Main()
        {
            Console.WriteLine("Enter tree:");
            BracketForm = Console.ReadLine();

            try
            {
                Tree ResultTree = Tree.CreateByBracketForm(BracketForm);
                Console.WriteLine("Press any key to draw the tree");
                Console.ReadKey(true);
                Console.Clear();
                ResultTree.DrawTree();
                ResultTree.PreorderedTraversing();
                ResultTree.InorderedTraversing();
            }

            catch(OutOfTreeException)
            {
                Console.WriteLine("We've got out of tree range exception, exit");
            }

            Console.ReadKey(true);
        }
    }
}
