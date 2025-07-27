using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Infrastructure.Sql
{
    public static class UserQueries
    {
        public const string GetAll = @"
        SELECT id, Name, Email, PhoneNumber AS PhoneNumber, PasswordHash AS PasswordHash,
               Role, IsActive AS IsActive, IsDeleted AS IsDeleted, CreatedAt AS CreatedAt
        FROM users WHERE IsDeleted = FALSE ORDER BY CreatedAt DESC";

        public const string GetById = @"
        SELECT id, Name, Email, PhoneNumber AS PhoneNumber, PasswordHash AS PasswordHash,
               Role, IsActive AS IsActive, IsDeleted AS IsDeleted, CreatedAt AS CreatedAt
        FROM users WHERE id = @Id AND IsDeleted = FALSE";

        public const string Insert = @"
        INSERT INTO users (Name, Email, PhoneNumber, PasswordHash, Role, IsActive, IsDeleted, CreatedAt)
        VALUES (@Name, @Email, @PhoneNumber, @PasswordHash, @Role, @IsActive, FALSE, NOW())
        RETURNING id";

        public const string Update = @"
        UPDATE users SET
            name = @Name,
            email = @Email,
            PhoneNumber = @PhoneNumber,
            PasswordHash = @PasswordHash,
            Role = @Role,
            IsActive = @IsActive
        WHERE id = @Id AND IsDeleted = FALSE";

        public const string SoftDelete = @"UPDATE users SET IsDeleted = TRUE WHERE id = @Id";

        public const string GetByEmail = @"
        SELECT id, Name, Email, PhoneNumber AS PhoneNumber, PasswordHash AS PasswordHash,
               Role, IsActive AS IsActive, IsDeleted AS IsDeleted, CreatedAt AS CreatedAt
        FROM users WHERE Email = @Email AND IsDeleted = FALSE";
    }
}
