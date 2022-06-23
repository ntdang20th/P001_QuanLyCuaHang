namespace P001_QuanLyCuaHang.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using ViewModel;

    public partial class NhaCungCap : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NhaCungCap()
        {
            this.HoaDonNhaps = new HashSet<HoaDonNhap>();
        }

        private string _Ten;
        private string _DiaChi;
        private string _Sdt;
        private string _Email;
        private Nullable<int> _An;

        public int Id { get; set; }
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        public string Sdt { get => _Sdt; set { _Sdt = value; OnPropertyChanged(); } }
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        public Nullable<int> An { get => _An; set { _An = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoaDonNhap> HoaDonNhaps { get; set; }
    }
}

