using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetPPMClientListTFSAResDTO
    {
        public List<PPMClientTFSADetail> PPMClientTFSADetails { get; set; }
        public int TotalCount { get; set; }

    }
    public class PPMClientTFSADetail
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public decimal AccValue { get; set; }
        public string Currency { get; set; }
    }

}
