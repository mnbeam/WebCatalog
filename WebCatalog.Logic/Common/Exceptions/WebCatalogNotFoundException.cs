namespace WebCatalog.Logic.Common.Exceptions;

public class WebCatalogNotFoundException : Exception
{
    public WebCatalogNotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) not found")
    {
    }
}