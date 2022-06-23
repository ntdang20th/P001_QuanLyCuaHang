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
    public class NhaCungCapViewModel:BaseViewModel
    {
        #region List
        public ObservableCollection<NhaCungCap> _ListNhaCungCap;
        public ObservableCollection<NhaCungCap> ListNhaCungCap { get => _ListNhaCungCap; set { _ListNhaCungCap = value; OnPropertyChanged(); } }
        #endregion

        #region Properties
        public string _Ten = "";
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        public string _DiaChi = "";
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        public string _Sdt = "";
        public string Sdt { get => _Sdt; set { _Sdt = value; OnPropertyChanged(); } }

        public string _Email = "";
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        #endregion

        #region Selected Item
        public NhaCungCap _SelectedNhaCungCap;
        public NhaCungCap SelectedNhaCungCap
        {
            get => _SelectedNhaCungCap; set
            {
                _SelectedNhaCungCap = value; OnPropertyChanged();
                if (SelectedNhaCungCap != null)
                {
                    Ten = SelectedNhaCungCap.Ten;
                    Email = SelectedNhaCungCap.Email;
                    DiaChi = SelectedNhaCungCap.DiaChi;
                    Sdt = SelectedNhaCungCap.Sdt;
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


        public NhaCungCapViewModel()
        {
            ListNhaCungCap = new ObservableCollection<NhaCungCap>(DataProvider.Instance.DB.NhaCungCaps.Where(x=>x.An == 0));

            Them_Command = new RelayCommand<object>((p) =>
            {
                if (String.IsNullOrEmpty(Ten) || String.IsNullOrEmpty(Sdt) || String.IsNullOrEmpty(DiaChi))
                    return false;
                return true;
            }, (p) =>
            {
                var ncc = new NhaCungCap() { Ten = Ten, DiaChi = DiaChi, Sdt = Sdt, Email = Email, An = 0 };

                DataProvider.Instance.DB.NhaCungCaps.Add(ncc);
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã thêm", "Thông báo");
                ListNhaCungCap.Add(ncc);
            });

            Sua_Command = new RelayCommand<object>((p) =>
            {
                if (String.IsNullOrEmpty(Ten) || String.IsNullOrEmpty(Sdt) || String.IsNullOrEmpty(DiaChi))
                    return false;
                return true;
            }, (p) =>
            {
                var ncc = DataProvider.Instance.DB.NhaCungCaps.Where(t => t.Id == SelectedNhaCungCap.Id).SingleOrDefault();
                ncc.Ten = Ten;
                ncc.DiaChi = DiaChi;
                ncc.Sdt = Sdt;
                ncc.Email = Email;
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã sửa", "Thông báo");
                //SelectedItem.TenNhaCungCap = DisplayName;
            });

            Xoa_Command = new RelayCommand<object>((p) =>
            {
                if (SelectedNhaCungCap == null)
                    return false;
                return true;
            }, (p) =>
            {
                var ncc = DataProvider.Instance.DB.NhaCungCaps.Where(t => t.Id == SelectedNhaCungCap.Id).SingleOrDefault();
                ncc.An = 1;
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã xóa", "Thông báo");
                ListNhaCungCap = new ObservableCollection<NhaCungCap>(DataProvider.Instance.DB.NhaCungCaps.Where(x => x.An == 0));
            });

            EnterCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                ListNhaCungCap = new ObservableCollection<NhaCungCap>(DataProvider.Instance.DB.NhaCungCaps.Where(t => t.Ten.Contains(p.Text)).ToList());
            });
        }
    }
}
