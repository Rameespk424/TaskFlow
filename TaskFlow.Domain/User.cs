

namespace TaskFlow.Domain
{
    public class User : BaseEntity
    {

        public string Name { get; private set; }
        public string Email { get; private set; }


        public void UpdateName(string name)
        {


            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name is required", nameof(name));
            }


            Name = name;

        }


        public void UpdateEmail(string newEmail)
        {

            if (string.IsNullOrWhiteSpace(newEmail) || !newEmail.Contains("@"))
            {
                throw new ArgumentException("Valid email is required", nameof(newEmail));
            }

            Email = newEmail;


        }


     public User (string name, string email)
        {
            UpdateName(name);
            UpdateEmail(email);
        }

    }   
  

    }
