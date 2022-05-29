using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaizenCaseStudyOne.Business.Abstract
{
    public interface IPackagingCodeService
    {
        bool ValidatePackagingCode(string packagingCode);
        public string GeneratePackagingCode();
    }
}
