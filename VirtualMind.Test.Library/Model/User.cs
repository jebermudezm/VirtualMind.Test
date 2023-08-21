using System.ComponentModel.DataAnnotations;

namespace VirtualMind.Test.Library.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Transaction> Transaction { get; set; }
    }
}
