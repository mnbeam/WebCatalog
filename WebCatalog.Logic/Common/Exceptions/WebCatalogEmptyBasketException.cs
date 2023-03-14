namespace WebCatalog.Logic.Common.Exceptions;

public class WebCatalogEmptyBasketException : Exception
{
    public WebCatalogEmptyBasketException() : base("Basket can not be empty")
    {
    }
}