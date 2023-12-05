namespace BusinessLogic.ApiModels.Autos
{
    public class CreateAutoModel
    {
        public string Mark { get; set; }
        public string Model { get; set; }
        public double Price { get; set; }
        public int Year { get; set; }
        public string Image { get; set; }
        public int ColorId { get; set; }
        public string About { get; set; }

    }
}
