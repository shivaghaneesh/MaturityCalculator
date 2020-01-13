using Maturity.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace Maturity.Data
{
    /// <summary>
    /// Interface for reader with csvReader method
    /// </summary>
    public interface IReader
    {
        List<PolicyDetail> CsvReader(Stream InputStream);
    }
}
