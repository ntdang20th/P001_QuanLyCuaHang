namespace P001_QuanLyCuaHang.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    using ViewModel;

    public partial class DonViTinh : BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DonViTinh()
        {
            this.Hangs = new HashSet<Hang>();
        }

        private string _TenDonViTinh;
        public int Id { get; set; }
        public string TenDonViTinh { get => _TenDonViTinh; set { _TenDonViTinh = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hang> Hangs { get; set; }
    }
}
