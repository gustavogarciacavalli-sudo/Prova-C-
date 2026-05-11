using System;

namespace GustavoHenriqueCavalliGarcia.Models;

public class Livro
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Autor { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public bool Emprestado { get; set; } = false;
}
