using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoverLetterGenerator.Service.Models
{
    public class CoverLetterModel
    {
        public string CompanyName { get; set; } = string.Empty;
        public string CompanyAddress { get; set; } = string.Empty;
        public string CompanyZip { get; set; } = string.Empty;
        public string CompanyCity { get; set; } = string.Empty;
        public string JobPosition { get; set; } = string.Empty;
    }
}
