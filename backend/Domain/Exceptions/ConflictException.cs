namespace Domain.Exceptions;

public class ConflictException : Exception
{
    public ConflictException( string exceptionMessage ) : base( exceptionMessage ) { }
}
