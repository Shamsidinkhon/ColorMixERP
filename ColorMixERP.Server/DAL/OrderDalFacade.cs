﻿using ColorMixERP.Server.Config;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.BL;
using ColorMixERP.Server.Entities;
using ColorMixERP.Server.Entities.DTO;
using ColorMixERP.Server.Entities.Pagination;
using ColorMixERP.Server.Utils;

namespace ColorMixERP.Server.DAL
{
    public class OrderDalFacade
    {
        private LinqContext db = null;
        public OrderDalFacade()
        {
            db = new LinqContext(LinqContext.DB_CONNECTION);
        }
        public List<OrderDTO> GetClientOrders(OrderCommand cmd, int workplaceId,bool isAdmin,  ref int pagesCount)
        {
            IQueryable<OrderDTO> query;
            if (isAdmin)
            {
                query = from c in db.ClientOrders
                    where c.IsDeleted == false
                    select new OrderDTO()
                    {
                        Id = c.Id,
                        TransactionId = c.TransactinoId,
                        SalerId = c.Saler.Id ?? 0,
                        SalerName = c.Saler.Name + " " + c.Saler.Surname,
                        OrderDate = c.OrderDate,
                        Sales = new SaleBL().GetOrderSale(c.Id),
                        ClientId = c.Client.Id ?? 0,
                        ClientName = c.Client.Name,
                        ClientRepresentitive = c.ClientRepresentitive,
                        OverallPrice = c.OverallPrice,
                        PaymentByCard = c.PaymentByCard,
                        PaymentByTransfer = c.PaymentByTransfer,
                        PaymentByCash = c.PaymentByCash,
                        IsDebt = c.IsDebt,
                        WorkPlaceName = c.Saler.WorkPlace.Name,
                        WorkPlaceLocation = c.Saler.WorkPlace.Location,
                        WorkPlaceId = c.Saler.WorkPlaceId
                    };
            }
            else
            {
                query = from c in db.ClientOrders
                    where c.IsDeleted == false
                    join user in db.AccountUsers on c.Saler.Id equals user.Id
                    where user.WorkPlaceId == workplaceId
                    select new OrderDTO()
                    {
                        Id = c.Id,
                        TransactionId = c.TransactinoId,
                        SalerId = c.Saler.Id ?? 0,
                        SalerName = c.Saler.Name + " " + c.Saler.Surname,
                        OrderDate = c.OrderDate,
                        Sales = new SaleBL().GetOrderSale(c.Id),
                        ClientId = c.Client.Id ?? 0,
                        ClientName = c.Client.Name,
                        ClientRepresentitive = c.ClientRepresentitive,
                        OverallPrice = c.OverallPrice,
                        PaymentByCard = c.PaymentByCard,
                        PaymentByTransfer = c.PaymentByTransfer,
                        PaymentByCash = c.PaymentByCash,
                        IsDebt = c.IsDebt,
                        WorkPlaceName = c.Saler.WorkPlace.Name,
                        WorkPlaceLocation = c.Saler.WorkPlace.Location,
                        WorkPlaceId = c.Saler.WorkPlaceId
                    };
            }

            if (cmd.FilterWorkPlaceId > 0)
            {
                query = from p in query where p.WorkPlaceId == cmd.FilterWorkPlaceId select p;
            }
            if (cmd.SalerId > 0)
            {
                query = from p in query where p.SalerId == cmd.SalerId select p;
            }

            if (cmd.ClientId > 0)
            {
                query = from p in query where p.ClientId == cmd.ClientId select p;
            }

            if (cmd.OrderDate != null)
            {
                query = from p in query where p.OrderDate.Date >= cmd.OrderDate.Value.Date
                                            && p.OrderDate.Date <= cmd.OrderDate.Value.Date
                        select p;
            }

            if (cmd.FromDate != null && cmd.ToDate != null)
            {
                query = from p in query where p.OrderDate.Date >= cmd.FromDate.Value.Date &&
                                              p.OrderDate.Date <= cmd.ToDate.Value.Date
                        select p;
            }

            if (cmd.SortByOrderDate != null)
            {
                query = cmd.SortByOrderDate == true
                    ? (from p in query orderby p.OrderDate select p)
                    : (from p in query orderby p.OrderDate descending select p);
            }

            if (cmd.SortBySalerId != null)
            {
                query = cmd.SortBySalerId == true
                    ? (from p in query orderby p.SalerId select p)
                    : (from p in query orderby p.SalerId descending select p);
            }

            if (cmd.SortByClientName != null)
            {
                query = cmd.SortByClientName == true
                    ? (from p in query orderby p.ClientName select p)
                    : (from p in query orderby p.ClientName descending select p);
            }

            if (cmd.SortByPrice != null)
            {
                query = cmd.SortByPrice == true
                    ? (from p in query orderby p.OverallPrice select p)
                    : (from p in query orderby p.OverallPrice descending select p);
            }

            pagesCount = (int)Math.Ceiling((double)(from p in query select p).Count()/cmd.PageSize);
            query = query.Page(cmd.PageSize, cmd.Page);
            return query.ToList();
        }

