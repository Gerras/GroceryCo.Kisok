namespace GroceryCo.KioskSystem.DAL.DALDefinitions
{
    public interface IConfigurationManager
    {
        string GetFilePathToPriceCatalog();
        string GetFilePathToPromotionCatalog();
    }
}
