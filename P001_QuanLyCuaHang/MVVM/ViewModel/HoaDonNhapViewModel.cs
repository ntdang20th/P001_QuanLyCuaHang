using P001_QuanLyCuaHang.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P001_QuanLyCuaHang.MVVM.ViewModel
{
    public class HoaDonNhapViewModel:BaseViewModel
    {
        #region List
        public ObservableCollection<Hang> _ListHang;
        public ObservableCollection<Hang> ListHang { get => _ListHang; set { _ListHang = value; OnPropertyChanged(); } }
        #endregion

        #region Properties
        public string _Ten = "";
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        public int _GiaBan = 0;
        public int GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        public int _SoLuongTon = 0;
        public int SoLuongTon { get => _SoLuongTon; set { _SoLuongTon = value; OnPropertyChanged(); } }
        #endregion

        #region Selected Item
        public Hang _SelectedHang;
        public Hang SelectedHang
        {
            get => _SelectedHang; set
            {
                _SelectedHang = value; OnPropertyChanged();
                if (SelectedHang != null)
                {
                    Ten = SelectedHang.TenHang;
                    GiaBan = (int)SelectedHang.GiaBan;
                    SoLuongTon = (int)SelectedHang.SoLuongTon;
                }
            }
        }
        #endregion

        #region Command
        public ICommand ChiTietNCC_Command { get; set; }
        #endregion


        public HoaDonNhapViewModel()
        {
            ListHang = new ObservableCollection<Hang>(DataProvider.Instance.DB.Hangs);
        }
    }
}
