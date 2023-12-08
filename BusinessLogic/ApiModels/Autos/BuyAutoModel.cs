using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.ApiModels.Autos
{
    public class BuyAutoModel
    {
        public int Id { get; set; }
        public string Mark { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Image { get; set; }
        public int ColorId { get; set; }
        public string ColorColorName { get; set; }
    }
}
