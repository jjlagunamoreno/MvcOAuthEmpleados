using System.Security.Claims;
using ApiOAuthEmpleados.Models;
using Microsoft.AspNetCore.Mvc;
using MvcOAuthEmpleados.Filters;
using MvcOAuthEmpleados.Services;

namespace MvcOAuthEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private ServiceEmpleados service;

        public EmpleadosController(ServiceEmpleados service)
        {
            this.service = service;
        }
        [AuthorizeEmpleados]
        public async Task<IActionResult> Index()
        {
            List<Empleado> empleados =
                await this.service.GetEmpleadosAsync();
            return View(empleados);
        }

        public async Task<IActionResult> Details(int id)
        {
            Empleado empleado = await
                    this.service.FindEmpleadoAsync(id);
            return View(empleado);
        }
        public async Task<IActionResult> PerfilEmpleado()
        {
            //NECESITAMOS BUSCAR AL EMPLEADO QUE HA REALIZADO 
            //LOGIN
            var data = HttpContext.User.FindFirst
                (x => x.Type == ClaimTypes.NameIdentifier).Value;
            int id = int.Parse(data);
            Empleado empleado = await
                    this.service.FindEmpleadoAsync(id);
            return View(empleado);
        }

        [AuthorizeEmpleados]
        public async Task<IActionResult> Perfil()
        {
            Empleado empleado = await
                this.service.GetPerfilAsync();
            return View(empleado);
        }

        [AuthorizeEmpleados]
        public async Task<IActionResult> Compis()
        {
            List<Empleado> empleados = await
                this.service.GetCompisAsync();
            return View(empleados);
        }
    }
}
