namespace Enterprise.Example.Domain
{
    public class Notification
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public string EmailTemplate { get; set; }
        public bool IsActive { get; set; }
    }
}
