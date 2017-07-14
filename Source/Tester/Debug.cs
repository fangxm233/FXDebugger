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
        process.StartInfo.FileName = "G:/GIT/FXDebugger/Source/FXDebugger/bin/Debug/FXDebugger.exe";
        process.StartInfo.UseShellExecute = false;
        //2333333
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
    /*
    public static T GetPrivateProperty<T>(this object instance, string propertyname)
    {
        BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic;
        Type type = instance.GetType();
        PropertyInfo field = type.GetProperty(propertyname, flag);
        return (T)field.GetValue(instance, null);
    }*/

    public void GetImformation()
    {
        var method = new StackFrame(1).GetMethod();
        var property = (from p in method.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)
                        where p.GetGetMethod(true) == method || p.GetSetMethod(true) == method
                        select p).FirstOrDefault();
        //Console.WriteLine(method.DeclaringType.GetProperties()[0]);
        BindingFlags flag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
        foreach (FieldInfo item in method.DeclaringType.GetFields(flag))
        {
            Assembly assembly = Assembly.GetExecutingAssembly(); // 获取当前程序集 
            object obj = assembly.CreateInstance(method.DeclaringType.Namespace + "." + method.DeclaringType.Name);
            Log(item.Name + "的值为" + item.GetValue(obj).ToString());


            #region 奇怪的注释
            //Console.WriteLine(method.DeclaringType.Namespace);
            //Type type = Type.GetType(method.DeclaringType.Name);
            //object obj = type.Assembly.CreateInstance(type.Name);
            //Console.WriteLine(obj.GetType().Name);
            /*
            object o = method.DeclaringType.ReflectedType;
            item.GetValue(o);
            Console.WriteLine(item.Name);
            */
            //Console.WriteLine("666666");
            //object o = new object();
            //Console.WriteLine(item.GetValue(obj).ToString());
            #endregion
        }
    }
}

