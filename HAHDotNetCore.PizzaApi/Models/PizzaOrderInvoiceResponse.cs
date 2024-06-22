using System;
using System.Collections.Generic;

namespace HAHDotNetCore.PizzaApi.Models;

public class PizzaOrderInvoiceResponse
{
    public PizzaOrderInvoiceHeadModel Order { get; set; }

    public List<PizzaOrderInvoiceDetailModel> OrderDetail { get; set; }
}

