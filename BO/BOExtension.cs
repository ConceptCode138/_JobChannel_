using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using FluentValidation;
using System.Threading.Tasks;

namespace BO
{
    public static class BOExtension
    { 
        public static void AddBO(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
