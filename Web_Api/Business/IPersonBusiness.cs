﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web_Api.Model;

namespace Web_Api.Business
{
    public interface IPersonBusiness
    {
        Person Create(Person person);
        Person FindById(long id);
        List<Person> FindAll();
        Person Update(Person person);
        void Delete(long id);
    }
}