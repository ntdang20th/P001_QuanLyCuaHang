using P001_QuanLyCuaHang.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace P001_QuanLyCuaHang.MVVM.ViewModel
{
    public class DonViTinhViewModel:BaseViewModel
    {
        #region List
        public ObservableCollection<DonViTinh> _ListDonViTinh;
        public ObservableCollection<DonViTinh> ListDonViTinh { get => _ListDonViTinh; set { _ListDonViTinh = value; OnPropertyChanged(); } }
        #endregion

        #region Properties
        public string _Ten = "";
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }
        #endregion

        #region Selected Item
        public DonViTinh _SelectedDonViTinh;
        public DonViTinh SelectedDonViTinh
        {
            get => _SelectedDonViTinh; set
            {
                _SelectedDonViTinh = value; OnPropertyChanged();
                if (SelectedDonViTinh != null)
                {
                    Ten = SelectedDonViTinh.TenDonViTinh;
                }
            }
        }
        #endregion

        #region Command
        public ICommand Them_Command { get; set; }
        public ICommand Sua_Command { get; set; }
        public ICommand Xoa_Command { get; set; }
        #endregion


        public DonViTinhViewModel()
        {
            ListDonViTinh = new ObservableCollection<DonViTinh>(DataProvider.Instance.DB.DonViTinhs);

            Them_Command = new RelayCommand<object>((p) =>
            {
                if (String.IsNullOrEmpty(Ten))
                    return false;
                return true;
            }, (p) =>
            {
                var donViTinh = new DonViTinh() { TenDonViTinh = Ten };

                DataProvider.Instance.DB.DonViTinhs.Add(donViTinh);
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã thêm", "Thông báo");
                ListDonViTinh.Add(donViTinh);
            });

            Sua_Command = new RelayCommand<object>((p) =>
            {
                if (String.IsNullOrEmpty(Ten))
                    return false;
                return true;
            }, (p) =>
            {
                var donViTinh = DataProvider.Instance.DB.DonViTinhs.Where(t => t.Id == SelectedDonViTinh.Id).SingleOrDefault();
                donViTinh.TenDonViTinh = Ten;
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã sửa", "Thông báo");
                //SelectedItem.TenDonViTinh = DisplayName;
            });

            Xoa_Command = new RelayCommand<object>((p) =>
            {
                if (SelectedDonViTinh == null)
                    return false;
                return true;
            }, (p) =>
            {
                var donViTinh = DataProvider.Instance.DB.DonViTinhs.Where(t => t.Id == SelectedDonViTinh.Id).SingleOrDefault();
                DataProvider.Instance.DB.DonViTinhs.Remove(donViTinh);
                DataProvider.Instance.DB.SaveChanges();
                MessageBox.Show("Đã xóa", "Thông báo");
                ListDonViTinh.Remove(donViTinh);
            });
        }

    }
}
