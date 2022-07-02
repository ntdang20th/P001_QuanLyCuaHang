using P001_QuanLyCuaHang.MVVM.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P001_QuanLyCuaHang.MVVM.ViewModel
{
    public class MainViewModel:BaseViewModel
    {
        public ICommand KhoCommand { get; set; }
        public ICommand NhapHangCommand { get; set; }
        public ICommand BanHangCommand { get; set; }
        public ICommand KhachHangCommand { get; set; }
        public ICommand NhaCUngCapCommand { get; set; }
        public ICommand ThongKeCommand { get; set; }
        public ICommand DVTCommand { get; set; }



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

            KhoCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CurrentView = KhoVM;
            });

            DVTCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CurrentView = DVTVM;
            });

            NhapHangCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CurrentView = NhapHangVM;
            });

            BanHangCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CurrentView = BanHangVM;
            });

            KhachHangCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CurrentView = KhachHangVM;
            });

            NhaCUngCapCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CurrentView = NhaCungCapVM;
            });

            ThongKeCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                CurrentView = ThongKeVM;
            });
        }
    }
}
