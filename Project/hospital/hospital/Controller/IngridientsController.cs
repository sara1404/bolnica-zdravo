using hospital.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Controller
{
    public class IngridientsController
    {
        private IngridientsService ingridientsService;

        public IngridientsController(IngridientsService ingridientsService) {
            this.ingridientsService = ingridientsService;
        }

        public List<string> FindAll() {
            return ingridientsService.FindAll();
        }

        public void Create(string ingridient) {
            ingridientsService.Create(ingridient);
        }
    }
}
