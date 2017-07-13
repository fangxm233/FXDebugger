using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Reflection;

class Debug
{
    public static Debug Debugger;

    Process process = new Process();
    StreamWriter StandardInput;
    bool isPause = false;

    /// <summary>
    /// Run it before you use the dubugger.
    /// </summary>
    public void Initialization()
    {
        Debugger = this;
        //process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardInput = true;
        //****************下面就是我打不开的原因 --Asixa
        process.StartInfo.FileName = "C:/Users/fangxm/Documents/visual studio 2017/Projects/FXDebugger/FXDebugger/bin/Debug/FXDebugger.exe";
        process.StartInfo.UseShellExecute = false;

        process.Start();
        process.OutputDataReceived += OutputDataReceived;
        process.ErrorDataReceived += ErrorDataReceived;
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        StandardInput = process.StandardInput;
    }

    public void Pause()
    {
        StandardInput.WriteLine("pause");
        isPause = true;
        while (isPause){}
    }

    public void Close()
    {
        process.WaitForExit(100);
        process.Kill();
        StandardInput.Close();
        isPause = false;
    }


    public void Log(object t)
    {
        StandardInput.WriteLine(t);
    }

    void OutputDataReceived(object sender, DataReceivedEventArgs e)
    {
        if(e.Data == "continue")
        {
            isPause = false;
        }
        else
        {
            Console.WriteLine(e.Data);
        }
    }

    void ErrorDataReceived(object sender, DataReceivedEventArgs e)
    {
        Console.WriteLine(e.Data);
    }

    public void GetImformation()
    {
        var method = new StackFrame(1).GetMethod();
        var property = (from p in method.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        where p.GetGetMethod(true) == method || p.GetSetMethod(true) == method
                        select p).FirstOrDefault();
    }
}

