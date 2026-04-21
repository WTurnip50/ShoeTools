using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeTools.Core.Entities;


public class AppUsers : EntityBase
{
    public string username { get; set; }
    public string password { get; set; }
}