using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Infrastructure.Sql
{
    public static class ClientQueries
    {
        public const string GetAll = @"SELECT id, UserId, Name, Email, PhoneNumber, Address, CreatedAt FROM clients WHERE UserId = @UserId AND IsDeleted = FALSE ORDER BY CreatedAt DESC;";
        public const string GetById = @"SELECT id, UserId, Name, Email, PhoneNumber, Address, CreatedAt FROM clients WHERE id = @Id AND IsDeleted = FALSE;";
        public const string Insert = @"INSERT INTO clients (UserId, name, email, PhoneNumber, Address) VALUES (@UserId, @Name, @Email, @PhoneNumber, @Address) RETURNING id;";
        public const string Update = @"UPDATE clients SET Name = @Name, Email = @Email, PhoneNumber = @PhoneNumber, address = @Address WHERE id = @Id 
                                       AND IsDeleted = FALSE;";
        public const string Delete = @"UPDATE clients SET IsDeleted = TRUE WHERE id = @Id;";
    }

}
