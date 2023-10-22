using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Model.Other;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using SqlSugar;
using System.Text;
using WebAPI.Config;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // ���ñ���Ͱ汾����/swagger/index.html��չʾ
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "zdc.Admin.API", Version = "v1" });
    // ���ò���Ĭ��ֵ
    options.ParameterFilter<DefaultValueParameterFilter>();
    // ���ö������Ͳ���Ĭ��ֵ
    options.SchemaFilter<DefaultValueSchemaFilter>();   //������Դ��ģ���������Լ�������
    //���Ӱ�ȫ����
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "������token,��ʽΪ Bearer xxxxxxxx��ע���м�����пո�",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    //���Ӱ�ȫҪ��
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme{
                            Reference =new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id ="Bearer"
                            }
                        },Array.Empty<string>()
                    }
                });
});

//�ӿں�ʵ����ע���������ע�룬api����ʹ���ʵ����
//builder.Services.AddScoped<IApplicationBuilder, ApplicationBuilder>();

//�滻����IOC������ΪAutofac����ע������
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    #region ͨ��ģ�黯�ķ�ʽע��ӿڲ��ʵ�ֲ� 
    container.RegisterModule(new AutofacModuleRegister());
    #endregion

    #region ע��sqlsugar
    container.Register<ISqlSugarClient>(context =>
    {
        SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
        {
            DbType = DbType.MySql,
            ConnectionString = builder.Configuration.GetConnectionString("conn"),
            IsAutoCloseConnection = true,
        });
        //֧��sql��������������ų�����
        db.Aop.OnLogExecuting = (sql, par) =>
        {
            Console.WriteLine($"===================");
            Console.WriteLine($"Sql���:{sql}");
            List<string> list = new List<string>();
            par.ToList().ForEach(p => { list.Add(p.Value != null ? p.Value.ToString() : ""); });
            Console.WriteLine($"����:{string.Join(",", list)}");
        };

        return db;
    });
    #endregion
});
//Automapperӳ�䣬ע��AutoMapper��ط���
builder.Services.AddAutoMapper(typeof(AutoMapperConfigs));

// ��ȡ����
builder.Services.Configure<JWTTokenOptions>(builder.Configuration.GetSection("JWTTokenOptions"));

#region jwtУ�� 
{
    //�ڶ��������Ӽ�Ȩ�߼�
    JWTTokenOptions tokenOptions = new JWTTokenOptions();
    builder.Configuration.Bind("JWTTokenOptions", tokenOptions);
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//Scheme
     .AddJwtBearer(options =>  //���������õļ�Ȩ���߼�
     {
         options.TokenValidationParameters = new TokenValidationParameters
         {
             //JWT��һЩĬ�ϵ����ԣ����Ǹ���Ȩʱ�Ϳ���ɸѡ��
             ValidateIssuer = true,//�Ƿ���֤Issuer
             ValidateAudience = true,//�Ƿ���֤Audience
             ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
             ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
             ValidAudience = tokenOptions.Audience,//
             ClockSkew = TimeSpan.FromSeconds(0),//����token���ں���ʧЧ��Ĭ�Ϲ��ں�300��������Ч
             ValidIssuer = tokenOptions.Issuer,//Issuer���������ǰ��ǩ��jwt������һ��
             IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.SecurityKey))//�õ�SecurityKey 
         };
     });
}
#endregion


//Ҫ��װnuget������ԭ���Ĵ�Сд
builder.Services.AddControllers().AddNewtonsoftJson(options => {
    // ָ����ν��ѭ�����ã�
    //1��Ignore������ѭ������
    //2��Serialize�����л�ѭ������
    //3��Error���׳��쳣
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    // ͳһ����API�����ڸ�ʽ
    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
    // ͳһ����JSON��ʵ��ĸ�ʽ��Ĭ��JSON�������ĸΪСд�������Ϊͬ���Modeһ�£�
    options.SerializerSettings.ContractResolver = new DefaultContractResolver();//����JSON���ظ�ʽͬmodelһ��
});






var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


// �����ļ�����(ֱ��ͨ��·���Ϳ��Է���)
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(AppContext.BaseDirectory, "wwwroot")),
    RequestPath = "/static"
});


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();



