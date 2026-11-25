using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Text.Json;
using System.Text.Json.Serialization;
using WordCollector.Client.Pages.Shared;
using WordCollector.Shared;

namespace WordCollector.Client.Pages;

public partial class AddWord
{
    [CascadingParameter] private Modal? ParentModal { get; set; }
    [Parameter] public EventCallback<WordEntry> OnValidSave { get; set; }

    private WordEntry _newWord = new();
    private EditContext _editContext = default!;

    private readonly JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    protected override void OnInitialized()
    {
        _editContext = new EditContext(_newWord);
        ParentModal?.RegisterForm((IModalForm)this);
    }

    public async Task<bool> SubmitAsync()
    {
        // Trigger DataAnnotations validation
        if (!_editContext.Validate())
        {
            return false;
        }
        await OnValidSave.InvokeAsync(_newWord);
        return true;
    }
}
