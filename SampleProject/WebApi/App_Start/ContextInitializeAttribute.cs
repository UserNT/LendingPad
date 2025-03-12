using System;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using Raven.Client;

namespace WebApi.App_Start
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ContextInitializeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            var container = GlobalConfiguration.Configuration.DependencyResolver;
            var method = actionExecutedContext.Request.Method;
            if (method == HttpMethod.Post || method == HttpMethod.Put || method == HttpMethod.Delete)
            {
                var session = (IDocumentSession)container.GetService(typeof(IDocumentSession));

                try
                {
                    session.SaveChanges();
                }
                catch (Raven.Abstractions.Exceptions.ConcurrencyException) when (method == HttpMethod.Post)
                {
                    var json = JsonConvert.SerializeObject(new
                    {
                        status = 409,
                        message = "Conflict: Record already exists."
                    });

                    actionExecutedContext.Response.StatusCode = System.Net.HttpStatusCode.Conflict;
                    actionExecutedContext.Response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }
            }
        }
    }
}