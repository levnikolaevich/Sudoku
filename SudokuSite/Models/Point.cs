//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SudokuSite.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Point
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int IdGame { get; set; }
        public int Value { get; set; }
        public bool Visibled { get; set; }
        public bool Checked { get; set; }
        public bool Guessed { get; set; }
    
        public virtual Game Game { get; set; }
    }
}
