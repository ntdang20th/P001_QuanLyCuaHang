using P001_QuanLyCuaHang.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace P001_QuanLyCuaHang.MVVM.ViewModel
{
    public class KhachHangViewModel:BaseViewModel
    {
        #region List
        public ObservableCollection<KhachHang> _ListKhachHang;
        public ObservableCollection<KhachHang> ListKhachHang { get => _ListKhachHang; set { _ListKhachHang = value; OnPropertyChanged(); } }
        #endregion

        #region Properties
        public string _HoTen = "";
        public string HoTen { get => _HoTen; set { _HoTen = value; OnPropertyChanged(); } }

        public string _GioiTinh = "";
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }

        public DateTime? _NamSinh = DateTime.Now;
        public DateTime? NamSinh { get => _NamSinh; set { _NamSinh = value; OnPropertyChanged(); } }

        public string _DiaChi = "";
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        public string _Sdt = "";
        public string Sdt { get => _Sdt; set { _Sdt = value; OnPropertyChanged(); } }

        public bool _CheckNam = true;
        public bool CheckNam { get => _CheckNam; set { _CheckNam = value; OnPropertyChanged(); } }

        public bool _CheckNu;
        public bool CheckNu { get => _CheckNu; set { _CheckNu = value; OnPropertyChanged(); } }

        #endregion

        #region Selected Item
        public KhachHang _SelectedKhachHang;
        public KhachHang SelectedKhachHang
        {
            get => _SelectedKhachHang; set
            {
                _SelectedKhachHang = value; OnPropertyChanged();
                if (SelectedKhachHang != null)
                {
                    HoTen = SelectedKhachHang.HoTen;
                    
                    GioiTinh = SelectedKhachHang.GioiTinh;
                    CheckNam = GioiTinh == "Nam" ? true : false;
                    CheckNu = GioiTinh == "Nữ" ? true : false;

                    NamSinh = SelectedKhachHang.NamSinh;

                    DiaChi = SelectedKhachHang.DiaChi;
                    Sdt = SelectedKhachHang.Sdt; 
                }
            }
        }
        #endregion

        #region Command
        public ICommand Them_Command { get; set; }
        public ICommand Sua_Command { get; set; }
        public ICommand Xoa_Command { get; set; }
        public ICommand EnterCommand { get; set; }
        #endregion

        public KhachHangViewModel()
        {
            ListKhachHang = new ObservableCollection<KhachHang>(DataProvider.Instance.DB.KhachHangs.Where(x => x.An == 0));

            Them_Command = new RelayCommand<object>((p) =>
            {
                if (String.IsNullOrEmpty(HoTen))
                    return false;

                var ds = DataProvider.Instance.DB.KhachHangs.Where(t => t.HoTen == HoTen);
                if (ds == null || ds.Count() != 0)
                    return false;

                return true;
            }, (p) =>
            {
                GioiTinh = CheckNam ? "Nam" : "Nữ";
                var kh = new KhachHang() { HoTen = HoTen, GioiTinh = GioiTinh, NamSinh = NamSinh, DiaChi = DiaChi, Sdt = Sdt, An = 0 };

                DataProvider.Instance.DB.KhachHangs.Add(kh);
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã thêm", "Thông báo");
                ListKhachHang.Add(kh);
            });

            Sua_Command = new RelayCommand<object>((p) =>
            {
                if (String.IsNullOrEmpty(HoTen) || SelectedKhachHang == null)
                    return false;

                return true;
            }, (p) =>
            {
                var khCu = DataProvider.Instance.DB.KhachHangs.Where(t => t.MaKh == SelectedKhachHang.MaKh).SingleOrDefault();
                khCu.HoTen = HoTen;
                khCu.GioiTinh = GioiTinh;
                khCu.NamSinh = NamSinh;
                khCu.DiaChi = DiaChi;
                khCu.Sdt = Sdt;
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã sửa", "Thông báo");
            });

            Xoa_Command = new RelayCommand<object>((p) =>
            {
                if (SelectedKhachHang == null)
                    return false;
                return true;
            }, (p) =>
            {
                var khCu = DataProvider.Instance.DB.KhachHangs.Where(t => t.MaKh == SelectedKhachHang.MaKh).SingleOrDefault();
                khCu.An = 1;
                DataProvider.Instance.DB.SaveChanges();
                ListKhachHang.Remove(khCu);
                MessageBox.Show("Đã xóa", "Thông báo");
            });

            EnterCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                ListKhachHang = new ObservableCollection<KhachHang>(DataProvider.Instance.DB.KhachHangs.Where(t => t.HoTen.Contains(p.Text)).ToList());
            });
        }
    }
}