        public ClientOrderStatisticalDTO GetClientOrdersStats(OrderCommand cmd, int workplaceId, bool isAdmin, ref int pagesCount)
        {
            IQueryable<OrderDTO> query;
            if (isAdmin)
            {
                query = from c in db.ClientOrders
                        where c.IsDeleted == false
                        select new OrderDTO()
                        {
                            Id = c.Id,
                            TransactionId = c.TransactinoId,
                            SalerId = c.Saler.Id ?? 0,
                            SalerName = c.Saler.Name + " " + c.Saler.Surname,
                            OrderDate = c.OrderDate,
                            Sales = new SaleBL().GetOrderSale(c.Id),
                            ClientId = c.Client.Id ?? 0,
                            ClientName = c.Client.Name,
                            ClientRepresentitive = c.ClientRepresentitive,
                            OverallPrice = c.OverallPrice,
                            PaymentByCard = c.PaymentByCard,
                            PaymentByTransfer = c.PaymentByTransfer,
                            PaymentByCash = c.PaymentByCash,
                            IsDebt = c.IsDebt,
                            WorkPlaceName = c.Saler.WorkPlace.Name,
                            WorkPlaceLocation = c.Saler.WorkPlace.Location
                        };
            }
            else
            {
                query = from c in db.ClientOrders
                        where c.IsDeleted == false
                        join user in db.AccountUsers on c.Saler.Id equals user.Id
                        where user.WorkPlaceId == workplaceId
                        select new OrderDTO()
                        {
                            Id = c.Id,
                            TransactionId = c.TransactinoId,
                            SalerId = c.Saler.Id ?? 0,
                            SalerName = c.Saler.Name + " " + c.Saler.Surname,
                            OrderDate = c.OrderDate,
                            Sales = new SaleBL().GetOrderSale(c.Id),
                            ClientId = c.Client.Id ?? 0,
                            ClientName = c.Client.Name,
                            ClientRepresentitive = c.ClientRepresentitive,
                            OverallPrice = c.OverallPrice,
                            PaymentByCard = c.PaymentByCard,
                            PaymentByTransfer = c.PaymentByTransfer,
                            PaymentByCash = c.PaymentByCash,
                            IsDebt = c.IsDebt,
                            WorkPlaceName = c.Saler.WorkPlace.Name,
                            WorkPlaceLocation = c.Saler.WorkPlace.Location
                        };
            }
            

            if (cmd.ClientId > 0)
            {
                query = from p in query where p.ClientId == cmd.ClientId select p;
            }

            if (cmd.OrderDate != null)
            {
                query = from p in query
                        where p.OrderDate.Date >= cmd.OrderDate.Value.Date
                            && p.OrderDate.Date <= cmd.OrderDate.Value.Date
                        select p;
            }

            if (cmd.FromDate != null && cmd.ToDate != null)
            {
                query = from p in query
                        where p.OrderDate.Date >= cmd.FromDate.Value.Date &&
                              p.OrderDate.Date <= cmd.ToDate.Value.Date
                        select p;
            }

            var aggregation = from c in query
                group c by 1  into grp
            let totalOverallPrice = grp.Sum(x => x.OverallPrice)
            let totalPaymentByTransfer = grp.Sum(x => x.PaymentByTransfer)
            let totalPaymentByCard = grp.Sum(x => x.PaymentByCard)
            let totalPaymentByCash = grp.Sum(x => x.PaymentByCash)
            select new ClientOrderStatisticalDTO()
            {
                TotalOverallPrice = totalOverallPrice,
                TotalPaymentCard = totalPaymentByCard,
                TotalPaymentCash = totalPaymentByCash,
                TotalPaymentTransfer = totalPaymentByTransfer

            };
            var result2 = aggregation.FirstOrDefault();
            if (result2 == null)
            {
                return null;
            }
            result2.ClientOrders = GetClientOrders(cmd, workplaceId, isAdmin, ref pagesCount);

            return result2;
        }

