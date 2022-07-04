
namespace P001_QuanLyCuaHang.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using ViewModel;
    public partial class Hang : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Hang()
        {
            this.ChiTietHDBs = new HashSet<ChiTietHDB>();
            this.ChiTietHDNs = new HashSet<ChiTietHDN>();
        }

        private string _TenHang;
        private Nullable<int> _GiaBan;
        private Nullable<int> _SoLuongTon;
        private Nullable<int> _IdDVT;
        private Nullable<int> _An;
        public int  GiaNhap { get; set; }


        public int Id { get; set; }
        public string TenHang { get => _TenHang; set { _TenHang = value; OnPropertyChanged(); } }
        public Nullable<int> GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }
        public Nullable<int> SoLuongTon { get => _SoLuongTon; set { _SoLuongTon = value; OnPropertyChanged(); } }
        public Nullable<int> IdDVT { get => _IdDVT; set { _IdDVT = value; OnPropertyChanged(); } }
        public Nullable<int> An { get => _An; set { _An = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHDB> ChiTietHDBs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietHDN> ChiTietHDNs { get; set; }
        public virtual DonViTinh DonViTinh { get; set; }
    }
}

