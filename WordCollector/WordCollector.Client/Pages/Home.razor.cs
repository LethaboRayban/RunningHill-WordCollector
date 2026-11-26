using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using WordCollector.Shared;

namespace WordCollector.Client.Pages;

public partial class Home
{
    [Inject] private HttpClient _http { get; set; } = default!;

    private List<WordEntry> words = new List<WordEntry>();
    private bool isModalVisible;
    private string modalTitle = string.Empty;
    private Dictionary<string, object>? formParameters;
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
        catch (HttpRequestException ex)
        {
             //add logging 
             //api request failer
        }

        catch (Exception ex)
        {
            //add logging
            //other unexpected error
        }
    }

    private void OpenWordEntryModal(WordEntry? wordToEdit)
    {
        bool isNew = wordToEdit is null;
        modalTitle = isNew ? "Add New Word" : "Edit Word";

        
        var model = isNew
            ? new WordEntry()
            : new WordEntry
            {
                Id = wordToEdit!.Id,
                Word = wordToEdit.Word,
                Type = wordToEdit.Type
            };

        formParameters = new Dictionary<string, object>
        {
            { nameof(WordForm.WordEntry), model },
            { nameof(WordForm.OnValidSave), EventCallback.Factory.Create<WordEntry>(this, HandlWordEntrySaved) }
        };

        isModalVisible = true;
    }

    private async Task FilterByType(WordType? type)
    {
        selectedFilter = type;
        await LoadWords();
    }

    private async Task HandlWordEntrySaved(WordEntry wordEntry)
    {
        if(wordEntry.Id == 0)
        {
            // Persist newUser to your API or database
            var response = await _http.PostAsJsonAsync("api/words", wordEntry, jsonOptions);


            if (response.IsSuccessStatusCode)
            {
                //newWord = new WordEntry();
                await LoadWords();
            }
        }
        else
        {

        }
      
        await Task.CompletedTask;
    }





}
