using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using SmsGateway.Domain.CoreContext.SMSModule.Services;

namespace SmsGateway.Infrastructure.Data.CoreContext.SMSModule.Services
{
 
    public class DatabaseService : IDatabases
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public bool Start()
        {
            return StartDatabase();
        }

        private bool StartDatabase()
        {
            //Variavel de controle (caso algum banco de dados não seja iniciado corretamente, retornará falso)
            var sucesso = false;
            sucesso = SetProvider();
            if(sucesso)
            {
            //Setar provider correto de acordo com o web.config (SQL Server CE / SQL Server / etc.. )
           sucesso =  DataBaseStart<SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.CoreContextUnitOfWork,
                         SmsGateway.Infrastructure.Data.CoreContext.Migrations.Configuration>(GetCoreContextConnectionString());
            //Try again
            if(!sucesso)
                sucesso = DataBaseStart<SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.CoreContextUnitOfWork,
                            SmsGateway.Infrastructure.Data.CoreContext.Migrations.Configuration>(GetCoreContextConnectionString());
            }
            else
            {
                throw new Exception("Cannot start database, verify connection and provider name.");
            }
            return sucesso;

        }

        public bool SetProvider()
        {
            var sucesso = false;
             
                //Checar se vamos usar SQL Server Compact (Ambiente de desenvolvimento)
                string dbProvider = GetProviderInvariantName();
                if (!string.IsNullOrEmpty(dbProvider))
                {
                    switch (dbProvider)
                    {
                        case "System.Data.SqlServerCe.4.0":
                            Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0");
                            sucesso = true;
                            break;
                        case "SqlConnectionFactory":
                            //DO NOTHING
                            sucesso = true;
                            break;
                        case "System.Data.SqlClient":
                            //DO NOTHING
                            sucesso = true;
                            break;
                    }


                }
           
             
            return sucesso;

        }

        private string GetProviderInvariantName()
        {

            try
            {
                //Checar se vamos usar SQL Server Compact (Ambiente de desenvolvimento)
                return System.Configuration.ConfigurationManager.ConnectionStrings["SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.CoreContextUnitOfWork"].ProviderName;

            }
            catch
            {
                return "System.Data.SqlClient";
            }


        }
        
        private string GetCoreContextConnectionString()
        {

            try
            {
                //Checar string de conexão para a base de dados.
                return System.Configuration.ConfigurationManager.ConnectionStrings["SmsGateway.Infrastructure.Data.CoreContext.UnitOfWork.CoreContextUnitOfWork"].ConnectionString;

            }
            catch 
            {
               throw new Exception("Cannot Start Databse, Missing connection String.");
            }


        }
      
        private bool DataBaseStart<T, TC>(string connectionString)
            where T : DbContext, new()
            where TC : DbMigrationsConfiguration<T>, new()
        {


            //Atualiza a base de dados com base na configuração do migrations
            try
            {
                var migratorConfig = new TC();
                migratorConfig.TargetDatabase = new DbConnectionInfo(connectionString, GetProviderInvariantName());
                var dbMigrator = new DbMigrator(migratorConfig);


                dbMigrator.Update();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }


        }

    }
}