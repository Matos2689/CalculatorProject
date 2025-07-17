using CalculatorProject.Contracts;
using System.Xml;
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

            var entities = Memory.Select(item => item.ToEntity()).ToList();

            // Create an XmlSerializer for the MathLogEntity type
            var serializer = new XmlSerializer (typeof(List<MathLogEntity>));

            // Serialize the list of entities to XML and save it to a file
            using (var writer = XmlWriter.Create(filePath))
            {
                serializer.Serialize(writer, entities);
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
