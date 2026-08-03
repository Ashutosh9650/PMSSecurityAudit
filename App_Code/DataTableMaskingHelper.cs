using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

public class DataTableMaskingHelper
{

    private static readonly HashSet<string> KnownDobColumns = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "DOB",
        "Date of Birth",
        "DateOfBirth",
        "Contact DOB",
        "ContactDOB",
        "Child DOB",
        "ChildDOB",
        "Mother DOB",
        "Father DOB",
        "Beneficiary DOB"
    };

    private static bool CheckIsDobColumn(string colName)
    {
        if (string.IsNullOrEmpty(colName)) return false;

        return KnownDobColumns.Contains(colName) ||
               colName.IndexOf("dob", StringComparison.OrdinalIgnoreCase) >= 0 ||
               colName.IndexOf("date of birth", StringComparison.OrdinalIgnoreCase) >= 0;
    }

    public void DecryptAndMaskDataTable(DataTable dt, params string[] targetColumns)
    {
        DecryptAndMaskDataTable(dt, false, targetColumns);
    }


    public void DecryptAndMaskDataTable(DataTable dt, bool calculateAge, params string[] targetColumns)
    {
        if (dt == null || dt.Rows.Count == 0) return;

        int rowCount = dt.Rows.Count;

        List<ColumnInfo> activeColumns = new List<ColumnInfo>();

        if (targetColumns != null && targetColumns.Length > 0)
        {
            foreach (string colName in targetColumns)
            {
                int index = dt.Columns.IndexOf(colName);
                if (index != -1)
                {
                    string actualColName = dt.Columns[index].ColumnName;

                    activeColumns.Add(new ColumnInfo { Index = index, Name = actualColName, IsDobColumn = CheckIsDobColumn(actualColName) });
                }
            }
        }
        else
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                string actualColName = dt.Columns[i].ColumnName;
                activeColumns.Add(new ColumnInfo { Index = i, Name = actualColName, IsDobColumn = CheckIsDobColumn(actualColName) });
            }
        }

        if (activeColumns.Count == 0) return;

        int colCount = activeColumns.Count;
        string[,] processedData = new string[rowCount, colCount];

        int ageColIndex = dt.Columns.IndexOf("Age");
        int enrolDateColIndex = dt.Columns.IndexOf("EnrolmentDate");

        bool isAgeCalculationRequired = calculateAge && (ageColIndex != -1 && enrolDateColIndex != -1);

        string[] processedAge = new string[rowCount];
        

        // Parallel Processing
        Parallel.For(0, rowCount, i =>
        {
            SqlInjection sqlInjection = new SqlInjection();
            DataRow row = dt.Rows[i];

            for (int j = 0; j < colCount; j++)
            {
                ColumnInfo col = activeColumns[j];
                object val = row[col.Index];

                if (val != DBNull.Value && val != null)
                {
                    string enc = val.ToString();
                    if (!string.IsNullOrEmpty(enc))
                    {
                        string plain = sqlInjection.DecryptMatchingWithSessionMasking(enc, col.Name);

                        if (!string.IsNullOrEmpty(plain))
                        {
                            if (col.IsDobColumn)
                            {
                                DateTime dobDate;
                                if (plain != "01/01/1900" && plain != "1900-01-01" && DateTime.TryParse(plain, out dobDate))
                                {
                                    processedData[i, j] = dobDate.ToString("dd/MM/yyyy");

                                    if (isAgeCalculationRequired && row[enrolDateColIndex] != DBNull.Value)
                                    {
                                        processedAge[i] = CalculateAge(dobDate, row[enrolDateColIndex].ToString());
                                    }
                                }
                                else
                                {
                                    processedData[i, j] = plain;
                                    if (isAgeCalculationRequired) processedAge[i] = "";
                                }
                            }
                            else
                            {
                                processedData[i, j] = plain;
                            }
                        }
                    }
                }
            }
        });

 
        dt.BeginLoadData();
        for (int i = 0; i < rowCount; i++)
        {
            for (int j = 0; j < colCount; j++)
            {
                string newValue = processedData[i, j];
                if (newValue != null)
                {
                    dt.Rows[i][activeColumns[j].Index] = newValue;
                }
            }

            if (isAgeCalculationRequired && processedAge[i] != null)
            {
                dt.Rows[i][ageColIndex] = processedAge[i];
            }
        }
        dt.EndLoadData();
    }

    private static string CalculateAge(DateTime dobDate, string enrolDateStr)
    {
        if (string.IsNullOrEmpty(enrolDateStr)) return "";

        string[] allowedFormats = {
            "dd/MM/yyyy",
            "dd-MM-yyyy HH:mm:ss",
            "dd/MM/yyyy HH:mm:ss",
            "yyyy-MM-dd HH:mm:ss",
            "yyyy-MM-dd"
        };

        DateTime enrolmentDate;
        if (DateTime.TryParseExact(enrolDateStr, allowedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out enrolmentDate))
        {
            return UdfDateDiffinYrMonDay(dobDate, enrolmentDate);
        }

        return "";
    }

    public static string UdfDateDiffinYrMonDay(DateTime dateFrom, DateTime dateTo)
    {
        int years = dateTo.Year - dateFrom.Year;

        if (dateFrom.AddYears(years) > dateTo)
        {
            years--;
        }

        return years.ToString();
    }

    private class ColumnInfo
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public bool IsDobColumn { get; set; }
    }
}