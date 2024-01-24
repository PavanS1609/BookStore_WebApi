using Models_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.Interface
{
    public interface IAddressRepo
    {
        public string AddAddress(int User_Id,AddressModel addressModel);
        public IEnumerable<AddressModel> AddressList();

        public string DeleteAddress(int Address_Id);
        public AddressModel UpdateAddresss(int Address_Id);

    }
}
