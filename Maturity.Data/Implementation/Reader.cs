using Maturity.Data.Map;
using Maturity.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Maturity.Data.Implementation
{
    /// <summary>
    /// Class to handle read operations from input stream
    /// </summary>
    public class Reader : IReader
    {
        /// <summary>
        /// CsvReader method is to read policy details from the input stream.
        /// </summary>
        /// <param name="InputStream">Stream to read the file and retrieve policy details.</param>
        /// <returns>Returns a list of collection of policy details after parsing of input file.</returns>
        public List<PolicyDetail> CsvReader(Stream InputStream)
        {
            try
            {
                using (var reader = new StreamReader(InputStream))
                {
                    var csvHelperReader = new CsvHelper.CsvReader(reader);

                    csvHelperReader.Configuration.Delimiter = ",";
                    csvHelperReader.Configuration.HasHeaderRecord = true;

                    csvHelperReader.Read();
                    csvHelperReader.ReadHeader();

                    csvHelperReader.Configuration.RegisterClassMap<PolicyDetailCsvMap>();
                    csvHelperReader.ValidateHeader<PolicyDetail>();
                    return csvHelperReader.GetRecords<PolicyDetail>().ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
