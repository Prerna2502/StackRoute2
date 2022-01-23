using System;
using System.Collections.Generic;
using CategoryService.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using NoteService.Models;
using ReminderService.Models;
using UserService.Models;

namespace Test
{
    //WebApplication Factory for Category API
    public class CategoryWebApplicationFactory<TStartup> : WebApplicationFactory<CategoryService.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<CategoryContext>();

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<CategoryContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CategoryWebApplicationFactory<TStartup>>>();

                    try
                    {
                        // Seed the database with some specific test data.
                        context.Categories.DeleteMany(Builders<CategoryService.Models.Category>.Filter.Empty);
                        context.Categories.InsertMany(new List<CategoryService.Models.Category>
            {
                new CategoryService.Models.Category{Id=101, Name="Sports", CreatedBy="Mukesh", Description="All about sports", CreationDate=DateTime.Now },
                 new CategoryService.Models.Category{Id=102, Name="Politics", CreatedBy="Mukesh", Description="INDIAN politics", CreationDate=DateTime.Now }
            });
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }

    //WebApplication Factory for Reminder API
    public class ReminderWebApplicationFactory<TStartup> : WebApplicationFactory<ReminderService.Startup>
    {
        
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<ReminderContext>();

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<ReminderContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<ReminderWebApplicationFactory<TStartup>>>();

                    try
                    {
                        // Seed the database with some specific test data.
                        context.Reminders.DeleteMany(Builders<ReminderService.Models.Reminder>.Filter.Empty);
                        context.Reminders.InsertMany(new List<ReminderService.Models.Reminder>
            {
                new ReminderService.Models.Reminder{Id=201, Name="Sports", CreatedBy="Mukesh", Description="sports reminder", CreationDate=DateTime.Now, Type="email" },
                 new ReminderService.Models.Reminder{Id=202, Name="Politics", CreatedBy="Mukesh", Description="politics reminder", CreationDate=DateTime.Now,Type="email" }
            });
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }

    //WebApplication Factory for User API
    public class UserWebApplicationFactory<TStartup> : WebApplicationFactory<UserService.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<UserContext>();

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<UserContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<UserWebApplicationFactory<TStartup>>>();

                    try
                    {
                        // Seed the database with some specific test data.
                        context.Users.DeleteMany(Builders<User>.Filter.Empty);
                        context.Users.InsertMany(new List<User>
                            {
                                new User{ UserId="Mukesh", Name="Mukesh",Contact="9812345670", AddedDate=DateTime.Now},
                                new User{ UserId="Sachin", Name="Sachin", Contact="8987653412", AddedDate=DateTime.Now}
                            });
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }

    public class NoteWebApplicationFactory<TStartup> : WebApplicationFactory<NoteService.Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<NoteContext>();

                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<NoteContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<NoteWebApplicationFactory<TStartup>>>();

                    try
                    {
                        // Seed the database with some specific test data.
                        context.Notes.DeleteMany(Builders<NoteUser>.Filter.Empty);
                        context.Notes.InsertMany(new List<NoteUser>
            {
                new NoteUser{
                    UserId="Mukesh", Notes=new List<Note>{
                        new Note {Id=101, Category= new NoteService.Models.Category{Id=101, Name="Sports", CreatedBy="Mukesh", Description="All about sports", CreationDate=DateTime.Now },
                        Content="Sample Note", CreatedBy="Mukesh", Reminders=new List<NoteService.Models.Reminder>{ new NoteService.Models.Reminder { Id = 201, Name = "Sports", CreatedBy = "Mukesh", Description = "sports reminder", CreationDate = DateTime.Now, Type = "email" } } ,
                        Title="Sample", CreationDate=DateTime.Now}
                    }
                },

                new NoteUser{
                    UserId="Sachin", Notes=new List<Note>{
                        new Note {Id=101, Category= new NoteService.Models.Category{Id=102, Name="Sports", CreatedBy="Sachin", Description="All about sports", CreationDate=DateTime.Now },
                        Content="Sample Note", CreatedBy="Sachin", Reminders=new List<NoteService.Models.Reminder>{ new NoteService.Models.Reminder { Id = 202, Name = "Sports", CreatedBy = "Sachin", Description = "sports reminder", CreationDate = DateTime.Now, Type = "email" } } ,
                        Title="Sample", CreationDate=DateTime.Now}
                    }
                }

            });

                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "An error occurred seeding the " +
                                            "database with test messages. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
