using System;
namespace HAHDotNetCore.PizzaApi.Queries;

public class PizzaQuery
{
    public static string PizzaOrderQuery =
        @"select po.*, p.PizzaName, p.Price from [dbo].[Tbl_PizzaOrder] po
        inner join Tbl_Pizza p on p.PizzaId = po.PizzaId
        where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";


    public static string PizzaOrderDetailQuery =
        @"select pod.*, pe.PizzaExtraName, pe.Price from [dbo].[Tbl_PIzzaOrderDetail] pod
        inner join Tbl_PizzaExtra pe on pe.PizzaExtraId = pod.PizzaExtraId
        where PizzaOrderInvoiceNo = @PizzaOrderInvoiceNo;";
}

