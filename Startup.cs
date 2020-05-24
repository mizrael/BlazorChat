using System;
using System.Collections.Generic;
using System.Linq;
using BlazorChat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.Circuits;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorChat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton<IUserStateProvider, UserStateProvider>();

            services.AddScoped<IConnectedClientProvider, ConnectedClientProvider>();
            services.AddScoped<ClientCircuitHandler>();
            services.AddScoped<CircuitHandler>(ctx => ctx.GetService<ClientCircuitHandler>());

            var channel = System.Threading.Channels.Channel.CreateBounded<Message>(100);

            services.AddSingleton<IMessagesPublisher>(ctx =>
            {
                return new MessagesPublisher(channel.Writer);
            });

            services.AddSingleton<IMessagesConsumer>(ctx =>
            {
                return new MessagesConsumer(channel.Reader);
            });

            services.AddHostedService<MessagesConsumerWorker>();

            services.AddSingleton<IChatService, ChatService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
