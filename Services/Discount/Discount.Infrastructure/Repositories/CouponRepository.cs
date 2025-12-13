using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Discount.Infrastructure.Data.Contexts;
using System.Data;

namespace Discount.Infrastructure.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly IDiscountContext _context;

        public CouponRepository(IDiscountContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Coupon>> GetAllCouponsAsync()
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Coupons";
            return await connection.QueryAsync<Coupon>(query);
        }

        public async Task<Coupon> GetCouponByIdAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Coupons WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Coupon>(query, new { Id = id });
        }

        public async Task<Coupon> GetCouponByProductNameAsync(string productName)
        {
            using var connection = _context.CreateConnection();
            var query = "SELECT * FROM Coupons WHERE ProductName = @ProductName";
            return await connection.QueryFirstOrDefaultAsync<Coupon>(query, new { ProductName = productName });
        }

        public async Task<Coupon> CreateCouponAsync(Coupon coupon)
        {
            using var connection = _context.CreateConnection();
            var query = @"INSERT INTO Coupons (ProductName, Description, Amount, CreatedAt, ExpiresAt, IsActive) 
                         VALUES (@ProductName, @Description, @Amount, @CreatedAt, @ExpiresAt, @IsActive) 
                         RETURNING *";
            return await connection.QueryFirstOrDefaultAsync<Coupon>(query, coupon);
        }

        public async Task<bool> UpdateCouponAsync(Coupon coupon)
        {
            using var connection = _context.CreateConnection();
            var query = @"UPDATE Coupons 
                         SET ProductName = @ProductName, Description = @Description, Amount = @Amount, 
                             ExpiresAt = @ExpiresAt, IsActive = @IsActive 
                         WHERE Id = @Id";
            var affected = await connection.ExecuteAsync(query, coupon);
            return affected > 0;
        }

        public async Task<bool> DeleteCouponAsync(int id)
        {
            using var connection = _context.CreateConnection();
            var query = "DELETE FROM Coupons WHERE Id = @Id";
            var affected = await connection.ExecuteAsync(query, new { Id = id });
            return affected > 0;
        }
    }
}
