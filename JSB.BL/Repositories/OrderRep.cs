using JSB.BL.Interfaces;
using JSB.DAL.DBContext;
using JSB.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSB.BL.Repositories
{
    public class OrderRep :IOrderRep
    {
        private readonly ApplicationContext _db;

        public OrderRep(ApplicationContext _db)
        {
            this._db = _db;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _db.Orders.Include(o => o.OrderProducts)
                                        .ThenInclude(op => op.Product)
                                        .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _db.Orders.Include(o => o.OrderProducts)
                                        .ThenInclude(op => op.Product)
                                        .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public async Task AddOrderAsync(Order order)
        {
            _db.Orders.Add(order);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _db.Entry(order).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _db.Orders.FindAsync(orderId);
            if (order != null)
            {
                _db.Orders.Remove(order);
                await _db.SaveChangesAsync();
            }
        }
    }
}
