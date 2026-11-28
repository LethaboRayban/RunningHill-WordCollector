namespace WordCollector.Shared
{
    public enum WordType
    {
        Noun,
        Verb,
        Adjective,
        Adverb,
        Pronoun,
        Preposition,
        Interjection,
        Conjunction,
        Determiner
    }

    public class WordTypeEntry
    {
        public WordType Id { get; set; }
        public string Name { get; set; } = string.Empty; 
    }
}
