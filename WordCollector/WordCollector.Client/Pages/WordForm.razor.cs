using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using WordCollector.Client.Pages.Shared;
using WordCollector.Shared;

namespace WordCollector.Client.Pages;

public partial class WordForm
{
    [Parameter] public WordEntry WordEntry { get; set; } = new();
    [Parameter] public EventCallback<WordEntry> OnValidSave { get; set; }

    private EditContext _editContext = default!;

    //private readonly JsonSerializerOptions jsonOptions = new()
    //{
    //    PropertyNameCaseInsensitive = true,
    //    Converters = { new JsonStringEnumConverter() }
    //};

    protected override void OnParametersSet()
    {
        _editContext = new EditContext(WordEntry);
    }

    public async Task<bool> SubmitAsync()
    {
        // Trigger DataAnnotations validation
        if (!_editContext.Validate())
        {
            return false;
        }
        await OnValidSave.InvokeAsync(WordEntry);
        return true;
    }
}
