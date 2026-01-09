using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects
{
    public class Email
    {
        public string Endereco { get; }

        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ArgumentException("Email não pode ser vazio");

            if (!Regex.IsMatch(endereco, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Formato de email inválido");

            Endereco = endereco;
        }

        public override string ToString() => Endereco;
    }
}