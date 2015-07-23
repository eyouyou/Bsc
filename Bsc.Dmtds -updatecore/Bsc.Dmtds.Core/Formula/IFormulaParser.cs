namespace Bsc.Dmtds.Core.Formula
{
    public interface IFormulaParser
    {
        string Populate(string formula, IValueProvider valueProvider);

    }
}