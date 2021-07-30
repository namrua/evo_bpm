using System;
using System.Collections.Generic;

namespace CleanEvoBPM.Application.Common
{
    public class GenericResponse
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<ValidationDetail> Errors { get; set; }
        public static GenericResponse SuccessResult()
        {
            return new GenericResponse
            {
                Success = true,
            };
        }

        public static GenericResponse FailureResult(int? code = null)
        {
            return new GenericResponse
            {
                Success = false,
                Code = code.HasValue ? code.Value : 404
            };
        }

        public static GenericResponse CustomValidationExceptionResult(Exception ex)
        {
            try
            {
                var listError = new List<ValidationDetail>();

                foreach (var item in ((Exceptions.CustomValidationException)ex).Errors)
                {
                    listError.Add(new ValidationDetail()
                    {
                        Key = item.Key,
                        Reason = item.Value.Length != 0 ? item.Value[0].ToString() : string.Empty
                    });
                }

                var error = new GenericResponse()
                {
                    Code = 400,
                    Success = false,
                    Errors = listError
                };
                return error;
            }
            catch (Exception)
            {
                var error = new GenericResponse()
                {
                    Code = 400,
                    Success = false
                };
                return error;
            }
        }

        public static GenericResponse<T> SuccessResult<T>(T item)
        {
            return new GenericResponse<T>
            {
                Success = true,
                Code = 0,
                Content = item
            };
        }
        public static GenericResponse<T> SuccessResult<T>()
        {
            return new GenericResponse<T>
            {
                Success = true,
                Code = 0
            };
        }

        public static GenericResponse<T> FailureResult<T>(int? code = null)
        {
            return new GenericResponse<T>
            {
                Success = false,
                Code = code.HasValue ? code.Value : 404
            };
        }

        public static GenericListResponse<T> SuccessResultList<T>(IEnumerable<T> items)
        {
            return new GenericListResponse<T>
            {
                Success = true,
                Code = 0,
                Content = items
            };
        }

        public static GenericListResponse<T> FailureResultList<T>(int? code = null)
        {
            return new GenericListResponse<T>
            {
                Success = false,
                Code = code.HasValue ? code.Value : 404
            };
        }


       
    }

    public class GenericResponse<T> : GenericResponse
    {
        public T Content { get; set; }
    }

    public class GenericListResponse<T> : GenericResponse
    {
        public IEnumerable<T> Content { get; set; }
    }
}