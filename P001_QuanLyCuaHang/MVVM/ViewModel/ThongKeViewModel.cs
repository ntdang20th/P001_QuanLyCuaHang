﻿using P001_QuanLyCuaHang.Functions;
using P001_QuanLyCuaHang.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace P001_QuanLyCuaHang.MVVM.ViewModel
{
    public class ThongKeViewModel:BaseViewModel
    {
        #region List
        private ObservableCollection<HoaDonNhap> _ListHDN;
        public ObservableCollection<HoaDonNhap> ListHDN { get => _ListHDN; set { _ListHDN = value; OnPropertyChanged(); } }

        private ObservableCollection<HoaDonBan> _ListHDB;
        public ObservableCollection<HoaDonBan> ListHDB { get => _ListHDB; set { _ListHDB = value; OnPropertyChanged(); } }

        private ObservableCollection<object> _ListKhachHang;
        public ObservableCollection<object> ListKhachHang { get => _ListKhachHang; set { _ListKhachHang = value; OnPropertyChanged(); } }

        private ObservableCollection<object> _ListNCC;
        public ObservableCollection<object> ListNCC { get => _ListNCC; set { _ListNCC = value; OnPropertyChanged(); } }
        #endregion

        #region Properties
        private int _NhapKho_SP;
        public int NhapKho_SP { get => _NhapKho_SP; set { _NhapKho_SP = value; OnPropertyChanged(); } }

        private int _NhapKho_GT;
        public int NhapKho_GT { get => _NhapKho_GT; set { _NhapKho_GT = value; OnPropertyChanged(); } }

        private int _XuatKho_SP;
        public int XuatKho_SP { get => _XuatKho_SP; set { _XuatKho_SP = value; OnPropertyChanged(); } }

        private int _XuatKho_GT;
        public int XuatKho_GT { get => _XuatKho_GT; set { _XuatKho_GT = value; OnPropertyChanged(); } }

        private int _LoiNhuan;
        public int LoiNhuan { get => _LoiNhuan; set { _LoiNhuan = value; OnPropertyChanged(); } }

        private bool _TuyChon = false;
        public bool TuyChon { get => _TuyChon; set { _TuyChon = value; OnPropertyChanged(); } }


        //hoadon
        public string _SoHD = "";
        public string SoHD { get => _SoHD; set { _SoHD = value; OnPropertyChanged(); } }

        public string _DateString = "";
        public string DateString { get => _DateString; set { _DateString = value; OnPropertyChanged(); } }

        public int _ThanhTienBangSo = 0;
        public int ThanhTienBangSo { get => _ThanhTienBangSo; set { _ThanhTienBangSo = value; OnPropertyChanged(); } }

        public string _ThanhTienBangChu = "";
        public string ThanhTienBangChu { get => _ThanhTienBangChu; set { _ThanhTienBangChu = value; OnPropertyChanged(); } }

        public ObservableCollection<ChiTietHDN> _ListCTHDN;
        public ObservableCollection<ChiTietHDN> ListCTHDN { get => _ListCTHDN; set { _ListCTHDN = value; OnPropertyChanged(); } }

        public ObservableCollection<ChiTietHDB> _ListCTHDB;
        public ObservableCollection<ChiTietHDB> ListCTHDB { get => _ListCTHDB; set { _ListCTHDB = value; OnPropertyChanged(); } }

        #endregion
        #region Selected Item
        private HoaDonNhap _SelectedHDN;
        public HoaDonNhap SelectedHDN { get => _SelectedHDN; set 
            { 
                _SelectedHDN = value; 
                OnPropertyChanged(); 
                if(SelectedHDN != null)
                {
                    SoHD = SelectedHDN.SoHD;
                    DateString = "Ngày " + DateTime.Today.Day + " tháng " + DateTime.Today.Month + " năm " + DateTime.Today.Year;
                    SelectedNCC = SelectedHDN.NhaCungCap;
                    ThanhTienBangSo = SelectedHDN.ThanhTien;
                    ThanhTienBangChu = ChuyenSoThanhChu.DocTienBangChu(ThanhTienBangSo, " đồng.");
                    ListCTHDN = new ObservableCollection<ChiTietHDN>(DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.IdHDN == SelectedHDN.SoHD));
                }
            } 
        }

        private HoaDonBan _SelectedHDB;
        public HoaDonBan SelectedHDB { get => _SelectedHDB; set 
            { 
                _SelectedHDB = value; 
                OnPropertyChanged();
                if (SelectedHDB != null)
                {
                    SoHD = SelectedHDB.SoHD;
                    DateString = "Ngày " + DateTime.Today.Day + " tháng " + DateTime.Today.Month + " năm " + DateTime.Today.Year;
                    SelectedKhachHang = SelectedHDB.KhachHang;
                    ThanhTienBangSo = SelectedHDB.ThanhTien;
                    ThanhTienBangChu = ChuyenSoThanhChu.DocTienBangChu(ThanhTienBangSo, " đồng.");
                    ListCTHDB = new ObservableCollection<ChiTietHDB>(DataProvider.Instance.DB.ChiTietHDBs.Where(t => t.IdHDB == SelectedHDB.SoHD));
                }
            } 
        }

        private KhachHang _SelectedKhachHang;
        public KhachHang SelectedKhachHang { get => _SelectedKhachHang; set { _SelectedKhachHang = value; OnPropertyChanged(); } }

        private NhaCungCap _SelectedNCC;
        public NhaCungCap SelectedNCC { get => _SelectedNCC; set { _SelectedNCC = value; OnPropertyChanged(); } }
        #endregion

        #region Command
        public ICommand Ngay_Command { get; set; }
        public ICommand Tuan_Command { get; set; }
        public ICommand Thang_Command { get; set; }
        public ICommand Quy_Command { get; set; }
        public ICommand Nam_Command { get; set; }
        public ICommand TuyChon_Command { get; set; }
        public ICommand ThongKe_Command { get; set; }
        #endregion

        public ThongKeViewModel()
        {

            Ngay_Command = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TuyChon = false;
                DateTime today = DateTime.Today;

                //danh sách hóa đơn trong ngày
                ListHDN = new ObservableCollection<HoaDonNhap>( DataProvider.Instance.DB.HoaDonNhaps.Where(t => t.NgayNhap == today));
                ListHDB = new ObservableCollection<HoaDonBan>( DataProvider.Instance.DB.HoaDonBans.Where(t => t.NgayBan == today));
                TinhTien();
                LayDSKHvNCC();
            });

            Tuan_Command = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TuyChon = false;
                int thisWeekNumber = GetIso8601WeekOfYear(DateTime.Today);
                DateTime firstDayOfWeek = FirstDateOfWeek(DateTime.Today.Year, thisWeekNumber, CultureInfo.CurrentCulture);
                DateTime lastDayOfWeek = firstDayOfWeek.AddDays(6);

                //danh sách hóa đơn trong ngày
                ListHDN = new ObservableCollection<HoaDonNhap>(DataProvider.Instance.DB.HoaDonNhaps.Where(t => t.NgayNhap <= lastDayOfWeek && t.NgayNhap>=firstDayOfWeek));
                ListHDB = new ObservableCollection<HoaDonBan>( DataProvider.Instance.DB.HoaDonBans.Where(t => t.NgayBan <= lastDayOfWeek && t.NgayBan >= firstDayOfWeek));
                TinhTien();
                LayDSKHvNCC();
            });

            Thang_Command = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TuyChon = false;

                //ngay dau thang nay`
                DateTime firstDayOfMonth = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);

                //danh sách hóa đơn trong ngày
                ListHDN = new  ObservableCollection<HoaDonNhap>(DataProvider.Instance.DB.HoaDonNhaps.Where(t => t.NgayNhap >= firstDayOfMonth));
                ListHDB = new  ObservableCollection<HoaDonBan>(DataProvider.Instance.DB.HoaDonBans.Where(t => t.NgayBan >= firstDayOfMonth));
                TinhTien();
                LayDSKHvNCC();
            });

            Quy_Command = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TuyChon = false;

                //ngay dau mua` nay`
                int thangdaumua = FistMonthOfSeason(DateTime.Today.Month);
                DateTime firstDayOfSeason = new DateTime(DateTime.Today.Year, thangdaumua, 1);

                //danh sách hóa đơn trong ngày
                ListHDN = new ObservableCollection<HoaDonNhap>(DataProvider.Instance.DB.HoaDonNhaps.Where(t => t.NgayNhap >= firstDayOfSeason));
                ListHDB = new ObservableCollection<HoaDonBan>(DataProvider.Instance.DB.HoaDonBans.Where(t => t.NgayBan >= firstDayOfSeason));
                TinhTien();
                LayDSKHvNCC();
            });

            Nam_Command = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TuyChon = false;

                //danh sách hóa đơn trong ngày
                ListHDN = new ObservableCollection<HoaDonNhap>(DataProvider.Instance.DB.HoaDonNhaps.Where(t => t.NgayNhap.Value.Year >= DateTime.Today.Year));
                ListHDB = new ObservableCollection<HoaDonBan>(DataProvider.Instance.DB.HoaDonBans.Where(t => t.NgayBan.Value.Year >= DateTime.Today.Year));
                TinhTien();
                LayDSKHvNCC();
            });

            TuyChon_Command = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                TuyChon = true;
            });

            ThongKe_Command = new RelayCommand<Grid>((p) =>
            {
                return true;
            }, (p) =>
            {
                DateTime ngaybd = DateTime.Parse(p.Children[0].ToString());
                DateTime ngaykt = DateTime.Parse(p.Children[1].ToString());

                //danh sách hóa đơn trong ngày
                ListHDN = new ObservableCollection<HoaDonNhap>(DataProvider.Instance.DB.HoaDonNhaps.Where(t => t.NgayNhap >= ngaybd && t.NgayNhap <= ngaykt));
                ListHDB = new ObservableCollection<HoaDonBan>(DataProvider.Instance.DB.HoaDonBans.Where(t => t.NgayBan >= ngaybd && t.NgayBan <= ngaykt));
                TinhTien();
                LayDSKHvNCC();
            });
        }

        #region Method
        void tinhThanhTienHDN(List<ChiTietHDN> list)
        {
            foreach(ChiTietHDN i in list)
            {
                i.ThanhTien = (int)(i.SoLuong * i.DonGiaNhap);
            }
        }

        void tinhThanhTienHDB(List<ChiTietHDB> list)
        {
            foreach (ChiTietHDB i in list)
            {
                i.ThanhTien = (int)(i.SoLuong * i.DonGiaBan);
            }
        }

        public static int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime FirstDateOfWeek(int year, int weekOfYear, System.Globalization.CultureInfo ci)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = (int)ci.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
            DateTime firstWeekDay = jan1.AddDays(daysOffset);
            int firstWeek = ci.Calendar.GetWeekOfYear(jan1, ci.DateTimeFormat.CalendarWeekRule, ci.DateTimeFormat.FirstDayOfWeek);
            if ((firstWeek <= 1 || firstWeek >= 52) && daysOffset >= -3)
            {
                weekOfYear -= 1;
            }
            return firstWeekDay.AddDays(weekOfYear * 7);
        }

        public static int FistMonthOfSeason(int month)
        {
            if (month < 4)
            {
                return 1;
            }
            else if (month < 7)
            {
                return 4;
            }
            else if (month < 10)
            {
                return 7;
            }
            return 10;
        }

        void TinhTien()
        {
            //tính
            NhapKho_SP = NhapKho_GT = 0;
            foreach (var i in ListHDN)
            {
                List<ChiTietHDN> chitiet = new List<ChiTietHDN>(DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.IdHDN == i.SoHD));
                NhapKho_SP += (int)chitiet.Sum(x => x.SoLuong);
                tinhThanhTienHDN(chitiet);
                i.ThanhTien = chitiet.Sum(x => x.ThanhTien);
                NhapKho_GT += i.ThanhTien;
            }

            XuatKho_GT = XuatKho_SP = 0;
            foreach (var i in ListHDB)
            {
                List<ChiTietHDB> chitiet = new List<ChiTietHDB>(DataProvider.Instance.DB.ChiTietHDBs.Where(t => t.IdHDB == i.SoHD));
                XuatKho_SP += (int)chitiet.Sum(x => x.SoLuong);
                tinhThanhTienHDB(chitiet);
                i.ThanhTien = chitiet.Sum(x => x.ThanhTien);
                XuatKho_GT += i.ThanhTien;
            }

            LoiNhuan = XuatKho_GT - NhapKho_GT;
        }

        void LayDSKHvNCC()
        {
            ListKhachHang = new ObservableCollection<object>();
            ListNCC = new ObservableCollection<object>();

            IEnumerable<int?> idkh = ListHDB.Select(i => i.Makh).Distinct();
            IEnumerable<int?> idncc = ListHDN.Select(i => i.IdNCC).Distinct();

            foreach(int i in idkh)
            {
                int count = ListHDB.Where(t => t.Makh == i).Count();
                var kh = ListHDB.Where(t => t.Makh == i).Select(x => new { TenKH = x.KhachHang.HoTen, SLHD = count, TongTT = x.ChiTietHDBs.Sum(y => y.ThanhTien) });
                ListKhachHang.Add(kh);
            }

            foreach (int i in idncc)
            {
                int count = ListHDN.Where(t => t.IdNCC == i).Count();
                var ncc = ListHDN.Where(t => t.IdNCC == i).Select(x => new { TenNCC = x.NhaCungCap.Ten, SLHD = count, TongTT = x.ChiTietHDNs.Sum(y => y.ThanhTien) });
                ListNCC.Add(ncc);
            }
        }
        #endregion
    }
}
