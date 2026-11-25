using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using WordCollector.Client.Pages.Shared;
using WordCollector.Shared;

namespace WordCollector.Client.Pages;

public partial class Home
{
    private List<WordEntry> words = new List<WordEntry>();

    private Modal _modal = default!;

    [Inject] private HttpClient _http { get; set; } = default!;
    private WordType? selectedFilter = null;

    private readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    protected override async Task OnInitializedAsync() => await LoadWords();

    private async Task LoadWords()
    {
        var url = selectedFilter.HasValue ? $"api/words?type={selectedFilter}" : "api/words";
        try
        {
            words = await _http.GetFromJsonAsync<List<WordEntry>>(url, jsonOptions);
        }
        catch (Exception ex)
        {

            throw;
        }
        
    }

    private async Task FilterByType(WordType? type)
    {
        selectedFilter = type;
        await LoadWords();
    }

    private async Task HandleUserSaved(WordEntry newWord)
    {
        // Persist newUser to your API or database
        var response = await _http.PostAsJsonAsync("api/words", newWord, jsonOptions);
        if (response.IsSuccessStatusCode)
        {
            newWord = new WordEntry();
            await LoadWords();
        }
        await Task.CompletedTask;
    }





}
