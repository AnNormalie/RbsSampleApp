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
    public partial class LocationsComponent : ComponentBase
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
        protected RadzenDataGrid<RbsSampleWebApp.Models.RbsSampleDb.Location> grid0;

        IEnumerable<RbsSampleWebApp.Models.RbsSampleDb.Location> _getLocationsResult;
        protected IEnumerable<RbsSampleWebApp.Models.RbsSampleDb.Location> getLocationsResult
        {
            get
            {
                return _getLocationsResult;
            }
            set
            {
                if (!object.Equals(_getLocationsResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getLocationsResult", NewValue = value, OldValue = _getLocationsResult };
                    _getLocationsResult = value;
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
            var rbsSampleDbGetLocationsResult = await RbsSampleDb.GetLocations(new Query() { Expand = "Language" });
            getLocationsResult = rbsSampleDbGetLocationsResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            UriHelper.NavigateTo("add-location");
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await RbsSampleDb.ExportLocationsToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Language", Select = "Id,Key,Language.Tag as LanguageTag,Information,Exist,CreatedOn,CreatedBy,LastModifiedOn,LastModifiedBy,IsDeleted" }, $"Locations");

            }

            if (args == null || args.Value == "xlsx")
            {
                await RbsSampleDb.ExportLocationsToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "Language", Select = "Id,Key,Language.Tag as LanguageTag,Information,Exist,CreatedOn,CreatedBy,LastModifiedOn,LastModifiedBy,IsDeleted" }, $"Locations");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(RbsSampleWebApp.Models.RbsSampleDb.Location args)
        {
            UriHelper.NavigateTo($"edit-location/{args.Id}");
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var rbsSampleDbDeleteLocationResult = await RbsSampleDb.DeleteLocation(data.Id);
                    if (rbsSampleDbDeleteLocationResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception rbsSampleDbDeleteLocationException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Location" });
            }
        }
    }
}
