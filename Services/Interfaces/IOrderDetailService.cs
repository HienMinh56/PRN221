﻿using BOs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderDetailService
    {
        public List<OrderDetail> GetOrderDetails(string orderId);
    }
}