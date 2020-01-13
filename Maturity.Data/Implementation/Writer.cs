using Maturity.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Maturity.Data.Implementation
{
    /// <summary>
    /// Class to handle all write operation from maturity details.
    /// </summary>
    public class Writer : IWriter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="maturityDetails">maturity details that is be converted into xml</param>
        /// <returns></returns>
        public byte[] XmlWriter(List<MaturityDetail> maturityDetails)
        {
            var xmlSerializer = new XmlSerializer(typeof(List<MaturityDetail>), new XmlRootAttribute("MaturityDetails"));
            byte[] content;

            using (MemoryStream stream = new MemoryStream())
            {
                xmlSerializer.Serialize(stream, maturityDetails);

                stream.Position = 0;

                content = stream.ToArray();
            }
            return content;
        }
    }
}