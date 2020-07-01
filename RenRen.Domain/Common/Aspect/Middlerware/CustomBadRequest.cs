using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using RenRen.Domain.Common.Utils;

namespace RenRen.Domain.Common.Aspect.Middlerware
{
    public class CustomBadRequest : R
    {
        public CustomBadRequest(ActionContext context) : base(400, "参数错误")
        {
            Dictionary<string, object> _errors = new Dictionary<string, object>();
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = GetErrorMessage(errors[0]);
                        _errors.Add(key, new[] { errorMessage });
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = GetErrorMessage(errors[i]);
                        }
                        _errors.Add(key, errorMessages);
                    }
                }
            }
            base.Put(_errors);

            static string GetErrorMessage(ModelError error)
            {
                return error.ErrorMessage;
            }
        }
    }
}