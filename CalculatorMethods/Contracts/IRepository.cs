namespace CalculatorMethods.Contracts
{
    public interface IRepository
    {
        public void Save(List<MathLogItem> logs);
        public List<MathLogItem> Load();
    }
}
