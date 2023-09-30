using Cms.Core.Services;
using Cms.Core.Services.Abstractions;
using Cms.DataLayer.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Core.Security
{
    public class PermissionCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private int _permissionId;
        private IPermissionService _permissionService;
        public PermissionCheckerAttribute(int permissionId)
        {
            _permissionId = permissionId;
        }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _permissionService = context.HttpContext.RequestServices.GetService<IPermissionService>();



            if(context.HttpContext.User.Identity.IsAuthenticated)
            {
                var username = context.HttpContext.User.Identity.Name;
                var checkPermission = _permissionService.CheckPermission(_permissionId, username);
                
                if( !checkPermission)
                    context.Result = new RedirectResult("/Login");

            }
            else
            {
                context.Result = new RedirectResult("/Login");
            }
        }
    }
}
