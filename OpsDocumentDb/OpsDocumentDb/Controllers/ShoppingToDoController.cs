using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System.Security;

namespace OpsDocumentDb.Controllers
{
    public class ShoppingToDoController : Controller
    {
        private const string EndpointUri = "&lt;YOUR-URI&gt;";
        private const string AccessKey = "&lt;YOUR-PRIMARY-KEY&gt;";

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(ShoppingTodo todoItem)
        {
            using (var client = new DocumentClient(new Uri(EndpointUri), ToSecureString(AccessKey)))
            {
                const string DatabaseName = "todo";

                // Check if Database exists
                var database = client.CreateDatabaseQuery()
                    .Where(db => db.Id == DatabaseName)
        .AsEnumerable()
        .FirstOrDefault();

                if (database == null)
                {
                    database = await client.CreateDatabaseAsync(new Database
                    {
                        Id = DatabaseName
                    });
                }



                const string CollectionName = "shopping";

                // Check if collection already exists
                DocumentCollection collection = client.CreateDocumentCollectionQuery(database.CollectionsLink)

                                        .Where(c => c.Id == CollectionName)
                            .AsEnumerable()
                            .FirstOrDefault();

                if (collection == null)
                {
                    // Create collection
                    collection = await client.CreateDocumentCollectionAsync(

                                                database.CollectionsLink,
                                                new DocumentCollection { Id = CollectionName });
                }

                ResourceResponse<Document> createdItem = await
         client.CreateDocumentAsync(collection.DocumentsLink, todoItem);
            }

            return RedirectToAction("Index");

        }

        private SecureString ToSecureString(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                return null;
            else
            {
                SecureString result = new SecureString();
                foreach (char c in source.ToCharArray())
                    result.AppendChar(c);
                return result;
            }
        }
    }
}