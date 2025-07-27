using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickBill.Domain.Models.Common
{
    public class ApiResponse<T>
    {
        public string Status { get; set; } = "success"; // success | fail | error
        public int Code { get; set; } = 200;
        public string Message { get; set; } = "OK";
        public T? Data { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiResponse<T> Success(T data, string message = "OK", int code = 200) =>
            new() { Status = "success", Code = code, Message = message, Data = data };

        public static ApiResponse<T> Fail(string message = "Failed", int code = 400) =>
            new() { Status = "fail", Code = code, Message = message };

        public static ApiResponse<T> Error(string message = "Internal Server Error", int code = 500) =>
            new() { Status = "error", Code = code, Message = message };
    }
}
