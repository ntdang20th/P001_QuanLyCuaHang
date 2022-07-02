namespace P001_QuanLyCuaHang.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using ViewModel;

    public partial class KhachHang : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public KhachHang()
        {
            this.HoaDonBans = new HashSet<HoaDonBan>();
        }

        private int _MaKh;
        private string _HoTen;
        private string _GioiTinh;
        private Nullable<System.DateTime> _NamSinh;
        private string _DiaChi;
        private string _Sdt;
        private Nullable<int> _An;


        public int MaKh { get => _MaKh; set { _MaKh = value; OnPropertyChanged(); } }
        public string HoTen { get => _HoTen; set { _HoTen = value; OnPropertyChanged(); } }
        public string GioiTinh { get => _GioiTinh; set { _GioiTinh = value; OnPropertyChanged(); } }
        public Nullable<System.DateTime> NamSinh { get => _NamSinh; set { _NamSinh = value; OnPropertyChanged(); } }
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        public string Sdt { get => _Sdt; set { _Sdt = value; OnPropertyChanged(); } }

        public Nullable<int> An { get => _An; set { _An = value; OnPropertyChanged(); } }
        public int TongHD { get; set; }
        public int TongTien { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonBan> HoaDonBans { get; set; }
    }
}

