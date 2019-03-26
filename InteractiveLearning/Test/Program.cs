using System;
using System.Collections.Generic;
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
            Category root = PrebuiltTaskCreator.GetDebugTree();

            string result = Serializer.Serialize(root);


            Category restored = Serializer.Deserialize(result);
        }


        
    }
}
