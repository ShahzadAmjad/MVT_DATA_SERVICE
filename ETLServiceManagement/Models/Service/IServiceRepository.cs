namespace ETLServiceManagement.Models.Service
{
    public interface IServiceRepository
    {
        Service AddService(Service service);
        Service GetService(int id);
        IEnumerable< Service> GetAllServices();
        Service UpdateService(Service service);
        Service DeleteService(int id);


    }
}
