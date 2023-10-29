using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProject.Data.Entitys;

namespace BusinessLogic.Dtos
{
    public class AutoDtos
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        public string Image { get; set; }      
        public int ColorId { get; set; }
        public string ColorColorName { get; set; }
    }
}
