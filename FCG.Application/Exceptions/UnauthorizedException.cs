namespace FCG.Application.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message = "Acesso não autorizado.") : base(message) { }
    }
}