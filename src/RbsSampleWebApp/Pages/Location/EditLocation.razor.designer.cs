using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Radzen;
using Radzen.Blazor;
using RbsSampleWebApp.Models.RbsSampleDb;
using Microsoft.EntityFrameworkCore;

namespace RbsSampleWebApp.Pages
{
    public partial class EditLocationComponent : ComponentBase
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

        [Inject]
        protected RbsSampleDbService RbsSampleDb { get; set; }

        [Parameter]
        public dynamic Id { get; set; }

        RbsSampleWebApp.Models.RbsSampleDb.Location _location;
        protected RbsSampleWebApp.Models.RbsSampleDb.Location location
        {
            get
            {
                return _location;
            }
            set
            {
                if (!object.Equals(_location, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "location", NewValue = value, OldValue = _location };
                    _location = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        IEnumerable<RbsSampleWebApp.Models.RbsSampleDb.Language> _getLanguagesForLanguageIdResult;
        protected IEnumerable<RbsSampleWebApp.Models.RbsSampleDb.Language> getLanguagesForLanguageIdResult
        {
            get
            {
                return _getLanguagesForLanguageIdResult;
            }
            set
            {
                if (!object.Equals(_getLanguagesForLanguageIdResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getLanguagesForLanguageIdResult", NewValue = value, OldValue = _getLanguagesForLanguageIdResult };
                    _getLanguagesForLanguageIdResult = value;
                    OnPropertyChanged(args);
                    Reload();
                }
            }
        }

        protected override async System.Threading.Tasks.Task OnInitializedAsync()
        {
            await Load();
        }
        protected async System.Threading.Tasks.Task Load()
        {
            var rbsSampleDbGetLocationByIdResult = await RbsSampleDb.GetLocationById(Guid.Parse(Id));
            location = rbsSampleDbGetLocationByIdResult;

            var rbsSampleDbGetLanguagesResult = await RbsSampleDb.GetLanguages();
            getLanguagesForLanguageIdResult = rbsSampleDbGetLanguagesResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(RbsSampleWebApp.Models.RbsSampleDb.Location args)
        {
            try
            {
                var rbsSampleDbUpdateLocationResult = await RbsSampleDb.UpdateLocation(Guid.Parse(Id), location);
                UriHelper.NavigateTo("locations");
            }
            catch (System.Exception rbsSampleDbUpdateLocationException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Location" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            UriHelper.NavigateTo("locations");
        }
    }
}
