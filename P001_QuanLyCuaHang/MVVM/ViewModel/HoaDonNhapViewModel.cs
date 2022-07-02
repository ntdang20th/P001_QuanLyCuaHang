using P001_QuanLyCuaHang.Functions;
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
        public HoaDonNhap HDN { get=>_HDN; set 
            { 
                _HDN = value; 
                OnPropertyChanged();
                DateString = "Ngày " + DateTime.Today.Day + " tháng " + DateTime.Today.Month + " năm " + DateTime.Today.Year;
            } 
        }

        public ChiTietHDN _CTHD;
        public ChiTietHDN CTHD { get => _CTHD; set { _CTHD = value; OnPropertyChanged(); } }

        public ObservableCollection<ChiTietHDN> _ListCTHDN;
        public ObservableCollection<ChiTietHDN> ListCTHDN { get => _ListCTHDN; set { _ListCTHDN = value; OnPropertyChanged(); } }


        public string _Ten = "";
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

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

        public int _GiaNhap = 0;
        public int GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); TinhThanhTien(); } }

        public string _TienGiam = "";
        public string TienGiam { get => _TienGiam; set { _TienGiam = value; OnPropertyChanged(); } }

        public int _ThanhTien = 0;
        public int ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }



        //hoadon
        public string _SoHD= "";
        public string SoHD { get => _SoHD; set { _SoHD = value; OnPropertyChanged(); } }

        public string _DateString = "";
        public string DateString { get => _DateString; set { _DateString = value; OnPropertyChanged(); } }

        public int _ThanhTienBangSo = 0;
        public int ThanhTienBangSo { get => _ThanhTienBangSo; set { _ThanhTienBangSo = value; OnPropertyChanged(); } }

        public string _ThanhTienBangChu = "";
        public string ThanhTienBangChu { get => _ThanhTienBangChu; set { _ThanhTienBangChu = value; OnPropertyChanged(); } }
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
                    SoLuongTon = (int)SelectedHang.SoLuongTon;
                    SoLuong = 0;
                    GiaNhap = 0;
                }
            }
        }

        private NhaCungCap _SelectedNCC;
        public NhaCungCap SelectedNCC { get => _SelectedNCC; set { _SelectedNCC = value;OnPropertyChanged(); } }

        private ChiTietHDN _SelectedCTHD;
        public ChiTietHDN SelectedCTHD { get => _SelectedCTHD; set { _SelectedCTHD = value; OnPropertyChanged(); } }

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
                if (DangNhap || SelectedNCC == null)
                    return false;
                return true;
            }, (p) =>
            {
                // bat cac nut khac
                DangNhap = !DangNhap;


                //them hoa don
                HDN = new HoaDonNhap() {SoHD = SoHD = CreateID(), NgayNhap = DateTime.Now, IdNCC = SelectedNCC.Id };
                DataProvider.Instance.DB.HoaDonNhaps.Add(HDN);
                DataProvider.Instance.DB.SaveChanges();
                ListCTHDN = new ObservableCollection<ChiTietHDN>();
            });

            ThemSP_Command = new RelayCommand<object>((p) =>
            {
                if (!DangNhap || SelectedHang == null || GiaNhap == 0)
                    return false;
                return true;
            }, (p) =>
            {
                //them sp
                CTHD = new ChiTietHDN() { IdHDN = HDN.SoHD, IdHang = SelectedHang.Id, SoLuong = SoLuong, DonGiaNhap = GiaNhap};
                DataProvider.Instance.DB.ChiTietHDNs.Add(CTHD);
                DataProvider.Instance.DB.SaveChanges();
                CTHD.ThanhTien = ThanhTien;
                ListCTHDN.Add(CTHD);

               

                //add vaof kho
                var hang = DataProvider.Instance.DB.Hangs.Where(x => x.Id == SelectedHang.Id).SingleOrDefault();
                hang.SoLuongTon = hang.SoLuongTon + SoLuong;
                DataProvider.Instance.DB.SaveChanges();

                TinhThanhTienBangSo();
            });

            HuyHD_Command = new RelayCommand<object>((p) =>
            {
                return DangNhap;
            }, (p) =>
            {
                // bat cac nut khac
                DangNhap = !DangNhap;

                //+ trong kho
                foreach (ChiTietHDN i in ListCTHDN)
                {
                    var x = DataProvider.Instance.DB.Hangs.Where(t => t.Id == i.IdHang).SingleOrDefault();
                    x.SoLuongTon -= i.SoLuong;
                    DataProvider.Instance.DB.SaveChanges();
                }

                //hoan tac listcthd
                DataProvider.Instance.DB.ChiTietHDNs.RemoveRange(DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.IdHDN == HDN.SoHD));
                DataProvider.Instance.DB.SaveChanges();

                

                //xoa hoa don
                DataProvider.Instance.DB.HoaDonNhaps.Remove(HDN);
                DataProvider.Instance.DB.SaveChanges();

                //reset form
                SoLuong = 0;
                GiaNhap = 0;
                SoHD = "";
                SelectedNCC = null;
                HDN = null;
                CTHD = null;
                ListCTHDN = null;
                ThanhTienBangSo = 0;
                ThanhTienBangChu = "";
                DateString = "";
                SelectedHang = null;
            });

            XoaSP_Command = new RelayCommand<object>((p) =>
            {
                if (!DangNhap || SelectedCTHD == null )
                    return false;
                return true;
            }, (p) =>
            {
                //- trong kho
                var hang = DataProvider.Instance.DB.Hangs.Where(t => t.Id == SelectedCTHD.IdHang).SingleOrDefault();
                hang.SoLuongTon = hang.SoLuongTon - SoLuong;
                DataProvider.Instance.DB.SaveChanges();

                ChiTietHDN x = DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.Id == SelectedCTHD.Id).SingleOrDefault();
                DataProvider.Instance.DB.ChiTietHDNs.Remove(x);
                DataProvider.Instance.DB.SaveChanges();
                ListCTHDN.Remove(x);
            });

            LuuHD_Command = new RelayCommand<object>((p) =>
            {
                return DangNhap;
            }, (p) =>
            {
                // bat cac nut khac
                DangNhap = !DangNhap;


                //reset form
                SoLuong = 0;
                GiaNhap = 0;
                SoHD = "";
                SelectedNCC = null;
                HDN = null;
                CTHD = null;
                ListCTHDN = null;
                ThanhTienBangSo = 0;
                ThanhTienBangChu = "";
                DateString = "";
                SelectedHang = null;
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
            if (SoLuong == 0  || GiaNhap == 0)
            {
                ThanhTien = 0;
                return;
            }
            ThanhTien = SoLuong * GiaNhap;
        }

        string CreateID()
        {
            DateTime today = DateTime.Today;
            int count = DataProvider.Instance.DB.HoaDonNhaps.Where(x => x.NgayNhap == today).Count();

            string t = "00000" + (count+1);
            t = t.Substring(t.Length - 5, 5);

            return "HDN_" + today.Year + "_" + today.Month + "_" + today.Day + "_" + t;
        }

        void TinhThanhTienBangSo()
        {
            ThanhTienBangSo = 0;
            foreach(ChiTietHDN i in ListCTHDN)
            {
                ThanhTienBangSo += i.ThanhTien;
            }
            ThanhTienBangChu = ChuyenSoThanhChu.DocTienBangChu(ThanhTienBangSo, " đồng.");
        }
        #endregion
    }
}
