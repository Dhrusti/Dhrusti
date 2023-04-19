using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ResDTO
{
    public class GetAllDashboardDetailsResDTO
    {
        public List<GetAllClientResDTO> getAllClientResDTOs { get; set; }
        public List<GetAllDoctorResDTO> getAllDoctorResDTOs { get; set; }
        public List<GetAllExtensionResDTO> getAllExtensionResDTOs { get; set; }
        public List<GetAllCallTypeResDTO> getAllCallTypeResDTOs { get; set; }
        public List<GetAllApptDoctorResDTO> getAllApptDoctorResDTOs { get; set; }

        public decimal? AccountNo { get; set; }
    }
}
