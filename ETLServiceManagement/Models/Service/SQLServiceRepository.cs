namespace ETLServiceManagement.Models.Service
{
    public class SQLServiceRepository:IServiceRepository
    {
        private readonly AppDbContext Context;
        public SQLServiceRepository(AppDbContext context)
        {
            Context = context;
        }

        public Service AddService(Service service)
        {
            Context.services.Add(service);
            Context.SaveChanges();
            return service;
        }

        public Service DeleteService(int id)
        {
            Service service = Context.services.Find(id);

            if(service != null)
            {
                Context.services.Remove(service);
                Context.SaveChanges(true);
            }
            
            return service;
        }

        public IEnumerable<Service> GetAllServices()
        {
            return Context.services;
        }

        public Service GetService(int id)
        {
            return Context.services.FirstOrDefault( e=>e.ServiceId==id);
            
        }

        public Service UpdateService(Service service)
        {
            var ExistingService = GetService(service.ServiceId);
            if (service != null)
            {
                Context.Entry(ExistingService).CurrentValues.SetValues(service);
                Context.SaveChanges() ;
                
            }

            throw new NotImplementedException();
        }
    }
}