        public OrderDTO GetClientOrder(int id)
        {
            var query = from c in db.ClientOrders where c.Id == id select new OrderDTO()
                {
                    Id = c.Id,
                    TransactionId = c.TransactinoId,
                    SalerId = c.Saler.Id ?? 0,
                    SalerName = c.Saler.Name + " " + c.Saler.Surname,
                    OrderDate = c.OrderDate,
                    Sales = new SaleBL().GetOrderSale(c.Id),
                    ClientId = c.Client.Id ?? 0,
                    ClientName = c.Client.Name,
                    ClientRepresentitive = c.ClientRepresentitive,
                    OverallPrice = c.OverallPrice,
                    PaymentByCard = c.PaymentByCard,
                    PaymentByTransfer = c.PaymentByTransfer,
                    PaymentByCash = c.PaymentByCash,
                    IsDebt = c.IsDebt,
                    WorkPlaceName = c.Saler.WorkPlace.Name,
                    WorkPlaceLocation = c.Saler.WorkPlace.Location
            };
            return query.FirstOrDefault();
        }

        public void Add(OrderDTO order)
        {
            var element = new ClientOrder(order.SalerId, order.ClientId)
            {
                TransactinoId = order.TransactionId,
                OrderDate = order.OrderDate,
                PaymentByCard = order.PaymentByCard,
                PaymentByTransfer = order.PaymentByTransfer,
                PaymentByCash = order.PaymentByCash,
                IsDebt = order.IsDebt,
                ClientRepresentitive = order.ClientRepresentitive,
                OverallPrice = order.OverallPrice
            };
            element.DebtCovers = new EntitySet<DebtCover>();
            element.Sales = new EntitySet<Sale>();
            db.ClientOrders.InsertOnSubmit(element);
            db.SubmitChanges();
            new SaleBL().AddRange(order.Sales,element.Id);
        }

        public void Update(OrderDTO order)
        {
            var element = (from c in db.ClientOrders where c.Id == order.Id select c).FirstOrDefault();
            element.TransactinoId = order.TransactionId;
            element.OrderDate = order.OrderDate;
            element.PaymentByCard = order.PaymentByCard;
            element.PaymentByTransfer = order.PaymentByTransfer;
            element.PaymentByCash = order.PaymentByCash;
            element.UpdatedDate = DateTime.Now;
            element.OverallPrice = order.OverallPrice;
            db.SubmitChanges();
            foreach (var sale in order.Sales)
            {
                if (sale.Id == 0)
                {
                    sale.OrderId = order.Id;
                    new SaleBL().Add(sale);
                }
                else
                {
                    new SaleBL().Update(sale, order.SalerId);
                }
            }
        }

        public void Delete(int id)
        {
            var element = (from c in db.ClientOrders where c.Id == id select c).FirstOrDefault();
            element.IsDeleted = true;
            element.DeletedDate = DateTime.Now;
            db.SubmitChanges();
        }
    }
}
