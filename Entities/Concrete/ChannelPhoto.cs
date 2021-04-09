using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Entities.Concrete
{
    public class ChannelPhoto : IEntity
    {
        [Key]

        public int Id { get; set; }
        public int ChannelId { get; set; }
        public string ImagePath { get; set; }
        public DateTime Date { get; set; }
    }
}
