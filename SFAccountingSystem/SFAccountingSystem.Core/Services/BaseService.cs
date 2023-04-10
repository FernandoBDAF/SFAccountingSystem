using Microsoft.EntityFrameworkCore;
using SFAccountingSystem.Core.Models;

namespace SFAccountingSystem.Core.Services
{
    public class BaseService<Entity> where Entity : BaseModel
    {
        public readonly DataContext context;
        public readonly DbSet<Entity> entity;

        public BaseService(DataContext context)
        {
            this.context = context;
            entity = context.Set<Entity>();
        }

        public virtual async Task<List<Entity>> List()
        {
            return await entity.OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public virtual async Task<Entity> Get(int id)
        {
            return await entity.FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<Entity> Add(Entity obj)
        {
            obj.CreatedAt = DateTime.Now;
            await entity.AddAsync(obj);
            await context.SaveChangesAsync();

            return obj;
        }

        public virtual async Task Update(Entity obj)
        {
            entity.Update(obj);
            await context.SaveChangesAsync();
        }

        public virtual async Task Delete(int id)
        {
            var obj = await entity.FirstOrDefaultAsync(x => x.Id == id);

            obj.DeletedAt = DateTime.Now;

            entity.Update(obj);

            await context.SaveChangesAsync();
        }
    }
}
