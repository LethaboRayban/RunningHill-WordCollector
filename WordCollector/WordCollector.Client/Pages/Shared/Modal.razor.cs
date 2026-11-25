using Microsoft.AspNetCore.Components;
using WordCollector.Shared;

namespace WordCollector.Client.Pages.Shared;

public partial class Modal
{
    [Parameter] public bool IsVisible { get; set; }
    [Parameter] public string Title { get; set; } = "Modal Title";
    [Parameter] public string ConfirmButtonName { get; set; } = "Confirm";
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public EventCallback<bool> OnClose { get; set; }
    [Parameter] public EventCallback OnConfirmed { get; set; }

    private IModalForm? _activeForm;

    //child form registration 
    public void RegisterForm(IModalForm form) => _activeForm = form;
    public void Show() => IsVisible = true;
    public void Close() => IsVisible = false;

    private void Cancel() => Close();

    private async Task ConfirmAsync()
    {
        //form validation (modal will close once confirmed
        if (_activeForm is not null)
        {
            bool isValid = await _activeForm.SubmitAsync();
            if (!isValid) return;
        }

        await OnConfirmed.InvokeAsync();
        Close();
    }
}
