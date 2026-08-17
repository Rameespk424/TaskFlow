Console.WriteLine("Playground running");
public class TaskItem {

    public int Id { get; private set; }
    public string Name { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public TaskItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException("Title is required.", nameof(title));
        }

        Name = title;
        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateTitle(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
        {
            throw new ArgumentException("Title is Requored");
        }

        Name = newTitle;
}

}

