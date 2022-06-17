using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using RbsSampleWebApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RbsSampleWebApp.Pages;

public partial class AddLanguageApiComponent : ComponentBase
{
    [Parameter(CaptureUnmatchedValues = true)]
    public IReadOnlyDictionary<string, dynamic> Attributes { get; set; }

    public void Reload()
    {
        InvokeAsync(StateHasChanged);
    }

    public void OnPropertyChanged(PropertyChangedEventArgs args)
    {
    }

    [Inject]
    protected IJSRuntime JSRuntime { get; set; }

    [Inject]
    protected NavigationManager UriHelper { get; set; }

    [Inject]
    protected DialogService DialogService { get; set; }

    [Inject]
    protected TooltipService TooltipService { get; set; }

    [Inject]
    protected ContextMenuService ContextMenuService { get; set; }

    [Inject]
    protected NotificationService NotificationService { get; set; }

    //[Inject]
    //protected SecurityService Security { get; set; }

    //[Inject]
    //protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }

    [Inject]
    protected IRbsSampleApiService RbsSampleApiService { get; set; }

    RBSSample.Shared.Dtos.Language.LanguageForCreationDto _languageforcreationdto;
    protected RBSSample.Shared.Dtos.Language.LanguageForCreationDto languageforcreationdto
    {
        get
        {
            return _languageforcreationdto;
        }
        set
        {
            if (!object.Equals(_languageforcreationdto, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "languageforcreationdto", NewValue = value, OldValue = _languageforcreationdto };
                _languageforcreationdto = value;
                OnPropertyChanged(args);
                Reload();
            }
        }
    }



    protected override async Task OnInitializedAsync()
    {
        //await Security.InitializeAsync(AuthenticationStateProvider);
        //if (!Security.IsAuthenticated())
        //{
        //    UriHelper.NavigateTo("Login", true);
        //}
        //else
        //{
        //    await Load();
        //}
        await Load();
    }

    protected async Task Load()
    {
        languageforcreationdto = new RBSSample.Shared.Dtos.Language.LanguageForCreationDto() { };
    }

    protected async Task Form0Submit(RBSSample.Shared.Dtos.Language.LanguageForCreationDto args)
    {
        try
        {
            var result = await RbsSampleApiService.CreateLanguage(body: languageforcreationdto);

            UriHelper.NavigateTo("languages-api");
        }
        catch (System.Exception createLanguageException)
        {
            NotificationService.Notify(new NotificationMessage() { Severity = NotificationSeverity.Error, Summary = $"Error", Detail = $"Unable to create new Language!" });
        }
    }

    protected async Task Button2Click(MouseEventArgs args)
    {
        UriHelper.NavigateTo("languages-api");
    }    
}
