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

        var words = new WordEntry[]
        {
                new WordEntry { Word = "evaluates", Type = WordType.Noun},
                new WordEntry { Word = "play", Type = WordType.Verb}
        };

        context.Words.AddRange(words);
        context.SaveChanges();
    }
}
