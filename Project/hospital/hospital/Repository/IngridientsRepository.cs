using hospital.FileHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hospital.Repository
{
    public class IngridientsRepository
    {
        private IngridientsFileHandler ingridientsFileHandler;
        private List<string> ingridients;

        public IngridientsRepository() {
            this.ingridientsFileHandler = new IngridientsFileHandler();
            ingridients = new List<string>();
        }

        public List<string> FindAll() {
            return ingridients;
        }

        public void Create(string ingridient) {
            ingridients.Add(ingridient);
            WriteIngridientsData();
        }
        public void LoadIngridientsData()
        {
            if (ingridientsFileHandler.Read() != null)
            {
                foreach (string ingridient in ingridientsFileHandler.Read())
                {
                    ingridients.Add(ingridient);
                }
            }
        }

        public void WriteIngridientsData() {
            ingridientsFileHandler.Write(ingridients);
        }
    }
}
