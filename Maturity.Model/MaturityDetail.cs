namespace Maturity.Model
{ /// <summary>
  /// To hold the maturity details values of the policies and used serialize maturity value into output XML.
  /// </summary>
    public class MaturityDetail
    {
        /// <summary>
        /// Gets or sets the policy number.
        /// </summary>
        public string PolicyNumber { get; set; }
        /// <summary>
        /// Gets or sets the maturity Value.
        /// </summary>
        public string MaturityValue { get; set; }
    }
}
