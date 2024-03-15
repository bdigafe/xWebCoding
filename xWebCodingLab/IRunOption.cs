using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xWebCodingLab.xWebNetForumXML;

namespace xWebCodingLab
{
    public interface IRunOption
    {
        void RunOption(xWebWrapper xWebClient, RunConfig config);
    }
}
