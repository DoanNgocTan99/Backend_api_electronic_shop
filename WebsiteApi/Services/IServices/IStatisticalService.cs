﻿using System.Collections.Generic;
using WebsiteApi.Model.Dtos;

namespace WebsiteApi.Services.IServices
{
    public interface IStatisticalService
    {
        IEnumerable<StatisticalDto> GetAll();
    }
}
