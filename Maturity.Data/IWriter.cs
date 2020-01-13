using Maturity.Model;
using System.Collections.Generic;

namespace Maturity.Data
{
    /// <summary>
    /// Interface for writer with xmlwriter method
    /// </summary>
    public interface IWriter
    {
        byte[] XmlWriter(List<MaturityDetail> maturityDetails);
    }
}