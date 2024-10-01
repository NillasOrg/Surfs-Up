using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


namespace Surfs_Up.Models
{
    public class Surfboard
    {
        public int? Id {get; set;}
        public string? Name {get; set;}
        public string? Description {get; set;}
        public double? Length {get; set;}
        public double? Width {get; set;}
        public double? Thickness {get; set;}
        public double? Volume {get; set;}
        public BOARDTYPE Type {get; set;}
        public double Price {get; set;}
        public string? Equipment {get; set;}
        public string? ImagePath {get; set;}
        public List<Booking>? Bookings { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }

    public enum BOARDTYPE
    {
        Shortboard,
        Funboard,
        Fish,
        Longboard,
        SUP
    }
}