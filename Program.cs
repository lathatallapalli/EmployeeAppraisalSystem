using EmployeeAppraisalSystem.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;
using UseCases.Interfaces;
using UseCases.EmployeeUseCases;
using Plugins.DataStore.InMemory;
using UseCases.CompetencyUseCases;
using UseCases.AppraisalUseCases;
using UseCases.ObjectiveUseCases;
using Plugins.DataStore.SQLServer;
using Microsoft.EntityFrameworkCore;


namespace EmployeeAppraisalSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<AppraisalSystemContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeAppraisalSystem"));
            });


            builder.Services.AddControllersWithViews();


            builder.Services.AddSingleton<AppraisalRepository>();
            builder.Services.AddSignalR();

            if (builder.Environment.IsEnvironment("QA"))
            {
                builder.Services.AddSingleton<IEmployeeRepository, EmployeesInMemoryRepository>();
                builder.Services.AddSingleton<ICompetencyRepository, CompetencyInMemoryRepository>();
                builder.Services.AddSingleton<IRoleRepository, RoleInMemoryRepository>();
                builder.Services.AddSingleton<IRoleCompetencyRepository, RoleCompetencyInMemoryRepository>();
                builder.Services.AddSingleton<IAppraisalRepository, AppraisalInMemoryRepository>();
                builder.Services.AddSingleton<IAppraisalDetailsObjectiveRepository, AppraisalDetailsObjectiveInMemoryRepository>();
                builder.Services.AddSingleton<IAppraisalDetailsCompetencyRepository, AppraisalDetailsCompetencyInMemoryRepository>();

            }
            else
            {
                builder.Services.AddTransient<IEmployeeRepository, EmployeeSQLRepository>();
                builder.Services.AddTransient<ICompetencyRepository, CompetencySQLRepository>();
                builder.Services.AddTransient<IRoleRepository, RoleSQLRepository>();
                builder.Services.AddTransient<IRoleCompetencyRepository, RoleCompetencySQLRepository>();
                builder.Services.AddTransient<IAppraisalRepository, AppraisalSQLRepository>();
                builder.Services.AddTransient<IAppraisalDetailsObjectiveRepository, AppraisalDetailsObjectiveSQLRepository>();
                builder.Services.AddTransient<IAppraisalDetailsCompetencyRepository, AppraisalDetailsCompetencySQLRepository>();
            }



            builder.Services.AddTransient<IViewEmployeesUseCase, ViewEmployeesUseCase>();
            builder.Services.AddTransient<IAddEmployeeUseCase, AddEmployeeUseCase>();
            builder.Services.AddTransient<IDeleteEmployeeUseCase, DeleteEmployeeUseCase>();
            builder.Services.AddTransient<IEditEmployeeUseCase, EditEmployeeUseCase>();
            builder.Services.AddTransient<IViewSelectedEmployeeUseCase, ViewSelectedEmployeeUseCase>();
            builder.Services.AddTransient<IViewManagerDirectsUseCase, ViewManagerDirectsUseCase>();
            builder.Services.AddTransient<IUserAuthenticationUseCase, UserAuthenticationUseCase>();
            builder.Services.AddTransient<IViewCompetenciesOfEmployeeUseCase, ViewCompetenciesOfEmployeeUseCase>();


            builder.Services.AddTransient<IViewCompetenciesUseCase, ViewCompetenciesUseCase>();
            builder.Services.AddTransient<IAddCompetencyUseCase, AddCompetencyUseCase>();
            builder.Services.AddTransient<IDeleteCompetencyUseCase, DeleteCompetencyUseCase>();
            builder.Services.AddTransient<IGetCompetencyUseCase, GetCompetencyUseCase>();
            builder.Services.AddTransient<IViewCompetenciesOfRoleUseCase, ViewCompetenciesOfEmployee>();


            builder.Services.AddTransient<IAddObjectiveUseCase, AddObjectiveUseCase>();
            builder.Services.AddTransient<IViewEmployeeAppraisalObjectivesUseCase, ViewEmployeeAppraisalObjectivesUseCase>();


            builder.Services.AddTransient<IStartAppraisalCycleUseCase, StartAppraisalCycleUseCase>();
            builder.Services.AddTransient<IUpdateAppraisalStatusUseCase, UpdateAppraisalStatusUseCase>();
            builder.Services.AddTransient<IAddSelfAppraisalCompetenciesUseCase, AddSelfAppraisalCompetenciesUseCase>();
            builder.Services.AddTransient<IAddSelfAppraisalObjectivesUseCase, AddSelfAppraisalObjectivesUseCase>();
            builder.Services.AddTransient<IViewAppraisalObjectivesUseCase, ViewAppraisalObjectivesUseCase>();
            builder.Services.AddTransient<IViewAppraisalCompetenciesUseCase, ViewAppraisalCompetenciesUseCase>();
            builder.Services.AddTransient<IUpdateManagerAppraisalCompetenciesUseCase, UpdateManagerAppraisalCompetenciesUseCase>();
            builder.Services.AddTransient<IUpdateManagerAppraisalObjectivesUseCase, UpdateManagerAppraisalObjectivesUseCase>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapHub<NotificationHub>("/notificationHub");

            app.Run();
        }

        public class NotificationHub : Hub
        {
            public async Task SendNotification(string message)
            {
                // Broadcast the notification to all connected clients
                await Clients.All.SendAsync("ReceiveNotification", message);
            }
        }
    }
}
