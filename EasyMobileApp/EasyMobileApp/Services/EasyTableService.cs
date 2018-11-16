using System;
using System.IO;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Http;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using EasyMobileApp.Models;

namespace EasyMobileApp.Services
{
    public class EasyTableService
    {

        private CloudStorageAccount _cloudStorageAccount;
        private CloudTableClient _cloudTableClient;
        private string _tableNameString;
        private CancellationTokenSource _tokenSource;

        public EasyTableService()
        {

            string connectionString = ConfigurationManager.AppSettings["STORAGE_CONNECTION_STRING"];
            string tableNameString = ConfigurationManager.AppSettings["TABLE_CONTAINER_NAME"];

            CloudStorageAccount.TryParse(connectionString, out _cloudStorageAccount);

            _cloudTableClient = _cloudStorageAccount.CreateCloudTableClient();
            _tableNameString = string.Copy(tableNameString);
            _tokenSource = new CancellationTokenSource();

        }

        public async Task<Tuple<int, List<EasyTableModel>>> FetchAllAsync()
        {

            var tableReference = _cloudTableClient.GetTableReference(_tableNameString);
            string tableExceptionMessage = string.Empty;

            try
            {

                var filterConditionString = TableQuery.GenerateFilterCondition("PartitionKey",
                                                                                QueryComparisons.NotEqual,
                                                                                string.Empty);
                var tableQuery = (new TableQuery<EasyTableModel>()).Where(filterConditionString);
                List<EasyTableModel> insertResult = null;

                await Task.Run(() =>
                {

                    insertResult = tableReference.ExecuteQuery(tableQuery).ToList();
                    
                });

                return (new Tuple<int, List<EasyTableModel>>(1, insertResult));

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                return (new Tuple<int, List<EasyTableModel>>(-1, null));

            }

        }

        public async Task<Tuple<int, object>> InsertRowAsync(EasyTableModel insertModel)
        {

            var tableReference = _cloudTableClient.GetTableReference(_tableNameString);
            string tableExceptionMessage = string.Empty;

            try
            {

                var tableOperation = TableOperation.InsertOrReplace(insertModel);
                var insertResult = await tableReference.ExecuteAsync(tableOperation);
                return (new Tuple<int, object>(insertResult.HttpStatusCode, insertResult.Result));
                

            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                return (new Tuple<int, object>(-1, null));

            }          
            
        }

    }
}