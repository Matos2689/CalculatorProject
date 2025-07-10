using CalculatorMethods.Contracts;
using System.Xml.Serialization;

namespace CalculatorMethods.Persistance
{
    public class XmlRepositoryManager : IRepository
    {
        public void Save(List<MathLogItem> logs, string filePath)
        {
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var entities = logs.Select(logs => logs.ToEntity()).ToList();

            // Create an XmlSerializer for the MathLogEntity type
            var serializer = new XmlSerializer
                (typeof(List<MathLogEntity>));

            // Serialize the list of entities to XML and save it to a file
            using (var stream = File.Create(filePath))
            {
                serializer.Serialize(stream, entities);
            }
        }
        public List<MathLogItem> Load(string filePath)
        {
            if (!File.Exists(filePath))
                return new List<MathLogItem>();

            var serializer = new XmlSerializer
                (typeof(List<MathLogEntity>));

            using (var stream = File.OpenRead(filePath))
            {
                // Deserialize the XML file into a list of MathLogEntity
                var entities = (serializer.Deserialize(stream) as List<MathLogEntity>)
               ?? new List<MathLogEntity>();

                return entities.Select(entity => entity.FromEntity()).ToList();
            }
        }
    }
}
