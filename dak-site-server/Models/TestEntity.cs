using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DakSite.Models
{
    [Table("test")]
    public class TestEntity
    {
        [Key]
        [Comment("主键 ID")]
        public int Id { get; set; }

        [Comment("名称")]
        public string Name { get; set; } = string.Empty;
    }
}
