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

        public ObservableCollection<NhaCungCap> _ListNCC;
        public ObservableCollection<NhaCungCap> ListNCC { get => _ListNCC; set { _ListNCC = value; OnPropertyChanged(); } }
        #endregion

        #region Properties
        private HoaDonNhap _HDN;
        public HoaDonNhap HDN { get=>_HDN; set { _HDN = value; OnPropertyChanged(); } }

        public ObservableCollection<ChiTietHDN> _ListCTHDN;
        public ObservableCollection<ChiTietHDN> ListCTHDN { get => _ListCTHDN; set { _ListCTHDN = value; OnPropertyChanged(); } }


        public string _Ten = "";
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        public int _GiaBan = 0;
        public int GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        public int _SoLuongTon = 0;
        public int SoLuongTon { get => _SoLuongTon; set { _SoLuongTon = value; OnPropertyChanged(); } }

        public bool _DangNhap = false;
        public bool DangNhap { get => _DangNhap; set { _DangNhap = value; OnPropertyChanged(); } }


        public string _NCCSearchText = "";
        public string NCCSearchText { get => _NCCSearchText; 
            set 
            { 
                _NCCSearchText = value; 
                OnPropertyChanged();
                ListNCC = new ObservableCollection<NhaCungCap>(DataProvider.Instance.DB.NhaCungCaps.Where(t => t.Ten.Contains(NCCSearchText)));
            } 
        }


        public int _SoLuong = 0;
        public int SoLuong 
        { 
            get => _SoLuong; 
            set { _SoLuong = value; 
                OnPropertyChanged();
                TinhThanhTien();
            } 
        }

        public int _KhuyenMai = 0;
        public int KhuyenMai { get => _KhuyenMai; 
            set 
            { 
                _KhuyenMai = value; 
                OnPropertyChanged();
                TinhThanhTien();
            } 
        }

        public string _TienGiam = "";
        public string TienGiam { get => _TienGiam; set { _TienGiam = value; OnPropertyChanged(); } }

        public int _ThanhTien = 0;
        public int ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }
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
                    SoLuong = 0;
                    KhuyenMai = 0;
                    TienGiam = "";
                }
            }
        }

        private NhaCungCap _SelectedNCC;
        public NhaCungCap SelectedNCC { get => _SelectedNCC; set { _SelectedNCC = value;OnPropertyChanged(); } }

        #endregion

        #region Command
        public ICommand ChiTietNCC_Command { get; set; }
        public ICommand ThemHD_Command { get; set; }
        public ICommand ThemSP_Command { get; set; }
        public ICommand LuuHD_Command { get; set; }
        public ICommand XoaSP_Command { get; set; }
        public ICommand HuyHD_Command { get; set; }
        public ICommand InHD_Command { get; set; }
        #endregion


        public HoaDonNhapViewModel()
        {
            ListHang = new ObservableCollection<Hang>(DataProvider.Instance.DB.Hangs);
            ListNCC = new ObservableCollection<NhaCungCap>(DataProvider.Instance.DB.NhaCungCaps);

            ThemHD_Command = new RelayCommand<object>((p) =>
            {
                return !DangNhap;
            }, (p) =>
            {
                // bat cac nut khac
                DangNhap = !DangNhap;


                //them hoa don

            });

            ThemSP_Command = new RelayCommand<object>((p) =>
            {
                return DangNhap;
            }, (p) =>
            { 

                //them hoa don
            });

            HuyHD_Command = new RelayCommand<object>((p) =>
            {
                return DangNhap;
            }, (p) =>
            {


                //them hoa don
            });

            XoaSP_Command = new RelayCommand<object>((p) =>
            {
                return DangNhap;
            }, (p) =>
            {

                //them hoa don
            });

            LuuHD_Command = new RelayCommand<object>((p) =>
            {
                return DangNhap;
            }, (p) =>
            {
                // bat cac nut khac
                DangNhap = !DangNhap;


                //them hoa don
            });

            InHD_Command = new RelayCommand<object>((p) =>
            {
                return DangNhap;
            }, (p) =>
            {


                //them hoa don
            });
        }


        #region Method
        void TinhThanhTien()
        {
            if (SoLuong == 0 || KhuyenMai == 0 || GiaBan == 0)
            {
                ThanhTien = 0;
                return;
            }
            int km = SoLuong * GiaBan * KhuyenMai / 100;
            ThanhTien = SoLuong * GiaBan - km;
            TienGiam = "( -" + km + ")";
        }
        #endregion
    }
}
