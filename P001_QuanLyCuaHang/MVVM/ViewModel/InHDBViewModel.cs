using P001_QuanLyCuaHang.Functions;
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
    public class InHDBViewModel : BaseViewModel
    {
        #region List
        public ObservableCollection<HoaDonBan> _ListHDB;
        public ObservableCollection<HoaDonBan> ListHDB { get => _ListHDB; set { _ListHDB = value; OnPropertyChanged(); } }

        public ObservableCollection<ChiTietHDB> _ListCTHDB;
        public ObservableCollection<ChiTietHDB> ListCTHDB { get => _ListCTHDB; set { _ListCTHDB = value; OnPropertyChanged(); } }
        #endregion

        //hoadon
        public string _SoHD = "";
        public string SoHD { get => _SoHD; set { _SoHD = value; OnPropertyChanged(); } }

        public string _DateString = "";
        public string DateString { get => _DateString; set { _DateString = value; OnPropertyChanged(); } }

        public int _ThanhTienBangSo = 0;
        public int ThanhTienBangSo { get => _ThanhTienBangSo; set { _ThanhTienBangSo = value; OnPropertyChanged(); } }

        public string _ThanhTienBangChu = "";
        public string ThanhTienBangChu { get => _ThanhTienBangChu; set { _ThanhTienBangChu = value; OnPropertyChanged(); } }

        private KhachHang _SelectedKhachHang;
        public KhachHang SelectedKhachHang { get => _SelectedKhachHang; set { _SelectedKhachHang = value; OnPropertyChanged(); } }

        private HoaDonBan _SelectedSoHD;
        public HoaDonBan SelectedSoHD { get=>_SelectedSoHD; set 
            {
                _SelectedSoHD = value;
                OnPropertyChanged();
                if(SelectedSoHD != null)
                {
                    SoHD = SelectedSoHD.SoHD;
                    DateString = "Ngày " + DateTime.Today.Day + " tháng " + DateTime.Today.Month + " năm " + DateTime.Today.Year;
                    SelectedKhachHang = SelectedSoHD.KhachHang;

                    ListCTHDB = new ObservableCollection<ChiTietHDB>(DataProvider.Instance.DB.ChiTietHDBs.Where(t => t.IdHDB == SoHD));

                    TinhThanhTienBangSo();

                }
            } 
        }

        #region Command
        public ICommand XemHDCommand { get; set; }
        public ICommand InHDCommand { get; set; }
        #endregion

        public InHDBViewModel()
        {
             //hoa don ban
            ListHDB = new ObservableCollection<HoaDonBan>(DataProvider.Instance.DB.HoaDonBans);

            XemHDCommand = new RelayCommand<Grid>((p) =>
            {
                if (String.IsNullOrEmpty(SoHD))
                    return false;
                return true;
            }, (p) =>
            {
                HoaDonBan hdb = DataProvider.Instance.DB.HoaDonBans.Where(t => t.SoHD == SoHD).SingleOrDefault();

                if(hdb == null)
                {
                    MessageBox.Show("Không tìn thấy hóa đơn!");
                    return;
                }

                SelectedSoHD = hdb;
            });


            InHDCommand = new RelayCommand<Grid>((p) =>
            {
                if (String.IsNullOrEmpty(SoHD) || SelectedSoHD == null)
                    return false;
                return true;
            }, (p) =>
            {
                try
                {
                    PrintDialog printDialog = new PrintDialog();
                    if(printDialog.ShowDialog() == true)
                    {
                        printDialog.PrintVisual(p, "Hóa đơn bán hàng");
                    }
                }
                finally
                {

                }
            });
        }

        void TinhThanhTienBangSo()
        {
            ThanhTienBangSo = 0;
            foreach (ChiTietHDB i in ListCTHDB)
            {
                i.ThanhTien = (int)(i.DonGiaBan*i.SoLuong - i.DonGiaBan * i.SoLuong*i.KhuyenMai/100);
                ThanhTienBangSo += i.ThanhTien;
            }
            ThanhTienBangChu = ChuyenSoThanhChu.DocTienBangChu(ThanhTienBangSo, " đồng.");
        }
    }
}
