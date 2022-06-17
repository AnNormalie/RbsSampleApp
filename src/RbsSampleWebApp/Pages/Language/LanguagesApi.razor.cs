using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using RBSSample.Shared.Dtos.Language;
using RbsSampleWebApp.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RbsSampleWebApp.Pages;

public partial class LanguagesApiComponent : ComponentBase
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


    protected RadzenDataGrid<RBSSample.Shared.Dtos.Language.LanguageDto> grid0;


    List<RBSSample.Shared.Dtos.Language.LanguageDto> _getLanguagesResult;
    protected List<RBSSample.Shared.Dtos.Language.LanguageDto> getLanguagesResult
    {
        get
        {
            return _getLanguagesResult;
        }
        set
        {
            if (!object.Equals(_getLanguagesResult, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "getLanguagesResult", NewValue = value, OldValue = _getLanguagesResult };
                _getLanguagesResult = value;
                OnPropertyChanged(args);
                Reload();
            }
        }
    }

    int _getLanguagesCount;
    protected int getLanguagesCount
    {
        get
        {
            return _getLanguagesCount;
        }
        set
        {
            if (!object.Equals(_getLanguagesCount, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "getLanguagesCount", NewValue = value, OldValue = _getLanguagesCount };
                _getLanguagesCount = value;
                OnPropertyChanged(args);
                Reload();
            }
        }
    }

    protected bool isLoading;

    protected RBSSample.Shared.Dtos.Language.LanguageParametersDto queryParams;


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
        queryParams = new RBSSample.Shared.Dtos.Language.LanguageParametersDto() { };
        
    }

    protected async Task Button0Click(MouseEventArgs args)
    {
        UriHelper.NavigateTo("/add-language-api");
    }

    protected async Task Grid0LoadData(LoadDataArgs args)
    {
        isLoading = true;
        try
        {
            queryParams.PageSize = grid0.PageSize;
            queryParams.PageNumber = grid0.CurrentPage + 1;
            queryParams.SortOrder = null; //args.OrderBy
            queryParams.Filters = null; //args.Filter
            queryParams.Includes = null; //Array.Empty<string>()

            var result = await RbsSampleApiService.GetLanguagesPaged(queryParams);
            getLanguagesResult = result.Languages;
            getLanguagesCount = result.TotalCount;

            isLoading = false;
        }
        catch (System.Exception iemsdbGetCompaniesException)
        {
            NotificationService.Notify(
                new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to load Languages",
                    Duration = 5000
                });

            isLoading = false;
        }
    }

    protected async Task Grid0RowSelect(RBSSample.Shared.Dtos.Language.LanguageDto args)
    {
        UriHelper.NavigateTo($"details-language-api/{args.Id}");
    }

    protected async Task GridEditButtonClick(MouseEventArgs args, dynamic data)
    {
        UriHelper.NavigateTo($"edit-language-api/{data.Id}");
    }

    protected async Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
    {
        try
        {
            if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
            {
                //Delete an entry
            }
        }
        catch (System.Exception deleteLanguageException)
        {
            NotificationService.Notify(
                new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to delete Language",
                    Duration = 5000
                });

        }
    }    
}
