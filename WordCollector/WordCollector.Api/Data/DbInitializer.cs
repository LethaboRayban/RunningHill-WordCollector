using Microsoft.EntityFrameworkCore;
using WordCollector.Shared;

namespace WordCollector.Api.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {

        context.Database.Migrate();

        if (context.Words.Any())
        {
            return;
        }

        // 3. Add your demo records
        var demoWords = new WordEntry[]
        {
                new WordEntry { Id = 1, Word = "evaluates", Type = WordType.Noun},
                new WordEntry { Id = 2, Word = "play", Type = WordType.Verb}
        };

        context.Words.AddRange(demoWords);
        context.SaveChanges();
    }
}
