namespace CalculatorMethods.Contracts
{
    public interface IRepository
    {
        public void Save(List<MathLogItem> logs, string filePath);
        public List<MathLogItem> Load(string filePath);
    }
}
