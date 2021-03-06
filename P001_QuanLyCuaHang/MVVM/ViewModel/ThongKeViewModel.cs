using ClosedXML.Excel;
using ExcelDataReader;
using P001_QuanLyCuaHang.Functions;
using P001_QuanLyCuaHang.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

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

        private ObservableCollection<object> _ListHetHang;
        public ObservableCollection<object> ListHetHang { get => _ListHetHang; set { _ListHetHang = value; OnPropertyChanged(); } }
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

        private string _Status;
        public string Status { get => _Status; set { _Status = value; OnPropertyChanged(); } }

        private string _GTStatus;
        public string GTStatus { get => _GTStatus; set { _GTStatus = value; OnPropertyChanged(); } }

        private int _GTKho;
        public int GTKho { get => _GTKho; set { _GTKho = value; OnPropertyChanged(); } }

        private int _GTBan;
        public int GTBan { get => _GTBan; set { _GTBan = value; OnPropertyChanged(); } }

        private bool _TuyChon = false;
        public bool TuyChon { get => _TuyChon; set { _TuyChon = value; OnPropertyChanged(); } }

        private DateTime _NgayBD;
        public DateTime NgayBD { get => _NgayBD; set { _NgayBD = value; OnPropertyChanged(); } }

        private DateTime _NgayKT;
        public DateTime NgayKT { get => _NgayKT; set { _NgayKT = value; OnPropertyChanged(); } }

        #endregion
        #region Selected Item
        private HoaDonNhap _SelectedHDN;
        public HoaDonNhap SelectedHDN { get => _SelectedHDN; set 
            { 
                _SelectedHDN = value; 
                OnPropertyChanged(); 
                if(SelectedHDN != null)
                {
                   
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
        public ICommand ChiTietHDB_Command { get; set; }
        public ICommand ChiTietHDN_Command { get; set; }
        public ICommand DonRacCommand { get; set; }
        public ICommand XuatExcelCommand { get; set; }
        #endregion

        public ThongKeViewModel()
        {
            LayDSHetHang();
            TinhHoanVon();
            NgayBD = NgayKT = DateTime.Today;

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
                demSTT();
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
                demSTT();
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
                demSTT();
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
                demSTT();
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
                demSTT();
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

            ThongKe_Command = new RelayCommand<object>((p) =>
            {
                if (NgayBD == null || NgayKT == null )
                {
                    return false;
                }
                return true;
            }, (p) =>
            {
                //danh sách hóa đơn trong ngày
                ListHDN = new ObservableCollection<HoaDonNhap>(DataProvider.Instance.DB.HoaDonNhaps.Where(t => t.NgayNhap >= NgayBD && t.NgayNhap <= NgayKT));
                ListHDB = new ObservableCollection<HoaDonBan>(DataProvider.Instance.DB.HoaDonBans.Where(t => t.NgayBan >= NgayBD && t.NgayBan <= NgayKT));
                demSTT();
                TinhTien();
                LayDSKHvNCC();
            });

            ChiTietHDN_Command = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                InHDN f = new InHDN(SelectedHDN.SoHD);
                f.ShowDialog();
            });

            ChiTietHDB_Command = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                InHDB f = new InHDB(SelectedHDB.SoHD);
                f.ShowDialog();
            });

            DonRacCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (ListHDB == null || ListHDN == null)
                {
                    System.Windows.Forms.MessageBox.Show("Đã dọn xong!");
                    return;
                }

                List<string> listCTHDB = DataProvider.Instance.DB.ChiTietHDBs.Select(t=>t.IdHDB).Distinct().ToList();
                foreach(var i in ListHDB.ToList())
                {
                    if (!listCTHDB.Contains(i.SoHD))
                    {
                        ListHDB.Remove(i);
                        DataProvider.Instance.DB.HoaDonBans.Remove(i);
                    }
                }
                
                List<string> listCTHDN = DataProvider.Instance.DB.ChiTietHDNs.Select(t => t.IdHDN).Distinct().ToList();
                foreach (var i in ListHDN.ToList())
                {
                    if (!listCTHDN.Contains(i.SoHD))
                    {
                        ListHDN.Remove(i);
                        DataProvider.Instance.DB.HoaDonNhaps.Remove(i);
                    }
                }
                DataProvider.Instance.DB.SaveChanges();
                System.Windows.Forms.MessageBox.Show("Đã dọn xong!");
            });

            XuatExcelCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx" })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            using(XLWorkbook workbook = new XLWorkbook())
                            {
                                //Inizializar Librerias
                                workbook.AddWorksheet("ThongKeHoaDon");
                                var ws = workbook.Worksheet("ThongKeHoaDon");
                                //Recorrer el objecto


                                //status thong ke
                                ws.Range("A1", "K1").Merge();
                                ws.Cell("A1").Value = "Ngày xuất báo cáo: " + DateTime.Today.ToString("dd/MM/yyyy");
                                ws.Cell("A1").Style.Font.FontSize = 20;

                                //header

                                ws.Range("A2", "K2").Style.Fill.BackgroundColor = XLColor.Yellow;
                                ws.Cell("A2").Value = "STT";
                                ws.Cell("B2").Value = "SoHD";
                                ws.Cell("C2").Value = "Date";
                                ws.Cell("D2").Value = "Số loại SP";
                                ws.Cell("E2").Value = "Số lượng SP";
                                ws.Cell("F2").Value = "Khuyến mãi";
                                ws.Cell("G2").Value = "Tổng cộng";
                                ws.Cell("H2").Value = "Tên KH / NCC";
                                ws.Cell("I2").Value = "SDT";
                                ws.Cell("J2").Value = "Địa chỉ";
                                ws.Cell("K2").Value = "Ghi chú";

                                //chèn data hóa đơn nhập
                                ws.Range("A3", "K3").Merge();
                                ws.Cell("A3").Value = "Hóa đơn nhập";

                                int row = 4;
                                foreach (HoaDonNhap i in ListHDN)
                                {
                                    ws.Cell("A" + row).Value = row-3;
                                    ws.Cell("B" + row).Value = i.SoHD;
                                    ws.Cell("C" + row).Value = ((DateTime)i.NgayNhap).ToString("dd/MM/yyyy");
                                    ws.Cell("D" + row).Value = i.ChiTietHDNs.Count;
                                    ws.Cell("E" + row).Value = DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.IdHDN == i.SoHD).Sum(t => t.SoLuong);
                                    ws.Cell("F" + row).Value = 0;
                                    ws.Cell("G" + row).Value = i.ThanhTien;
                                    ws.Cell("H" + row).Value = i.NhaCungCap.Ten;
                                    ws.Cell("I" + row).Value = i.NhaCungCap.Sdt;
                                    ws.Cell("J" + row).Value = i.NhaCungCap.DiaChi;
                                    ws.Cell("K" + row).Value = "";
                                    row++;
                                }

                                //chèn data hóa đơn bán
                                row = 4 + ListHDN.Count;
                                ws.Range("A" + row, "K" + row).Merge();
                                ws.Cell("A" + row).Value = "Hóa đơn nhập";
                                row++;
                                foreach (HoaDonBan i in ListHDB)
                                {
                                    ws.Cell("A" + row).Value = row-4-ListHDN.Count;
                                    ws.Cell("B" + row).Value = i.SoHD;
                                    ws.Cell("C" + row).Value = ((DateTime)i.NgayBan).ToString("dd/MM/yyyy");
                                    ws.Cell("D" + row).Value = i.ChiTietHDBs.Count;
                                    ws.Cell("E" + row).Value = DataProvider.Instance.DB.ChiTietHDBs.Where(t => t.IdHDB == i.SoHD).Sum(t => t.SoLuong);
                                    TinhTien();
                                    ws.Cell("F" + row).Value = "---";
                                    ws.Cell("G" + row).Value = i.ThanhTien;
                                    ws.Cell("H" + row).Value = i.KhachHang.HoTen;
                                    ws.Cell("I" + row).Value = i.KhachHang.Sdt;
                                    ws.Cell("J" + row).Value = i.KhachHang.DiaChi;
                                    ws.Cell("K" + row).Value = "";
                                    row++;
                                }

                                workbook.SaveAs(sfd.FileName);
                                System.Windows.Forms.MessageBox.Show("Xuất file thành công!", "Thông báo");
                            }
                            
                        }
                        catch(Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message, "Thông báo");
                        }
                    }
                }
            });
        }

        #region Method
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

        void demSTT()
        {
            int index = 1;
            if(ListHDN != null)
            {
                foreach (HoaDonNhap i in ListHDN)
                {
                    var temp = index++;
                    i.STT = temp;
                }
            }
                
            if(ListHDB != null)
            {
                index = 1;
                foreach (HoaDonBan i in ListHDB)
                {
                    var temp = index++;
                    i.STT = temp;
                }
            }
            
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

        }

        void LayDSKHvNCC()
        {
            ListKhachHang = new ObservableCollection<object>();
            ListNCC = new ObservableCollection<object>();

            IEnumerable<int?> idkh = ListHDB.Select(i => i.Makh).Distinct();
            IEnumerable<int?> idncc = ListHDN.Select(i => i.IdNCC).Distinct();

            int index = 1;
            foreach(int i in idkh)
            {
                int count = ListHDB.Where(t => t.Makh == i).Count();
                int tt = ListHDB.Where(t => t.Makh == i).Sum(x=>x.ThanhTien);
                var temp = index;
                var kh = ListHDB.Where(t => t.Makh == i).Select(x => new { STT = temp,TenKH = x.KhachHang.HoTen, SLHD = count, TongTT = tt }).Distinct();
                ListKhachHang.Add(kh);
                index++;
            }

            index = 1;
            foreach (int i in idncc)
            {
                int count = ListHDN.Where(t => t.IdNCC == i).Count();
                int tt = ListHDN.Where(t => t.IdNCC == i).Sum(x=>x.ThanhTien);
                var temp = index;
                var ncc = ListHDN.Where(t => t.IdNCC == i).Select(x => new { STT = temp, TenNCC = x.NhaCungCap.Ten, SLHD = count, TongTT = tt}).Distinct();
                ListNCC.Add(ncc);
                index++;
            }
        }

        void LayDSHetHang()
        {
            var listHang = DataProvider.Instance.DB.Hangs.OrderBy(x=>x.SoLuongTon).ToList();
            ListHetHang = new ObservableCollection<object>();
            var ListCTHDN = DataProvider.Instance.DB.ChiTietHDNs.ToList();
            var ListCTHDB = DataProvider.Instance.DB.ChiTietHDBs.ToList();

            int index = 1;
            foreach(Hang h in listHang)
            {
                int idhang = h.Id;
                var listNhap = ListCTHDN.Where(x => x.IdHang == idhang);
                var listBan = ListCTHDB.Where(x => x.IdHang == idhang);
                int slNhap = (int)(listNhap.Count()  == 0 ? 0 : listNhap.Sum(x => x.SoLuong));
                int slBan = (int)(listBan.Count() == 0 ? 0 : listBan.Sum(x => x.SoLuong));
                var temp = index++;
                var hang = ListCTHDN.Where(t => t.IdHang == idhang).Select(x => new { STT = temp, Ten = x.Hang.TenHang, SoLuongNhap = slNhap, SoLuongBan = slBan, ConLai = h.SoLuongTon }).Distinct();
                ListHetHang.Add(hang);
            }
        }

        void TinhHoanVon()
        {
            List<Hang> ListHang = new List<Hang>(DataProvider.Instance.DB.Hangs.Where(t => t.An == 0));
            List<HoaDonNhap> ListNhap = new List<HoaDonNhap>( DataProvider.Instance.DB.HoaDonNhaps);
            List<HoaDonBan> ListXuat= new List<HoaDonBan>(DataProvider.Instance.DB.HoaDonBans);

            List<ChiTietHDB> ListCTXuat;
            List<ChiTietHDN> ListCTNhap;

            double daban = 0, danhap = 0;
            GTKho = 0; GTBan = 0;

            foreach (var i in ListXuat)
            {
                ListCTXuat = new List<ChiTietHDB>(DataProvider.Instance.DB.ChiTietHDBs.Where(t => t.IdHDB == i.SoHD));
                tinhThanhTienHDB(ListCTXuat);
                i.ThanhTien = ListCTXuat.Sum(x => x.ThanhTien);
                daban += i.ThanhTien;
            }

            foreach (var i in ListNhap)
            {
                ListCTNhap = new List<ChiTietHDN>(DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.IdHDN == i.SoHD));
                tinhThanhTienHDN(ListCTNhap);
                i.ThanhTien = ListCTNhap.Sum(x => x.ThanhTien);
                danhap += i.ThanhTien;
            }

            foreach(Hang i in ListHang)
            {
                GTKho += (int)(i.GiaBan * i.SoLuongTon);

                if (i.ChiTietHDNs.Count==0)
                    continue;

                var gianhap = DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.IdHang == i.Id).OrderByDescending(t => t.Id).Select(t=>t.DonGiaNhap).First();
                
                GTBan += (int)(i.SoLuongTon * (int)gianhap);
            }
            if(daban <= danhap)
            {
                Status = "Hoàn vốn: ";
                GTStatus = String.Format("{0:0.00}", daban/danhap * 100) + "%";
            }
            else
            {
                Status = "Lợi nhuận: ";
                GTStatus = String.Format("{0:#,#}", daban - danhap) + "VNĐ";
            }
             
        }
        #endregion
    }
}
