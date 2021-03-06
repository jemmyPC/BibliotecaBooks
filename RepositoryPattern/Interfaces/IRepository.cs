﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryPattern.Interfaces
{
   public interface IRepository <T> where T: class
    {
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Insert(T obj);
        void Update(T obj, int id);
        void Delete(T obj);
    }
}
