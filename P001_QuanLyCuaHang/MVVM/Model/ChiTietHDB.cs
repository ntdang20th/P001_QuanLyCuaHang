//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace P001_QuanLyCuaHang.MVVM.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietHDB
    {
        public int Id { get; set; }
        public Nullable<int> IdHDB { get; set; }
        public Nullable<int> IdHang { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<int> DonGiaBan { get; set; }
        public Nullable<double> KhuyenMai { get; set; }
    
        public virtual Hang Hang { get; set; }
        public virtual HoaDonBan HoaDonBan { get; set; }
    }
}