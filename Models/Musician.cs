namespace CrazyMusicians.Models
{
    public class Musician
    {
        private static int _nextId = 1;

        public Musician()
        {
            Id = _nextId++;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Job { get; set; }
        public string FunFeature { get; set; }
        public bool IsDeleted { get; set; }
    }
}
