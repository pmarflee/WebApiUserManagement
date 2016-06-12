[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApiUserManagement.MVCGridConfig), "RegisterGrids")]

namespace WebApiUserManagement
{

    using MVCGrid.Models;
    using MVCGrid.Web;
    using Models;
    using System.Web.Mvc;
    using AutoMapper;
    using Repository;
    using System.Linq;
    public static class MVCGridConfig 
    {
        public static void RegisterGrids()
        {
            MVCGridDefinitionTable.Add("Users", 
                new MVCGridBuilder<User>(new ColumnDefaults { EnableSorting = true })
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
                {
                    cols.Add().WithColumnName("UserName")
                        .WithHeaderText("User Name")
                        .WithValueExpression(i => i.Username);
                    cols.Add().WithColumnName("BrandName")
                        .WithHeaderText("Brand")
                        .WithValueExpression(i => i.Brand.BrandName);
                    cols.Add().WithColumnName("Roles")
                        .WithHeaderText("Roles")
                        .WithValueExpression(i => string.Join(", ", i.Roles.OrderBy(r => r.RoleName).Select(r => r.RoleName)));
                    cols.Add("Edit")
                        .WithHtmlEncoding(false)
                        .WithSorting(false)
                        .WithHeaderText(" ")
                        .WithValueExpression((i, c) => c.UrlHelper.Action("Detail", "Users", new { id = i.UserId }))
                        .WithValueTemplate("<a href='{Value}' class='btn btn-primary' role='button'>Edit</a>");
                })
                .WithSorting(true, "Username")
                .WithPaging(true, 20)
                .WithRetrieveDataMethod((context) =>
                {
                    var resolver = DependencyResolver.Current;
                    var userRepository = resolver.GetService<IUserRepository>();
                    var mapper = resolver.GetService<IMapper>();
                    var options = context.QueryOptions;
                    var sortAscending = 
                        options.SortDirection == SortDirection.Asc || 
                        options.SortDirection == SortDirection.Unspecified;
                    var users = userRepository.GetAll(options.SortColumnName, sortAscending);

                    return new QueryResult<User>()
                    {
                        Items = users,
                        TotalRecords = users.Count
                    };

                })
            );
        }
    }
}