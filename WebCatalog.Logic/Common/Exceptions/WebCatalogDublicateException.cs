namespace WebCatalog.Logic.Common.Exceptions;

public class WebCatalogDublicateException : Exception
{
    public WebCatalogDublicateException(string name, string property, object key)
        : base($"Entity {name} with \"{property}\" ({key}) is already exist")
    {
    }
}