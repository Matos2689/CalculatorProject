namespace CalculatorProject.Contracts
{
    public interface IRepository
    {
        public List<MathLogItem> Memory { get; }
        public void Save(string filePath);
        public void Load(string filePath);
    }
}
