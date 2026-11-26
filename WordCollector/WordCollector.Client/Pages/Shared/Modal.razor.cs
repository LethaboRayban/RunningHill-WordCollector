using Microsoft.AspNetCore.Components;
using WordCollector.Shared;

namespace WordCollector.Client.Pages.Shared;

public partial class Modal
{
    [Parameter] public bool IsVisible { get; set; }
    [Parameter] public string Title { get; set; } = "Modal Title";
    [Parameter] public string ConfirmButtonName { get; set; } = "Confirm";
    [Parameter] public Type? FormComponentType { get; set; }
    [Parameter] public Dictionary<string, object>? FormParameters { get; set; }
    [Parameter] public EventCallback<bool> OnClose { get; set; }

    private DynamicComponent? _ref;

    private async Task SaveAsync()
    {
        if (_ref?.Instance is IModalForm formModal)
        {
            if (await formModal.SubmitAsync())
            {
                await Close();
            }
        }
    }

    private Task Close() => OnClose.InvokeAsync();
}
