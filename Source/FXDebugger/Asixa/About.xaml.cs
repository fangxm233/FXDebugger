using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FXDebugger.Asixa
{
    public partial class About : Window
    {
        public About()
        {
            InitializeComponent();
        }

        private void Dev_Click(object sender, RoutedEventArgs e)
        {
            string url = "";
           switch( ((Button)sender).Content)
            {
                case "Asixa":url = "https://github.com/Asixa";break;
                case "Fangxm":url = "https://github.com/fangxm233";break;
                case "Sources":url = "https://github.com/fangxm233/FXDebugger";break;
            }
            try { System.Diagnostics.Process.Start(url); } catch { }
        }
    }

    
}
