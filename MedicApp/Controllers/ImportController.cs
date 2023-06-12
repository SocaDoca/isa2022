using MedicApp.Integrations;
using MedicApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace MedicApp.Controllers
{
    public class ImportController: ControllerBase
    {
        private readonly IImportIntegration _importIntegration;

        public ImportController(IImportIntegration importIntegration)
        {
            _importIntegration = importIntegration;
        }
        [HttpPost("import-equiment")]
        public void ImportEquipment()
        {
            _importIntegration.ImportEquipment();
        }

    }

}
