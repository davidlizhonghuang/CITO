using System;
using System.Linq;
using NumtoWord.Base.Interfaces;
using NumtoWord.Models;
using NumtoWord.EDMX.Repository.core;
using System.Collections.Generic;

namespace NumtoWord.EDMX.Repository
{
    public class NumToWordRepository : GenericRepository<NumToWord>, INumToWordRepository
    {

        public NumToWordRepository(IObjectContextManager objectContextManager) 
            : base(objectContextManager)
        {

        }

        public   List<NumToWord> GetAllDatas()
        {
             
            var alluser = (from e in Entities select e).ToList();

            return alluser;
        

    }

    public override void Add(NumToWord entity)
        {
            Entities.AddObject(entity);
        }

        public override void Delete(NumToWord entity)
        {
            Entities.DeleteObject(entity); 
        }

        public override void Update(NumToWord entity)
        {
            throw new NotImplementedException();
        }
    }
}