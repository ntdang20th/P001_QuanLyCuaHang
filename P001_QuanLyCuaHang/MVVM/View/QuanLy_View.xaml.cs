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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace P001_QuanLyCuaHang.MVVM.View
{
    /// <summary>
    /// Interaction logic for QuanLy_View.xaml
    /// </summary>
    public partial class QuanLy_View : UserControl
    {
        public QuanLy_View()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = int.Parse(((Button)e.Source).Uid);

            switch (index)
            {
                case 0:
                    {
                        GridSmallMain.Children.Clear();
                        GridSmallMain.Children.Add(new MVVM.View.HangHoa_View());
                        break;
                    }
                case 1:
                    {
                        GridSmallMain.Children.Clear();
                        GridSmallMain.Children.Add(new MVVM.View.HoaDonNhapView());
                        break;
                    }
                case 2:
                    {
                        
                        break;
                    }
                case 3:
                    {
                        
                        break;
                    }
                case 4:
                    {
                        break;
                    }
            }
        }

    }
}
