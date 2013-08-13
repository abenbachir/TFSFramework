
namespace TeamFoundation.Common.Entities
{

    public class Change
    {
        public string Collection { get; set; }

        public int Changeset { get; set; }

        public string ChangeType { get; set; }

        public string Path { get; set; }

        public string Type { get; set; }
    }
}
