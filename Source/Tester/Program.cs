using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        public int i = 1;
        string s = "fdfdfdfdfdfdfd";
        static void Main()
        {
            new Debug().Initialization();
            Debug.Debugger.Pause();
            Debug.Debugger.Log("fangxm 6666666,Asixa 更666666");
            Debug.Debugger.Log("是是是");
            Debug.Debugger.GetImformation();
            Debug.Debugger.Pause();
            Debug.Debugger.Close();
        }
    }
}