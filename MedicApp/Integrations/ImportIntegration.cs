using MedicApp.Database;
using MedicApp.Models;
using Newtonsoft.Json;

namespace MedicApp.Integrations
{
    public interface IImportIntegration
    {
        void ImportEquipment();
    }
    public class ImportIntegration : IImportIntegration
    {
        private readonly AppDbContext _appDbContext;
        public ImportIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void ImportEquipment()
        {
            using (StreamReader r = new StreamReader("Catalogs/EquipmentCatalog.json"))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject<List<Item>>(json);
                foreach (var item in items)
                {
                    var model = new WorkItem
                    {
                        Name = item.Name
                    };
                    _appDbContext.WorkItems.Add(model);
                    _appDbContext.SaveChanges();
                }
            }
        }

    }

    public class Item
    {
        public String Name { get; set; }
    }
}
