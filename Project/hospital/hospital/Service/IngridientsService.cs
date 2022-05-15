using hospital.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Service
{
    public class IngridientsService
    {
        private IngridientsRepository ingridientsRepository;

        public IngridientsService(IngridientsRepository ingridientsRepository) {
            this.ingridientsRepository = ingridientsRepository;
        }

        public List<string> FindAll() {
            return ingridientsRepository.FindAll();
        }
    }
}
