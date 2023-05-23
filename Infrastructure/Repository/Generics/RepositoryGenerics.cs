using Domain.Interfaces.Generics;
using Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Generics
{
    public class RepositoryGenerics<T> : IGenerics<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<ContextBase> _optionBuilder;

        public RepositoryGenerics()
        {
            _optionBuilder = new DbContextOptions<ContextBase>();
        }

        public async Task Add(T Object)
        {
            using (var data = new ContextBase(_optionBuilder))
            {
                await data.Set<T>().AddAsync(Object);
                await data.SaveChangesAsync();
            }
        }

        public async Task Delete(T Object)
        {
            using (var data = new ContextBase(_optionBuilder))
            {
                data.Set<T>().Remove(Object);
                await data.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityById(int Id)
        {
            using (var data = new ContextBase(_optionBuilder))
            {
                return await data.Set<T>().FindAsync(Id);                
            }
        }

        public async Task<List<T>> List()
        {
            using (var data = new ContextBase(_optionBuilder))
            {
                return await data.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task Update(T Object)
        {
            using (var data = new ContextBase(_optionBuilder))
            {
                data.Set<T>().Update(Object);
                await data.SaveChangesAsync();
            }
        }

        #region DISPOSE
        //*ESTA CONFIGURAÇÃO É PADRÃO DA MICROSOFT.

        bool disposedValue = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
                return;

            if (disposing)
                handle.Dispose();

            disposedValue = true;
        }

        #endregion

    }
}
