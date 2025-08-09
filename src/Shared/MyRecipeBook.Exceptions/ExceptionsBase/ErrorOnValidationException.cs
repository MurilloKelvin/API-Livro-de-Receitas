namespace MyRecipeBook.Exceptions.ExceptionsBase;

public class ErrorOnValidationException : MyRecipeBookException
{
    
    //array de mensagens de erro com struct 
    public IList<string> ErrorsMessages { get; set; }

    public ErrorOnValidationException(IList<string> errors)
    {
        ErrorsMessages = errors; // erro do array = a esse erro
    }
}