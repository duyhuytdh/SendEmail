using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SendMail.Util
{
    public static class HandleException
    {
        public static string SqlExcforContact(Exception exception)
        {
            DbUpdateConcurrencyException concurrencyEx = exception as DbUpdateConcurrencyException;
            if (concurrencyEx != null)
            {
                // A custom exception of yours for concurrency issues
                return exception.ToString();
            }

            DbUpdateException dbUpdateEx = exception as DbUpdateException;
            if (dbUpdateEx != null)
            {
                if (dbUpdateEx != null
                        && dbUpdateEx.InnerException != null
                        && dbUpdateEx.InnerException.InnerException != null)
                {
                    SqlException sqlException = dbUpdateEx.InnerException.InnerException as SqlException;
                    if (sqlException != null)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627: return "Lỗi danh sách chứa email đã tồn tại";  // Unique constraint error
                            case 547: return "Constraint check violation";  // Constraint check violation
                            case 2601: return "Duplicated key row error"; // Duplicated key row error
                                // Constraint violation exception

                            default:
                                // A custom exception of yours for other DB issues
                                return exception.ToString();
                        }
                    }
                }
            }
            return exception.ToString();
        }
    }
}