namespace App.Application.Models
{
    public class Responsible
    {
        public Responsible(int id, string name, string document)
        {
            this.Id = id;
            this.Name = name;
            this.Document = document;

        }
        
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Document { get; private set; }
    }
}