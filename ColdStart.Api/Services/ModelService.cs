using ColdStart.Api.Models.Entities;
using ColdStart.Api.Repositories.Interfaces;
using ColdStart.Api.Services.Interfaces;

namespace ColdStart.Api.Services;

public class ModelService : Service<Model>, IModelService
{
    public ModelService(IModelRepository modelRepository)
        : base(modelRepository) { }
}
