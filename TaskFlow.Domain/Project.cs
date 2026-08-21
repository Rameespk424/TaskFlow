
namespace TaskFlow.Domain
{
    public class Project : BaseEntity
    {

        public string? Description { get; set; }
        public string? Name { get; private set; }
        public int OwnerId { get; private set; }


        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Name is required", nameof(newName));
            }

            Name = newName;

        }


    }

   


}
