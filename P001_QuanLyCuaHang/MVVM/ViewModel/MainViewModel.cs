using P001_QuanLyCuaHang.MVVM.Model;
using P001_QuanLyCuaHang.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace P001_QuanLyCuaHang.MVVM.ViewModel
{
    
    public class MainViewModel:BaseViewModel
    {
        bool cancel = false;
        public static bool chualuu = false;

        public ICommand KhoCommand { get; set; }
        public ICommand NhapHangCommand { get; set; }
        public ICommand BanHangCommand { get; set; }
        public ICommand KhachHangCommand { get; set; }
        public ICommand NhaCUngCapCommand { get; set; }
        public ICommand ThongKeCommand { get; set; }
        public ICommand DVTCommand { get; set; }
        public ICommand CheckHDNCommand { get; set; }
        public ICommand CheckHDBCommand { get; set; }



        public HangViewModel KhoVM { get; set; }
        public HoaDonNhapViewModel NhapHangVM { get; set; }
        public HoaDonBanViewModel BanHangVM { get; set; }
        public KhachHangViewModel KhachHangVM { get; set; }
        public NhaCungCapViewModel NhaCungCapVM { get; set; }
        public ThongKeViewModel ThongKeVM { get; set; }
        public DonViTinhViewModel DVTVM { get; set; }



        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            KhoVM = new HangViewModel();
            NhapHangVM = new HoaDonNhapViewModel();
            BanHangVM = new HoaDonBanViewModel();
            KhachHangVM = new KhachHangViewModel();
            NhaCungCapVM = new NhaCungCapViewModel();
            ThongKeVM = new ThongKeViewModel();
            DVTVM = new DonViTinhViewModel();

            CurrentView = ThongKeVM;
            KhoCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (cancel)
                    return;
                CurrentView = KhoVM;
                cancel = false;
            });

            DVTCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (cancel)
                    return;
                CurrentView = DVTVM;
                cancel = false;
            });

            NhapHangCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CurrentView = NhapHangVM;
                cancel = false;
            });

            BanHangCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CurrentView = BanHangVM;
                cancel = false;
            });

            KhachHangCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (cancel)
                    return;
                CurrentView = KhachHangVM;
                cancel = false;
            });

            NhaCUngCapCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (cancel)
                    return;
                CurrentView = NhaCungCapVM;
                cancel = false;
            });

            ThongKeCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                if (cancel)
                    return;
                CurrentView = ThongKeVM;
                cancel = false;
            });

            CheckHDNCommand = new RelayCommand<System.Windows.Controls.RadioButton>((p) => { return true; }, (p) => {
                if (chualuu == false)
                    return;

                DialogResult result = MessageBox.Show("Bạn có muốn lưu hóa đơn đang nhập ??", "Thông báo", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    //lưu
                    cancel = false;
                }
                else if (result == DialogResult.No)
                {
                    //hủy
                    var lastBill = DataProvider.Instance.DB.HoaDonNhaps.OrderByDescending(t => t.NgayNhap).First();
                    var ListCTHDN = DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.IdHDN == lastBill.SoHD);

                    //+ trong kho
                    foreach (ChiTietHDN i in ListCTHDN)
                    {
                        var x = DataProvider.Instance.DB.Hangs.Where(t => t.Id == i.IdHang).SingleOrDefault();
                        x.SoLuongTon -= i.SoLuong;
                        DataProvider.Instance.DB.SaveChanges();
                    }

                    //hoan tac listcthd
                    DataProvider.Instance.DB.ChiTietHDNs.RemoveRange(DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.IdHDN == lastBill.SoHD));
                    DataProvider.Instance.DB.SaveChanges();

                    //xoa hoa don
                    DataProvider.Instance.DB.HoaDonNhaps.Remove(lastBill);
                    DataProvider.Instance.DB.SaveChanges();

                    MessageBox.Show("Đã hoàn tác!");

                    cancel = false;
                }
                else
                {
                    //ở lại
                    cancel = true;
                    p.IsChecked = true;
                }
            });

            CheckHDBCommand = new RelayCommand<System.Windows.Controls.RadioButton>((p) => { return true; }, (p) => {
                if (chualuu == false)
                    return;

                DialogResult result = MessageBox.Show("Bạn có muốn lưu hóa đơn đang nhập ??", "Thông báo", MessageBoxButtons.YesNoCancel);
                if (result == DialogResult.Yes)
                {
                    //lưu
                    cancel = false;
                }
                else if (result == DialogResult.No)
                {
                    //hủy
                    var lastBill = DataProvider.Instance.DB.HoaDonBans.OrderByDescending(t => t.NgayBan).First();
                    var ListCTHDB = DataProvider.Instance.DB.ChiTietHDBs.Where(t => t.IdHDB == lastBill.SoHD);

                    //+ trong kho
                    foreach (ChiTietHDB i in ListCTHDB)
                    {
                        var x = DataProvider.Instance.DB.Hangs.Where(t => t.Id == i.IdHang).SingleOrDefault();
                        x.SoLuongTon += i.SoLuong;
                        DataProvider.Instance.DB.SaveChanges();
                    }

                    //hoan tac listcthd
                    DataProvider.Instance.DB.ChiTietHDNs.RemoveRange(DataProvider.Instance.DB.ChiTietHDNs.Where(t => t.IdHDN == lastBill.SoHD));
                    DataProvider.Instance.DB.SaveChanges();

                    //xoa hoa don
                    DataProvider.Instance.DB.HoaDonBans.Remove(lastBill);
                    DataProvider.Instance.DB.SaveChanges();

                    MessageBox.Show("Đã hoàn tác!");

                    cancel = false;
                }
                else
                {
                    //ở lại
                    cancel = true;
                    p.IsChecked = true;
                }
            });
        }
    }
}
