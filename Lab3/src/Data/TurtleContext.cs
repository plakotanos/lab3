using System.Data.Common;
using Lab3.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Lab3.Data;

public class TurtleContext(DbConnection connection) : DbContext {
    public DbSet<Turtle> Turtles { get; set; } = null!;
    public DbSet<Step> Steps { get; set; } = null!;
    public DbSet<Figure> Figures { get; set; } = null!;

    private static DbConnection? _connection = null;

    private static DbConnection DefaultConnection()
    {
        if (_connection != null)
        {
            return _connection;
        }
        
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);

        path = Path.Join(path, "turtle.db");

        _connection = new SqliteConnection($"Data Source={path}");

        return _connection;
    }

    public TurtleContext() : this(DefaultConnection())
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Turtle>().Navigation(e => e.Path).AutoInclude();
        modelBuilder.Entity<Turtle>().Navigation(e => e.Steps).AutoInclude();
        modelBuilder.Entity<Turtle>().Navigation(e => e.Figures).AutoInclude();
        modelBuilder.Entity<Figure>().Navigation(e => e.Steps).AutoInclude();
    }
}