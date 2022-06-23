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
    public class HangViewModel:BaseViewModel
    {
        #region List
        public ObservableCollection<Hang> _ListHang;
        public ObservableCollection<Hang> ListHang { get => _ListHang; set { _ListHang = value; OnPropertyChanged(); } }

        public ObservableCollection<DonViTinh> _ListDonViTinh;
        public ObservableCollection<DonViTinh> ListDonViTinh { get => _ListDonViTinh; set { _ListDonViTinh = value; OnPropertyChanged(); } }
        #endregion

        #region Properties
        public string _TenHang = "";
        public string TenHang { get => _TenHang; set { _TenHang = value; OnPropertyChanged(); } }

        public int _GiaNhap = 0;
        public int GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); } }

        public int _GiaBan = 0;
        public int GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        public int _Slton = 0;
        public int Slton { get => _Slton; set { _Slton = value; OnPropertyChanged(); } }

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
                    TenHang = SelectedHang.TenHang;
                    GiaBan = (int)SelectedHang.GiaBan;
                    Slton = (int)SelectedHang.SoLuongTon;
                    SelectedDVT = DataProvider.Instance.DB.DonViTinhs.Where(x => x.Id == SelectedHang.IdDVT).SingleOrDefault();
                    try
                    {
                        GiaNhap = (int)DataProvider.Instance.DB.ChiTietHDNs.Where(x => x.IdHang == SelectedHang.Id).Select(t => t.DonGiaNhap).SingleOrDefault();
                    }
                    catch
                    {
                        GiaNhap = 0;
                    }
                   
                    
                }
            }
        }

        public DonViTinh _SelectedDVT;
        public DonViTinh SelectedDVT {get => _SelectedDVT; set{_SelectedDVT = value; OnPropertyChanged();}}  


        #endregion

        #region Command
        public ICommand Them_Command { get; set; }
        public ICommand Sua_Command { get; set; }
        public ICommand Xoa_Command { get; set; }
        public ICommand EnterCommand { get; set; }
        #endregion


        public HangViewModel()
        {
            ListHang = new ObservableCollection<Hang>(DataProvider.Instance.DB.Hangs);
            ListDonViTinh = new ObservableCollection<DonViTinh>(DataProvider.Instance.DB.DonViTinhs);


            Them_Command = new RelayCommand<object>((p) =>
            {
                if (String.IsNullOrEmpty(TenHang) || GiaBan==0 || Slton==0 || SelectedDVT == null)
                    return false;
                return true;
            }, (p) =>
            {
                var dvt = new Hang() { TenHang =TenHang, GiaBan = GiaBan, SoLuongTon = Slton, IdDVT = SelectedDVT.Id, An = 0 };

                DataProvider.Instance.DB.Hangs.Add(dvt);
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã thêm", "Thông báo");
                ListHang.Add(dvt);
            });

            Sua_Command = new RelayCommand<object>((p) =>
            {
                if (String.IsNullOrEmpty(TenHang) || GiaBan == 0 || Slton == 0)
                    return false;
                return true;
            }, (p) =>
            {
                var dvt = DataProvider.Instance.DB.Hangs.Where(t => t.Id == SelectedHang.Id).SingleOrDefault();
                dvt.TenHang = TenHang;
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã sửa", "Thông báo");
                //SelectedItem.TenHang = DisplayName;
            });

            Xoa_Command = new RelayCommand<object>((p) =>
            {
                if (SelectedHang == null)
                    return false;
                return true;
            }, (p) =>
            {
                var dvt = DataProvider.Instance.DB.Hangs.Where(t => t.Id == SelectedHang.Id).SingleOrDefault();
                dvt.An = 1;
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã xóa", "Thông báo");
                ListHang.Remove(dvt);
            });

            EnterCommand = new RelayCommand<TextBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                ListHang = new ObservableCollection<Hang>(DataProvider.Instance.DB.Hangs.Where(t => t.TenHang.Contains(p.Text)).ToList());
            });
        }
    }
}
