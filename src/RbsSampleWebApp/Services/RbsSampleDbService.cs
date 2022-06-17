using Radzen;
using System;
using System.Web;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Data;
using System.Text.Encodings.Web;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components;
using RbsSampleWebApp.Data;

namespace RbsSampleWebApp
{
    public partial class RbsSampleDbService
    {
        RbsSampleDbContext Context
        {
           get
           {
             return this.context;
           }
        }

        private readonly RbsSampleDbContext context;
        private readonly NavigationManager navigationManager;

        public RbsSampleDbService(RbsSampleDbContext context, NavigationManager navigationManager)
        {
            this.context = context;
            this.navigationManager = navigationManager;
        }

        public void Reset() => Context.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);

        public async Task ExportLanguagesToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/rbssampledb/languages/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/rbssampledb/languages/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLanguagesToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/rbssampledb/languages/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/rbssampledb/languages/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLanguagesRead(ref IQueryable<Models.RbsSampleDb.Language> items);

        public async Task<IQueryable<Models.RbsSampleDb.Language>> GetLanguages(Query query = null)
        {
            var items = Context.Languages.AsQueryable();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnLanguagesRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLanguageCreated(Models.RbsSampleDb.Language item);
        partial void OnAfterLanguageCreated(Models.RbsSampleDb.Language item);

        public async Task<Models.RbsSampleDb.Language> CreateLanguage(Models.RbsSampleDb.Language language)
        {
            OnLanguageCreated(language);

            var existingItem = Context.Languages
                              .Where(i => i.Id == language.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Languages.Add(language);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(language).State = EntityState.Detached;
                throw;
            }

            OnAfterLanguageCreated(language);

            return language;
        }
        public async Task ExportLocationsToExcel(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/rbssampledb/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/rbssampledb/locations/excel(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        public async Task ExportLocationsToCSV(Query query = null, string fileName = null)
        {
            navigationManager.NavigateTo(query != null ? query.ToUrl($"export/rbssampledb/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')") : $"export/rbssampledb/locations/csv(fileName='{(!string.IsNullOrEmpty(fileName) ? UrlEncoder.Default.Encode(fileName) : "Export")}')", true);
        }

        partial void OnLocationsRead(ref IQueryable<Models.RbsSampleDb.Location> items);

        public async Task<IQueryable<Models.RbsSampleDb.Location>> GetLocations(Query query = null)
        {
            var items = Context.Locations.AsQueryable();

            items = items.Include(i => i.Language);

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Expand))
                {
                    var propertiesToExpand = query.Expand.Split(',');
                    foreach(var p in propertiesToExpand)
                    {
                        items = items.Include(p);
                    }
                }

                if (!string.IsNullOrEmpty(query.Filter))
                {
                    if (query.FilterParameters != null)
                    {
                        items = items.Where(query.Filter, query.FilterParameters);
                    }
                    else
                    {
                        items = items.Where(query.Filter);
                    }
                }

                if (!string.IsNullOrEmpty(query.OrderBy))
                {
                    items = items.OrderBy(query.OrderBy);
                }

                if (query.Skip.HasValue)
                {
                    items = items.Skip(query.Skip.Value);
                }

                if (query.Top.HasValue)
                {
                    items = items.Take(query.Top.Value);
                }
            }

            OnLocationsRead(ref items);

            return await Task.FromResult(items);
        }

        partial void OnLocationCreated(Models.RbsSampleDb.Location item);
        partial void OnAfterLocationCreated(Models.RbsSampleDb.Location item);

        public async Task<Models.RbsSampleDb.Location> CreateLocation(Models.RbsSampleDb.Location location)
        {
            OnLocationCreated(location);

            var existingItem = Context.Locations
                              .Where(i => i.Id == location.Id)
                              .FirstOrDefault();

            if (existingItem != null)
            {
               throw new Exception("Item already available");
            }            

            try
            {
                Context.Locations.Add(location);
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(location).State = EntityState.Detached;
                location.Language = null;
                throw;
            }

            OnAfterLocationCreated(location);

            return location;
        }

