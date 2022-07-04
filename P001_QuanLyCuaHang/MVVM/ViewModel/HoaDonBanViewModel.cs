using P001_QuanLyCuaHang.Functions;
using P001_QuanLyCuaHang.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace P001_QuanLyCuaHang.MVVM.ViewModel
{
    public class HoaDonBanViewModel : BaseViewModel
    {
        #region List
        public ObservableCollection<Hang> _ListHang;
        public ObservableCollection<Hang> ListHang { get => _ListHang; set { _ListHang = value; OnPropertyChanged(); } }

        public ObservableCollection<KhachHang> _ListKhachHang;
        public ObservableCollection<KhachHang> ListKhachHang { get => _ListKhachHang; set { _ListKhachHang = value; OnPropertyChanged(); } }
        #endregion

        #region Properties
        private HoaDonBan _HDB;
        public HoaDonBan HDB
        {
            get => _HDB; set
            {
                _HDB = value;
                OnPropertyChanged();
                DateString = "Ngày " + DateTime.Today.Day + " tháng " + DateTime.Today.Month + " năm " + DateTime.Today.Year;
            }
        }

        public ChiTietHDB _CTHD;
        public ChiTietHDB CTHD { get => _CTHD; set { _CTHD = value; OnPropertyChanged(); } }

        public ObservableCollection<ChiTietHDB> _ListCTHDB;
        public ObservableCollection<ChiTietHDB> ListCTHDB { get => _ListCTHDB; set { _ListCTHDB = value; OnPropertyChanged(); } }


        public string _Ten = "";
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        public int _SoLuongTon = 0;
        public int SoLuongTon { get => _SoLuongTon; set { _SoLuongTon = value; OnPropertyChanged(); } }

        public bool _DangBan = false;
        public bool DangBan { get => _DangBan; set { _DangBan = value; OnPropertyChanged(); } }


        public string _KhachHangSearchText = "";
        public string KhachHangSearchText
        {
            get => _KhachHangSearchText;
            set
            {
                _KhachHangSearchText = value;
                OnPropertyChanged();
                ListKhachHang = new ObservableCollection<KhachHang>(DataProvider.Instance.DB.KhachHangs.Where(t => t.HoTen.Contains(KhachHangSearchText)));
            }
        }


        public int _SoLuong = 0;
        public int SoLuong
        {
            get => _SoLuong;
            set
            {
                _SoLuong = value;
                OnPropertyChanged();
                TinhThanhTien();
            }
        }

        public int _GiaBan = 0;
        public int GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); TinhThanhTien(); } }

        public int _KhuyenMai = 0;
        public int KhuyenMai { get => _KhuyenMai; set { _KhuyenMai = value; OnPropertyChanged(); TinhThanhTien(); } }

        public string _TienGiam = "";
        public string TienGiam { get => _TienGiam; set { _TienGiam = value; OnPropertyChanged(); } }

        public int _ThanhTien = 0;
        public int ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }



        //hoadon
        public string _SoHD = "";
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
                    GiaBan = (int)SelectedHang.GiaBan;
                    SoLuong = 0;
                }
            }
        }

        private KhachHang _SelectedKhachHang;
        public KhachHang SelectedKhachHang { get => _SelectedKhachHang; set { _SelectedKhachHang = value; OnPropertyChanged(); } }

        private ChiTietHDB _SelectedCTHD;
        public ChiTietHDB SelectedCTHD { get => _SelectedCTHD; set { _SelectedCTHD = value; OnPropertyChanged(); } }

        #endregion

        #region Command
        public ICommand ChiTietKhachHang_Command { get; set; }
        public ICommand ThemHD_Command { get; set; }
        public ICommand ThemSP_Command { get; set; }
        public ICommand LuuHD_Command { get; set; }
        public ICommand XoaSP_Command { get; set; }
        public ICommand HuyHD_Command { get; set; }
        public ICommand InHD_Command { get; set; }
        #endregion


        public HoaDonBanViewModel()
        {
            ListHang = new ObservableCollection<Hang>(DataProvider.Instance.DB.Hangs);
            ListKhachHang = new ObservableCollection<KhachHang>(DataProvider.Instance.DB.KhachHangs);

            ThemHD_Command = new RelayCommand<object>((p) =>
            {
                if (DangBan || SelectedKhachHang == null)
                    return false;
                return true;
            }, (p) =>
            {
                // bat cac nut khac
                DangBan = !DangBan;


                //them hoa don
                HDB = new HoaDonBan() { SoHD = SoHD = CreateID(), NgayBan = DateTime.Now, Makh = SelectedKhachHang.MaKh };
                DataProvider.Instance.DB.HoaDonBans.Add(HDB);
                DataProvider.Instance.DB.SaveChanges();
                ListCTHDB = new ObservableCollection<ChiTietHDB>();

                MainViewModel.chualuu = true;
            });

            ThemSP_Command = new RelayCommand<object>((p) =>
            {
            if (!DangBan || SelectedHang == null || KhuyenMai > 100 || SoLuong <= 0 || SoLuong > SoLuongTon ||KhuyenMai < 0)
                    return false;
                return true;
            }, (p) =>
            {
                //them sp
                CTHD = new ChiTietHDB() { IdHDB = HDB.SoHD, IdHang = SelectedHang.Id, SoLuong = SoLuong, DonGiaBan = GiaBan, KhuyenMai = KhuyenMai };
                DataProvider.Instance.DB.ChiTietHDBs.Add(CTHD);
                DataProvider.Instance.DB.SaveChanges();
                CTHD.ThanhTien = ThanhTien;
                ListCTHDB.Add(CTHD);

                //- trong kho
                var hang = DataProvider.Instance.DB.Hangs.Where(x => x.Id == SelectedHang.Id).SingleOrDefault();
                hang.SoLuongTon = hang.SoLuongTon - SoLuong;
                DataProvider.Instance.DB.SaveChanges();

                TinhThanhTienBangSo();
            });

            HuyHD_Command = new RelayCommand<object>((p) =>
            {
                return DangBan;
            }, (p) =>
            {
                // bat cac nut khac
                DangBan = !DangBan;

                //+ trong kho
                foreach (ChiTietHDB i in ListCTHDB)
                {
                    var x = DataProvider.Instance.DB.Hangs.Where(t => t.Id == i.IdHang).SingleOrDefault();
                    x.SoLuongTon += i.SoLuong;
                    DataProvider.Instance.DB.SaveChanges();
                }

                //hoan tac listcthd
                DataProvider.Instance.DB.ChiTietHDBs.RemoveRange(DataProvider.Instance.DB.ChiTietHDBs.Where(t => t.IdHDB == HDB.SoHD));
                DataProvider.Instance.DB.SaveChanges();

                

                //xoa hoa don
                DataProvider.Instance.DB.HoaDonBans.Remove(HDB);
                DataProvider.Instance.DB.SaveChanges();

                //reset form
                SoLuong = 0;
                GiaBan = 0;
                SoHD = "";
                SelectedKhachHang = null;
                HDB = null;
                CTHD = null;
                ListCTHDB = null;
                ThanhTienBangSo = 0;
                ThanhTienBangChu = "";
                DateString = "";
                SelectedHang = null;

                MainViewModel.chualuu = false;
            });

            XoaSP_Command = new RelayCommand<object>((p) =>
            {
                if (!DangBan || SelectedCTHD == null)
                    return false;
                return true;
            }, (p) =>
            {
                //+ trong kho
                var hang = DataProvider.Instance.DB.Hangs.Where(t => t.Id == SelectedCTHD.IdHang).SingleOrDefault();
                hang.SoLuongTon = hang.SoLuongTon + SoLuong;
                DataProvider.Instance.DB.SaveChanges();

                ChiTietHDB x = DataProvider.Instance.DB.ChiTietHDBs.Where(t => t.Id == SelectedCTHD.Id).SingleOrDefault();
                DataProvider.Instance.DB.ChiTietHDBs.Remove(x);
                DataProvider.Instance.DB.SaveChanges();
                ListCTHDB.Remove(x);

                
            });

            LuuHD_Command = new RelayCommand<object>((p) =>
            {
                return DangBan;
            }, (p) =>
            {
                // bat cac nut khac
                DangBan = !DangBan;


                //reset form
                SoLuong = 0;
                GiaBan = 0;
                SoHD = "";
                SelectedKhachHang = null;
                HDB = null;
                CTHD = null;
                ListCTHDB = null;
                ThanhTienBangSo = 0;
                ThanhTienBangChu = "";
                DateString = "";
                SelectedHang = null;

                MainViewModel.chualuu = false;
            });

            InHD_Command = new RelayCommand<Grid>((p) =>
            {
                return true;
            }, (p) =>
            {
                InHDB f = new InHDB(SoHD);
                f.ShowDialog();
            });
        }


        #region Method
        object getCard(Grid g)
        {
            object card = null;
            var x = g.Children[0];
            var y = (x as Grid).Children;

            return card;
        }

        void TinhThanhTien()
        {
            if (SoLuong == 0 || GiaBan == 0)
            {
                ThanhTien = 0;
                return;
            }
            
            ThanhTien = SoLuong * GiaBan - SoLuong*GiaBan*KhuyenMai/100;
            TienGiam = " ( -" + SoLuong * GiaBan * KhuyenMai / 100 + " )";
        }

        string CreateID()
        {
            DateTime today = DateTime.Today;
            int count = DataProvider.Instance.DB.HoaDonBans.Where(x => x.NgayBan == today).Count();

            string t = "00000" + (count++ + 1);
            t = t.Substring(t.Length - 5, 5);

            string id = "HDB_" + today.Year + "_" + today.Month + "_" + today.Day + "_" + t;

            var ds = DataProvider.Instance.DB.HoaDonBans.Where(x => x.SoHD == id).ToList();
            while (ds.Count > 0)
            {
                t = "00000" + (count++ + 1);
                t = t.Substring(t.Length - 5, 5);

                id = "HDB_" + today.Year + "_" + today.Month + "_" + today.Day + "_" + t;

                ds = DataProvider.Instance.DB.HoaDonBans.Where(x => x.SoHD == id).ToList();
            }


            return id;
        }

        void TinhThanhTienBangSo()
        {
            ThanhTienBangSo = 0;
            foreach (ChiTietHDB i in ListCTHDB)
            {
                ThanhTienBangSo += i.ThanhTien;
            }
            ThanhTienBangChu = ChuyenSoThanhChu.DocTienBangChu(ThanhTienBangSo, " đồng.");
        }
        #endregion
    }
}
