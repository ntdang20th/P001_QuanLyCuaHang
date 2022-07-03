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
    public class InHDNViewModel : BaseViewModel
    {
        #region List
        public ObservableCollection<HoaDonNhap> _ListHDN;
        public ObservableCollection<HoaDonNhap> ListHDN { get => _ListHDN; set { _ListHDN = value; OnPropertyChanged(); } }

        public ObservableCollection<ChiTietHDN> _ListCTHDN;
        public ObservableCollection<ChiTietHDN> ListCTHDN { get => _ListCTHDN; set { _ListCTHDN = value; OnPropertyChanged(); } }
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

        private NhaCungCap _SelectedNhaCungCap;
        public NhaCungCap SelectedNCC { get => _SelectedNhaCungCap; set { _SelectedNhaCungCap = value; OnPropertyChanged(); } }

        private HoaDonNhap _SelectedSoHD;
        public HoaDonNhap SelectedSoHD
        {
            get => _SelectedSoHD; set
            {
                _SelectedSoHD = value;
                OnPropertyChanged();
                if (SelectedSoHD != null)
                {
                    SoHD = SelectedSoHD.SoHD;
                    DateString = "Ngày " + DateTime.Today.Day + " tháng " + DateTime.Today.Month + " năm " + DateTime.Today.Year;
                    SelectedNCC = SelectedSoHD.NhaCungCap;

                    ListCTHDN = new ObservableCollection<ChiTietHDN>(DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.IdHDN == SoHD));

                    TinhThanhTienBangSo();

                }
            }
        }

        public ICommand XemHDCommand { get; set; }
        public ICommand InHDCommand { get; set; }

        public InHDNViewModel()
        {
            //hoa don ban
            ListHDN = new ObservableCollection<HoaDonNhap>(DataProvider.Instance.DB.HoaDonNhaps);

            XemHDCommand = new RelayCommand<Grid>((p) =>
            {
                if (String.IsNullOrEmpty(SoHD))
                    return false;
                return true;
            }, (p) =>
            {
                HoaDonNhap hdn = DataProvider.Instance.DB.HoaDonNhaps.Where(t => t.SoHD == SoHD).SingleOrDefault();

                if (hdn == null)
                {
                    MessageBox.Show("Không tìn thấy hóa đơn!");
                    return;
                }

                SelectedSoHD = hdn;
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
                    if (printDialog.ShowDialog() == true)
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
            foreach (ChiTietHDN i in ListCTHDN)
            {
                i.ThanhTien = (int)(i.DonGiaNhap * i.SoLuong);
                ThanhTienBangSo += i.ThanhTien;
            }
            ThanhTienBangChu = ChuyenSoThanhChu.DocTienBangChu(ThanhTienBangSo, " đồng.");
        }
    }
}
