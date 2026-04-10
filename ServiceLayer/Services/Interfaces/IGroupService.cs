using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServiceLayer.Services.Interfaces
{
   public interface IGroupService
    {
        Group Create(Group group);
        Group GetById(int id);
        List<Group> GetAll(Predicate<Group> predicate);
        Group Update(int id,Group group);
        void Delete(int id);
    }
}
