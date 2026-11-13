namespace Domain.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException( string exceptionMessage ) : base( exceptionMessage ) { }
}
