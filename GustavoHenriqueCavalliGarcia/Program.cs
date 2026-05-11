using GustavoHenriqueCavalliGarcia.Data;
using GustavoHenriqueCavalliGarcia.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();
var app = builder.Build();

app.MapPost("/api/livro/cadastrar", (Livro livro, AppDataContext ctx) =>
{
    var existente = ctx.Livros.FirstOrDefault(l => l.Nome == livro.Nome);
    if (existente != null)
    {
        return Results.BadRequest("Livro já existe.");
    }
    ctx.Livros.Add(livro);
    ctx.SaveChanges();
    return Results.Created("", livro);
});

app.MapGet("/api/livro/listar", (AppDataContext ctx) =>
{
    var livros = ctx.Livros.ToList();
    if (!livros.Any())
    {
        return Results.NotFound();
    }
    return Results.Ok(livros);
});

app.MapGet("/api/livro/buscar/{nome}", (string nome, AppDataContext ctx) =>
{
    var livro = ctx.Livros.FirstOrDefault(l => l.Nome == nome);
    if (livro == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(livro);
});

app.MapPut("/api/livro/emprestar", (Livro req, AppDataContext ctx) =>
{
    var livro = ctx.Livros.Find(req.Id);
    if (livro == null) return Results.NotFound();
    
    if (livro.Emprestado)
    {
        return Results.BadRequest("Livro já emprestado.");
    }
    livro.Emprestado = true;
    ctx.SaveChanges();
    return Results.Ok(livro);
});

app.MapPut("/api/livro/devolver", (Livro req, AppDataContext ctx) =>
{
    var livro = ctx.Livros.Find(req.Id);
    if (livro == null) return Results.NotFound();

    if (!livro.Emprestado)
    {
        return Results.BadRequest("Livro não está emprestado.");
    }
    livro.Emprestado = false;
    ctx.SaveChanges();
    return Results.Ok(livro);
});

app.MapGet("/api/livro/disponiveis", (AppDataContext ctx) =>
{
    return Results.Ok(ctx.Livros.Where(l => !l.Emprestado).ToList());
});

app.MapGet("/api/livro/emprestados", (AppDataContext ctx) =>
{
    return Results.Ok(ctx.Livros.Where(l => l.Emprestado).ToList());
});

app.Run();
