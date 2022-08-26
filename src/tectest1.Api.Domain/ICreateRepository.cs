namespace tectest1.Api.Domain
{
    public interface ICreateRepository<TEntity>
    {
        bool Create(TEntity entity);

    }
}