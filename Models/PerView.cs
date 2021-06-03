using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class PerView
    {
        public string Roler;
        public List<Action> Actions;
    }
    public class Action
    {
        public bool Index;
        public bool Create;
        public bool Edit;
        public bool Delete;
        public bool Approval;
    }
}
