using System.Data;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using SimRegisPortal.Core.AppSettings;
using SimRegisPortal.Domain.Entities;
using SimRegisPortal.Persistence.Constants;

namespace SimRegisPortal.WebApi.Extensions
{
    public static class SerilogExtensions
    {
        public static void AddSerilog(this WebApplicationBuilder builder, AppSettings appSettings)
        {
            var columnOptions = new ColumnOptions
            {
                PrimaryKey = new SqlColumn(nameof(SystemLog.Id), SqlDbType.BigInt, allowNull: false),
                TimeStamp =
                {
                    ColumnName = nameof(SystemLog.TimeStamp),
                    DataType = SqlDbType.DateTime2,
                    ConvertToUtc = true
                },
                Level =
                {
                    ColumnName = nameof(SystemLog.Level),
                    DataLength = EntityFieldPresets.DefaultStringLength,
                    AllowNull = false
                },
                Message =
                {
                    ColumnName = nameof(SystemLog.Message),
                    AllowNull = false
                },
                MessageTemplate = { ColumnName = nameof(SystemLog.MessageTemplate) },
                Exception = { ColumnName = nameof(SystemLog.Exception) },
                Properties = { ColumnName = nameof(SystemLog.Properties) }
            };

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MSSqlServer(
                    connectionString: appSettings.ConnectionStrings.SimRegisPortalDbConnection,
                    sinkOptions: new MSSqlServerSinkOptions
                    {
                        TableName = nameof(SystemLog),
                        AutoCreateSqlDatabase = false,
                        AutoCreateSqlTable = false
                    },
                    columnOptions: columnOptions
                )
                .MinimumLevel.Error()
                .MinimumLevel.Override("SimRegisPortal", LogEventLevel.Debug)
                .CreateLogger();
            builder.Host.UseSerilog();
        }
    }
}
