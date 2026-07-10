using Microsoft.EntityFrameworkCore;

namespace ThinkingHome.Plugins.Database;

public class HomeDbContext(DbModelBuilderDelegate[] inits, DbContextOptions options) : DbContext(options) {
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if (inits == null) return;

        foreach (var action in inits) {
            action(modelBuilder);
        }
    }
}