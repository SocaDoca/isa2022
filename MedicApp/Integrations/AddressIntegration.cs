using MedicApp.Database;
using MedicApp.Models;

namespace MedicApp.Integrations
{
    public class AddressIntegration
    {
        public AppDbContext _appDbContext { get; set; }


        public AddressIntegration(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Guid> CreateAddres(AddressSaveModel addressSaveModel)
        {
            var dbAddress = _appDbContext.Addresses.FirstOrDefault(x => x.Id == addressSaveModel.Id);
            var saveAddress = new DbAddress();
            if (dbAddress == null)
            {
                saveAddress.Address = addressSaveModel.Address;
                saveAddress.City = addressSaveModel.City;
                saveAddress.Country = addressSaveModel.Country;


                _appDbContext.Addresses.Add(saveAddress);
                _appDbContext.SaveChanges();
            }
            return saveAddress.Id;
        }

        public AddressSaveModel LoadAddressById (Guid id)
        {
            var dbAddress = _appDbContext.Addresses.FirstOrDefault(x => x.Id == id);
            if(dbAddress == null)
            {
                return null;
            }
            var loadAddress = new AddressSaveModel
            {
                Address = dbAddress.Address,
                City = dbAddress.City,
                Country = dbAddress.Country
            };

            return loadAddress;
        }
    }

    public class AddressSaveModel
    {
        public Guid Id { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}

