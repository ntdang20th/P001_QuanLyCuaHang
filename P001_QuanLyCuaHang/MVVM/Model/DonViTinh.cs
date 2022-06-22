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

        private string _TenTinhTrang;
        public int Id { get; set; }
        public string TenTinhTrang { get => _TenTinhTrang; set { _TenTinhTrang = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Hang> Hangs { get; set; }
    }
}
