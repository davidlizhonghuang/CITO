using NumtoWord.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumtoWord.Services
{
    public class NumToWordService : INumToWordService
    {
        EDMX.Repository.INumToWordRepository _repository;

        public NumToWordService(EDMX.Repository.INumToWordRepository repository)
        {
            _repository = repository;
        }

        public void Add(NumToWord numtoword) {

            _repository.Add(numtoword);
            _repository.SaveChanges();

        }

        public List<NumToWord> GetAll()
        {
            return _repository.GetAllDatas();
//not practical,  need grid data to improve the performance
        }



    }
}
