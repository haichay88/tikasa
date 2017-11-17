using Tikasa.Entities;

namespace Tikasa.Data
{
    public interface IDatabaseFactory
    {
        tikasaEntities Get();
    }
}