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
    public partial class LanguagesComponent : ComponentBase
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
        protected RadzenDataGrid<RbsSampleWebApp.Models.RbsSampleDb.Language> grid0;

        IEnumerable<RbsSampleWebApp.Models.RbsSampleDb.Language> _getLanguagesResult;
        protected IEnumerable<RbsSampleWebApp.Models.RbsSampleDb.Language> getLanguagesResult
        {
            get
            {
                return _getLanguagesResult;
            }
            set
            {
                if (!object.Equals(_getLanguagesResult, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "getLanguagesResult", NewValue = value, OldValue = _getLanguagesResult };
                    _getLanguagesResult = value;
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
            var rbsSampleDbGetLanguagesResult = await RbsSampleDb.GetLanguages();
            getLanguagesResult = rbsSampleDbGetLanguagesResult;
        }

        protected async System.Threading.Tasks.Task Button0Click(MouseEventArgs args)
        {
            UriHelper.NavigateTo("add-language");
        }

        protected async System.Threading.Tasks.Task Splitbutton0Click(RadzenSplitButtonItem args)
        {
            if (args?.Value == "csv")
            {
                await RbsSampleDb.ExportLanguagesToCSV(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,Tag,EnglishName" }, $"Languages");

            }

            if (args == null || args.Value == "xlsx")
            {
                await RbsSampleDb.ExportLanguagesToExcel(new Query() { Filter = $@"{(string.IsNullOrEmpty(grid0.Query.Filter)? "true" : grid0.Query.Filter)}", OrderBy = $"{grid0.Query.OrderBy}", Expand = "", Select = "Id,Tag,EnglishName" }, $"Languages");

            }
        }

        protected async System.Threading.Tasks.Task Grid0RowSelect(RbsSampleWebApp.Models.RbsSampleDb.Language args)
        {
            UriHelper.NavigateTo($"edit-language/{args.Id}");
        }

        protected async System.Threading.Tasks.Task GridDeleteButtonClick(MouseEventArgs args, dynamic data)
        {
            try
            {
                if (await DialogService.Confirm("Are you sure you want to delete this record?") == true)
                {
                    var rbsSampleDbDeleteLanguageResult = await RbsSampleDb.DeleteLanguage(data.Id);
                    if (rbsSampleDbDeleteLanguageResult != null)
                    {
                        await grid0.Reload();
                    }
                }
            }
            catch (System.Exception rbsSampleDbDeleteLanguageException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to delete Language" });
            }
        }
    }
}
