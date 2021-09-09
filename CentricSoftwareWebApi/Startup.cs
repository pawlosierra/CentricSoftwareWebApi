using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CentricSoftwareWebApi.Application.Queries.Products.GetProducts;
using CentricSoftwareWebApi.Domain.Repositories;
using CentricSoftwareWebApi.Infrastructure.Data;
using CentricSoftwareWebApi.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CentricSoftwareWebApi
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddControllers();

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

      services.AddMediatR(typeof(GetProductsHandler).Assembly);

      services.AddScoped<IClothingRepository, ClothingRepository>();
      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<ITagsRepository, TagsRepository>();

      services.AddDbContext<productsContext>(
        options => options.UseSqlServer(
          "Server=DESKTOP-9LJ95CE; Database=products; Trusted_Connection=True; User=centric;"
          ));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
