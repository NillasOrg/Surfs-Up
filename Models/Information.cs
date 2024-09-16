using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;


namespace Surfs_Up.Models 
{
    public class Information
    {
        [Key]
        public int InformationId {get; set;}
        public string Name {get; set;}
        public string Description {get; set;}
    }
}