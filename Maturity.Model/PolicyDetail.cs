using System;

namespace Maturity.Model
{
    /// <summary>
    /// To hold the policy details values of the policies from .csv file uploaded. 
    /// </summary>
    public class PolicyDetail
    {
        /// <summary>
        /// Gets or sets the policy number.
        /// </summary>
        public string PolicyNumber { get; set; }

        /// <summary>
        /// Gets the policy type from first character of policy number property and returns Enum value such as A or B or C.
        /// </summary>
        public PolicyType Policy => (PolicyType)Enum.Parse(typeof(PolicyType), PolicyNumber.Substring(0, 1));

        /// <summary>
        /// Gets or sets the policy start date
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///  Gets or sets policy premiums.
        /// </summary>
        public double Premium { get; set; }

        /// <summary>
        /// Gets or Sets value of Membership. This hold either Y or N.
        /// </summary>
        public string Membership { get; set; }

        /// <summary>
        /// Gets the hasmembership from Membership property. When memebership value is Y then it returns true else false.
        /// </summary>
        public bool HasMembership => string.Equals(Membership, "Y");

        /// <summary>
        /// Gets or sets discretion value.
        /// </summary>
        public double Discretion { get; set; }

        /// <summary>
        /// Gets or sets uplift percentage
        /// </summary>
        public decimal UpliftPercentage { get; set; }

        /// <summary>
        /// Gets uplift value from percentage . For example uplift percentage 50 will returns 1.50 as uplift value.
        /// </summary>
        public decimal UpliftValue => 1 + UpliftPercentage / 100;

    }
}
