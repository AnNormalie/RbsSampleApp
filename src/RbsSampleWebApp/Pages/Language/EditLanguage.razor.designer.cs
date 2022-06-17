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
    public partial class EditLanguageComponent : ComponentBase
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

        RbsSampleWebApp.Models.RbsSampleDb.Language _language;
        protected RbsSampleWebApp.Models.RbsSampleDb.Language language
        {
            get
            {
                return _language;
            }
            set
            {
                if (!object.Equals(_language, value))
                {
                    var args = new PropertyChangedEventArgs(){ Name = "language", NewValue = value, OldValue = _language };
                    _language = value;
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
            var rbsSampleDbGetLanguageByIdResult = await RbsSampleDb.GetLanguageById(Guid.Parse(Id));
            language = rbsSampleDbGetLanguageByIdResult;
        }

        protected async System.Threading.Tasks.Task Form0Submit(RbsSampleWebApp.Models.RbsSampleDb.Language args)
        {
            try
            {
                var rbsSampleDbUpdateLanguageResult = await RbsSampleDb.UpdateLanguage(Guid.Parse(Id), language);
                UriHelper.NavigateTo("languages");
            }
            catch (System.Exception rbsSampleDbUpdateLanguageException)
            {
                NotificationService.Notify(new NotificationMessage(){ Severity = NotificationSeverity.Error,Summary = $"Error",Detail = $"Unable to update Language" });
            }
        }

        protected async System.Threading.Tasks.Task Button2Click(MouseEventArgs args)
        {
            UriHelper.NavigateTo("languages");
        }
    }
}
