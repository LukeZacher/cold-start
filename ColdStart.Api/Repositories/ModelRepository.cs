using ColdStart.Api.Data;
using ColdStart.Api.Models.Entities;
using ColdStart.Api.Repositories.Interfaces;

namespace ColdStart.Api.Repositories;

public class ModelRepository : Repository<Model>, IModelRepository
{
    public ModelRepository(AppDbContext context)
        : base(context) { }
}
