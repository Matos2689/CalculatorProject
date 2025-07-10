using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CalculatorMethods.Contracts;
using System.IO;

namespace CalculatorMethods.Persistance
{
    public class XmlRepositoryManager : IRepository
    {
        private readonly string FilePath;
        public XmlRepositoryManager(string filePath = "SaveMathlog.xml")
        {
            FilePath = filePath;
        }
        public void Save(List<MathLogItem> logs)
        {
            var entities = logs.Select(logs => logs.ToEntity()).ToList();

            // Create an XmlSerializer for the MathLogEntity type
            var serializer = new System.Xml.Serialization.XmlSerializer
                (typeof(List<MathLogEntity>));

            // Serialize the list of entities to XML and save it to a file
            using (var stream = File.Create(FilePath))
            {
                serializer.Serialize(stream, entities);
            }
        }
        public List<MathLogItem> Load()
        {
            if (!File.Exists(FilePath))
                return new List<MathLogItem>();            
            
            var serializer = new System.Xml.Serialization.XmlSerializer
                (typeof(List<MathLogEntity>));

            using (var stream = File.OpenRead(FilePath))
            {
                // Deserialize the XML file into a list of MathLogEntity
                var entities = (serializer.Deserialize(stream) as List<MathLogEntity>)
               ?? new List<MathLogEntity>();

                return entities.Select(entity => entity.FromEntity()).ToList();
            }
        }        
    }
}
