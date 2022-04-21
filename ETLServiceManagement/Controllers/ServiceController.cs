using ETLServiceManagement.Models.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETLServiceManagement.Controllers
{
    public class ServiceController : Controller
    {
        // GET: ServiceController
        //return index view in service folder

        private IServiceRepository _appServiceRepository;
        public ServiceController(IServiceRepository appServiceRepository)
        {
            _appServiceRepository = appServiceRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateService()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult CreateService(Service service)
        {
            //if(ModelState.IsValid)
            {
                _appServiceRepository.AddService(service);
            }

            var model = _appServiceRepository.GetAllServices();
            return View("AllServices", model);
        }

        public ViewResult AllServices()
        {
            var model=_appServiceRepository.GetAllServices();
            return View(model);
        }

            // GET: ServiceController/Details/5
        public ActionResult Details(int id)
        {
            Service service= _appServiceRepository.GetService(id);
            return View("ServiceDetail",service);
        }


        // GET: ServiceController/Edit/5
        public ActionResult EditService(int id)
        {
            Service service= _appServiceRepository.GetService(id);
            return View("EditService",service);
        }

        // POST: ServiceController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditService(Service service)
        {
            
            try
            {
                _appServiceRepository.UpdateService(service);
                var model = _appServiceRepository.GetAllServices();
                return View("AllServices", model);
            }
            catch
            {
                var model = _appServiceRepository.GetAllServices();
                return View("AllServices", model);
            }


            //var employee = context.Employees.Attach(employeeChanges);
            //employee.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //context.SaveChanges();
            //return employeeChanges;

        }

        // GET: ServiceController/Delete/5
        public ActionResult DeleteService(int id)
        {
            _appServiceRepository.DeleteService(id);

            var model = _appServiceRepository.GetAllServices();

            return View("AllServices",model);
        }

    }
}
