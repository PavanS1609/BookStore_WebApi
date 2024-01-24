using Models_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bussiness_layer.Interface
{
    public interface IAddressBusiness
    {
        public string AddAddress(int User_Id,AddressModel addressModel);
        public IEnumerable<AddressModel> AddressList();
        public string DeleteAddress(int Address_Id);
        public AddressModel UpdateAddresss(int Address_Id);
    }
}
