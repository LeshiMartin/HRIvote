using AutoMapper;
using HRiVote.DTO;
using HRiVote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRiVote.App_Start
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            Mapper.CreateMap<Employee, EmployeeDTO>();
            Mapper.CreateMap<EmployeeDTO, Employee>();
        }
    }
}