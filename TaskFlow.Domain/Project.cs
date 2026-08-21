
namespace TaskFlow.Domain
{
    public class Project : BaseEntity
    {

        public string? Description { get; set; }
        public string Name { get; private set; }
        public int OwnerId { get; private set; }


        public void Rename(string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentException("Name is required", nameof(newName));
            }

            Name = newName;

        }

        public Project(string name, int ownerId)
        {
            if(ownerId <= 0)
            {
                throw new ArgumentException("OwnerId must be a positive integer", nameof(ownerId));
            }

            OwnerId = ownerId;

            Rename(name);


        }


    }

   


}
