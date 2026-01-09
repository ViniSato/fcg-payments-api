using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects
{
    public class Senha
    {
        public string Valor { get; }

        public Senha(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("Senha não pode ser vazia");

            if (valor.Length < 8 ||
                !Regex.IsMatch(valor, @"[A-Z]") ||
                !Regex.IsMatch(valor, @"[a-z]") ||
                !Regex.IsMatch(valor, @"[0-9]") ||
                !Regex.IsMatch(valor, @"[\W_]"))
                throw new ArgumentException("Senha deve ter no mínimo 8 caracteres, incluindo letras maiúsculas, minúsculas, números e símbolos");

            Valor = valor;
        }

        public override string ToString() => Valor;
    }
}