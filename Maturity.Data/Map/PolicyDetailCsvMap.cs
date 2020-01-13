using CsvHelper;
using CsvHelper.Configuration;
using Maturity.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Maturity.Data.Map
{
    public class PolicyDetailCsvMap : ClassMap<PolicyDetail>
    {
        private readonly string[] formats = { "dd/MM/yyyy", "dd/M/yyyy", "d/M/yyyy", "d/MM/yyyy",
                    "dd/MM/yy", "dd/M/yy", "d/M/yy", "d/MM/yy"};

        public PolicyDetailCsvMap()
        {
            Map(p => p.PolicyNumber).ConvertUsing(
            row =>
            {
                PolicyRecordsValidation(row);
                return row.GetField<string>("policy_number");
            });

            Map(p => p.PolicyNumber).Name("policy_number");
            Map(p => p.Premium).Name("premiums");
            Map(p => p.Membership).Name("membership");
            Map(p => p.Discretion).Name("discretionary_bonus");
            Map(p => p.UpliftPercentage).Name("uplift_percentage");
            Map(p => p.StartDate).Name("policy_start_date").TypeConverterOption.Format(formats).TypeConverterOption.DateTimeStyles(DateTimeStyles.AdjustToUniversal);

            Map(p => p.Policy).Ignore();
            Map(p => p.HasMembership).Ignore();
            Map(p => p.UpliftValue).Ignore();
        }

        #region Private Methods

        /// <summary>
        /// This private method to validate all the fields in a row.
        /// </summary>
        /// <param name="headerRow">contains details of a row . 
        private void PolicyRecordsValidation(IReaderRow headerRow)
        {
            ValidatePolicyNumber(headerRow.GetField<string>("policy_number"), headerRow.Context);
            ValidateStartDate(headerRow.GetField<string>("policy_start_date"), headerRow.Context);
            ValidatePremium(headerRow.GetField<string>("premiums"), headerRow.Context);
            ValidateMembership(headerRow.GetField<string>("membership"), headerRow.Context);
            ValidateDiscretion(headerRow.GetField<string>("discretionary_bonus"), headerRow.Context);
            ValidateUploadPercentage(headerRow.GetField<string>("uplift_percentage"), headerRow.Context);
        }

        /// <summary>
        /// Validate and set error message when policy number is empty or incorrect format.
        /// </summary>
        /// <param name="policyNumber">Policy number to validate.</param>
        /// <param name="context">To get the current row number from CSV.</param>
        /// Exception is thrown if the data is incorrect.
        private void ValidatePolicyNumber(string policyNumber, ReadingContext context)
        {
            if ((string.IsNullOrEmpty(policyNumber) || (!Regex.IsMatch(policyNumber, "^[A-C]{1,1}[0-9]{1,6}$"))))
            {
                throw new Exception("Incorrect data present in the input file row no " + context.RawRow.ToString());
            }
        }

        /// <summary>
        /// Validate and set error message when start date is empty or incorrect date format.
        /// </summary>
        /// <param name="startDate">startDate to validate.</param>
        /// <param name="context">To get the current row number from CSV.</param>
        /// Exception is thrown if the data is incorrect.
        private void ValidateStartDate(string startDate, ReadingContext context)
        {

            if ((string.IsNullOrEmpty(startDate)) || !(DateTime.TryParseExact(startDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime dt)))
            {
                throw new Exception("Incorrect data in the input file row no " + context.RawRow.ToString());
            }
        }

        /// <summary>
        /// Validate and set error message when premium is empty or discretion value is not a double or 0.
        /// </summary>
        /// <param name="premium">premium to validate.</param>
        /// <param name="context">To get the current row number from CSV.</param>
        /// Exception is thrown if the data is incorrect.
        private void ValidatePremium(string premium, ReadingContext context)
        {
            if ((string.IsNullOrEmpty(premium)) || ((!double.TryParse(premium, out double premiumAmount)) || premiumAmount == 0))
            {
                throw new Exception("Incorrect data in the input file row no " + context.RawRow.ToString());
            }
        }

        /// <summary>
        /// Validate and set error message when membership is empty or value contains either Y or N.
        /// </summary>
        /// <param name="membership">membership to validate.</param>
        /// <param name="context">To get the current row number from CSV.</param>
        /// Exception is thrown if the data is incorrect.
        private void ValidateMembership(string membership, ReadingContext context)
        {
            if ((string.IsNullOrEmpty(membership)) || (!Regex.IsMatch(membership, "^[Y|N]{1,1}$")))
            {
                throw new Exception("Incorrect data in the input file row no " + context.RawRow.ToString());
            }
        }

        /// <summary>
        /// Validate and set error message when membership is empty or discretion value is not a double or 0.
        /// </summary>
        /// <param name="discretion">discretion to validate.</param>
        /// <param name="context">To get the current row number from CSV.</param>
        /// Exception is thrown if the data is incorrect.
        private void ValidateDiscretion(string discretion, ReadingContext context)
        {
            if ((string.IsNullOrEmpty(discretion)) || ((!double.TryParse(discretion, out double discretionValue)) || discretionValue == 0))
            {
                throw new Exception("Incorrect data in the input file row no" + context.RawRow.ToString());
            }
        }

        /// <summary>
        /// Validate and set error message when uplift is empty or uplift is not decimal value.
        /// </summary>
        /// <param name="uplift">uplift to validate.</param>
        /// <param name="context">To get the current row number from CSV.</param>
        /// Exception is thrown if the data is incorrect.
        private void ValidateUploadPercentage(string uplift, ReadingContext context)
        {
            if ((string.IsNullOrEmpty(uplift)) || (!decimal.TryParse(uplift, out decimal upliftPercentage)))
            {
                throw new Exception("Incorrect data in the input file row no" + context.RawRow.ToString());
            }
        }

        #endregion Private Methods
    }
}