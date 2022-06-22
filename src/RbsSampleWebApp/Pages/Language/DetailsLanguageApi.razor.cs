using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using Radzen;
using Radzen.Blazor;
using RbsSampleWebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RbsSampleWebApp.Pages;

public partial class DetailsLanguageApiComponent : ComponentBase
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

    [Parameter]
    public dynamic Id { get; set; }

    protected RadzenDataGrid<RBSSample.Shared.Dtos.Location.LocationDto> gridLocations;


    protected bool isLoading;

    RBSSample.Shared.Dtos.Language.LanguageDto _getLanguageByIdDto;
    protected RBSSample.Shared.Dtos.Language.LanguageDto getLanguageByIdDto
    {
        get
        {
            return _getLanguageByIdDto;
        }
        set
        {
            if (!object.Equals(_getLanguageByIdDto, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "getLanguageByIdDto", NewValue = value, OldValue = _getLanguageByIdDto };
                _getLanguageByIdDto = value;
                OnPropertyChanged(args);
                Reload();
            }
        }
    }

    ICollection<RBSSample.Shared.Dtos.Location.LocationDto> _locationsForLanguageDto;
    protected ICollection<RBSSample.Shared.Dtos.Location.LocationDto> locationsForLanguageDto
    {
        get
        {
            return _locationsForLanguageDto;
        }
        set
        {
            if (!object.Equals(_locationsForLanguageDto, value))
            {
                var args = new PropertyChangedEventArgs() { Name = "locationsForLanguageDto", NewValue = value, OldValue = _locationsForLanguageDto };
                _locationsForLanguageDto = value;
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
        isLoading = true;

        try
        {
            getLanguageByIdDto = await RbsSampleApiService.GetLanguageById((Guid)Id);


            //locationsForLanguageDto = getLanguageByIdDto.Locations;
        }
        catch (System.Exception someException)
        {
            NotificationService.Notify(
                new NotificationMessage()
                {
                    Severity = NotificationSeverity.Error,
                    Summary = $"Error",
                    Detail = $"Unable to update Language"
                });
        }
        finally
        {
            isLoading = false;
        }

    }

    protected async Task ButtonBackClick(MouseEventArgs args)
    {
        UriHelper.NavigateTo("languages-api");
    }

}