        partial void OnLanguageDeleted(Models.RbsSampleDb.Language item);
        partial void OnAfterLanguageDeleted(Models.RbsSampleDb.Language item);

        public async Task<Models.RbsSampleDb.Language> DeleteLanguage(Guid? id)
        {
            var itemToDelete = Context.Languages
                              .Where(i => i.Id == id)
                              .Include(i => i.Locations)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLanguageDeleted(itemToDelete);

            Context.Languages.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLanguageDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnLanguageGet(Models.RbsSampleDb.Language item);

        public async Task<Models.RbsSampleDb.Language> GetLanguageById(Guid? id)
        {
            var items = Context.Languages
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            var itemToReturn = items.FirstOrDefault();

            OnLanguageGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.RbsSampleDb.Language> CancelLanguageChanges(Models.RbsSampleDb.Language item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLanguageUpdated(Models.RbsSampleDb.Language item);
        partial void OnAfterLanguageUpdated(Models.RbsSampleDb.Language item);

        public async Task<Models.RbsSampleDb.Language> UpdateLanguage(Guid? id, Models.RbsSampleDb.Language language)
        {
            OnLanguageUpdated(language);

            var itemToUpdate = Context.Languages
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(language);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterLanguageUpdated(language);

            return language;
        }

        partial void OnLocationDeleted(Models.RbsSampleDb.Location item);
        partial void OnAfterLocationDeleted(Models.RbsSampleDb.Location item);

        public async Task<Models.RbsSampleDb.Location> DeleteLocation(Guid? id)
        {
            var itemToDelete = Context.Locations
                              .Where(i => i.Id == id)
                              .FirstOrDefault();

            if (itemToDelete == null)
            {
               throw new Exception("Item no longer available");
            }

            OnLocationDeleted(itemToDelete);

            Context.Locations.Remove(itemToDelete);

            try
            {
                Context.SaveChanges();
            }
            catch
            {
                Context.Entry(itemToDelete).State = EntityState.Unchanged;
                throw;
            }

            OnAfterLocationDeleted(itemToDelete);

            return itemToDelete;
        }

        partial void OnLocationGet(Models.RbsSampleDb.Location item);

        public async Task<Models.RbsSampleDb.Location> GetLocationById(Guid? id)
        {
            var items = Context.Locations
                              .AsNoTracking()
                              .Where(i => i.Id == id);

            items = items.Include(i => i.Language);

            var itemToReturn = items.FirstOrDefault();

            OnLocationGet(itemToReturn);

            return await Task.FromResult(itemToReturn);
        }

        public async Task<Models.RbsSampleDb.Location> CancelLocationChanges(Models.RbsSampleDb.Location item)
        {
            var entityToCancel = Context.Entry(item);
            if (entityToCancel.State == EntityState.Modified)
            {
              entityToCancel.CurrentValues.SetValues(entityToCancel.OriginalValues);
              entityToCancel.State = EntityState.Unchanged;
            }

            return item;
        }

        partial void OnLocationUpdated(Models.RbsSampleDb.Location item);
        partial void OnAfterLocationUpdated(Models.RbsSampleDb.Location item);

        public async Task<Models.RbsSampleDb.Location> UpdateLocation(Guid? id, Models.RbsSampleDb.Location location)
        {
            OnLocationUpdated(location);

            var itemToUpdate = Context.Locations
                              .Where(i => i.Id == id)
                              .FirstOrDefault();
            if (itemToUpdate == null)
            {
               throw new Exception("Item no longer available");
            }

            var entryToUpdate = Context.Entry(itemToUpdate);
            entryToUpdate.CurrentValues.SetValues(location);
            entryToUpdate.State = EntityState.Modified;
            Context.SaveChanges();       

            OnAfterLocationUpdated(location);

            return location;
        }
    }
}
