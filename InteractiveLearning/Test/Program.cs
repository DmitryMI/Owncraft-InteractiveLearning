using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntLearnShared.Core;
using IntLearnShared.Networking;
using IntLearnShared.Utils;

namespace Test
{
    class Program
    {

        static void Main(string[] args)
        {
            Category rootA = PrebuiltTaskCreator.GetDebugTree();

            Category rootB = PrebuiltTaskCreator.GetPrebuiltTasksAux();

            Category merged = Category.MergeTrees(rootA, rootB);

            Debug.WriteLine("Finish!");
        }



    }
}
