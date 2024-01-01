using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitra.Domain
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? CreateByText { get; set; }
        public DateTime Created {  get; set; }
    }
}
