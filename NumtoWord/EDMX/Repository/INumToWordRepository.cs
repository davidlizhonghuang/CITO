using NumtoWord.Base.Interfaces;
using NumtoWord.Models;
using System.Collections.Generic;

namespace NumtoWord.EDMX.Repository
{
    public interface INumToWordRepository: IDALRepository<NumToWord>
    {
        void Add(NumToWord entity);
        void Delete(NumToWord entity);
        void Update(NumToWord entity);
        List<NumToWord> GetAllDatas();
    }
}