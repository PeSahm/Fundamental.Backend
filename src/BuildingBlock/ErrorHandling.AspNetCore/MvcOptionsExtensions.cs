using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ErrorHandling.AspNetCore
{
    public static class MvcOptionsExtensions
    {
        /// <summary>
        /// Adds <see cref="ResultConvention"/> which generates <see cref="ProducesResponseTypeAttribute"/>s
        /// for every endpoint marked with <see cref="TranslateResultToActionResultAttribute"/>
        /// based on default configuration
        /// </summary>
        public static MvcOptions AddDefaultResultConvention(this MvcOptions options)
        {

            options.Conventions.Add(new ResultConvention());

            return options;
        }
    }
}

