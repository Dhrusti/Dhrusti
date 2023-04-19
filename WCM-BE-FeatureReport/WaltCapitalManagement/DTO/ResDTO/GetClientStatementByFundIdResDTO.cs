using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetClientStatementByFundIdResDTO
    {
        public List<ClientStatementDetails> ClientList { get; set; }
    }
    public class ClientStatementDetails
    {
        public string ClientAccountNo { get; set; }
        public string Name { get; set; }
        
    }
}
   

