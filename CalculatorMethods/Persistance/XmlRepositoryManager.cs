using CalculatorProject.Contracts;
using System.Xml.Serialization;

namespace CalculatorProject.Persistance
{
    public class XmlRepositoryManager : IRepository
    {
        public List<MathLogItem> Memory { get; } = new List<MathLogItem>();
        public void Save(string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var entities = Memory.Select(logs => logs.ToEntity()).ToList();

            // Create an XmlSerializer for the MathLogEntity type
            var serializer = new XmlSerializer (typeof(List<MathLogEntity>));

            // Serialize the list of entities to XML and save it to a file
            using (var stream = File.Create(filePath))
            {
                serializer.Serialize(stream, entities);
            }
        }
        public void Load(string filePath)
        {
            if (!File.Exists(filePath))
                Memory.AddRange(new List<MathLogItem>());

            var serializer = new XmlSerializer(typeof(List<MathLogEntity>));

            using (var stream = File.OpenRead(filePath))
            {
                // Deserialize the XML file into a list of MathLogEntity
                var entities = (serializer.Deserialize(stream) as List<MathLogEntity>)
               ?? new List<MathLogEntity>();

                Memory.AddRange(entities.Select(entity => entity.FromEntity()).ToList());
            }
        }
    }
}
