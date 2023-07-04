using System.Collections.Generic;
using WebRhProject.Models;
using WebRhProject.Services.Exceptions;

namespace WebRhProject.Services
{
    public class HoleriteService
    {
        private readonly List<Holerite> _holerites; // Exemplo: lista em memória, substitua pela persistência real em um banco de dados

        public HoleriteService()
        {
            _holerites = new List<Holerite>();
        }

        public List<Holerite> GetAllHolerites()
        {
            return _holerites;
        }

        public Holerite GetHoleriteById(int id)
        {
            return _holerites.Find(h => h.Id == id);
        }

        public void InsertHolerite(Holerite holerite)
        {
            holerite.Id = GenerateHoleriteId();
            holerite.CalcularHolerite(); // Adicione esta linha para calcular os demais valores
            _holerites.Add(holerite);
        }


        public void UpdateHolerite(Holerite holerite)
        {
            int index = _holerites.FindIndex(h => h.Id == holerite.Id);
            if (index == -1)
            {
                throw new NotFoundException("Holerite not found");
            }

            _holerites[index] = holerite;
        }

        public void DeleteHolerite(int id)
        {
            int index = _holerites.FindIndex(h => h.Id == id);
            if (index == -1)
            {
                throw new NotFoundException("Holerite not found");
            }

            _holerites.RemoveAt(index);
        }

        private int GenerateHoleriteId()
        {
            // Lógica para gerar um novo ID de holerite (por exemplo, consultar o banco de dados ou usar um contador)
            // Substitua de acordo com sua implementação real
            int maxId = 0;
            foreach (var holerite in _holerites)
            {
                if (holerite.Id > maxId)
                {
                    maxId = holerite.Id;
                }
            }

            return maxId + 1;
        }
    }
}
