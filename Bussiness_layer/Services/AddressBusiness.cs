using Bussiness_layer.Interface;
using Models_Layer;
using Repository_Layer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_layer.Services
{
    public class AddressBusiness:IAddressBusiness
    {
        private readonly IAddressRepo iaddressRepo;

        public AddressBusiness(IAddressRepo iaddressRepo)
        {
            this.iaddressRepo = iaddressRepo;
        }
        public string AddAddress(int User_Id,AddressModel addressModel)
        {
            return iaddressRepo.AddAddress(User_Id,addressModel);   

        }
        public IEnumerable<AddressModel> AddressList()
        {
            return iaddressRepo.AddressList();
        }
        public string DeleteAddress(int Address_Id)
        {
            return iaddressRepo.DeleteAddress(Address_Id);
        }

        public AddressModel UpdateAddresss(int Address_Id)
        {
            return iaddressRepo.UpdateAddresss(Address_Id);
        }
    }
}
