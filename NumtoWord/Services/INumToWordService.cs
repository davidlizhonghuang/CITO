using NumtoWord.Models;
using System.Collections.Generic;

namespace NumtoWord.Services
{
    public interface INumToWordService
    {
        void Add(NumToWord numtoword);
        List<NumToWord> GetAll();
    }
}