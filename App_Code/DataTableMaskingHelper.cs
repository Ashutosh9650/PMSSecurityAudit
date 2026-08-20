using DocumentFormat.OpenXml;
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
        DecryptAndMaskDataTable(dt, false, false, targetColumns);
    }

    public void DecryptAndMaskDataTable(DataTable dt, bool calculateAge, params string[] targetColumns)
    {
        DecryptAndMaskDataTable(dt, calculateAge, false, targetColumns);
    }

    public void DecryptAndMaskDataTable(DataTable dt, bool calculateAge, bool calculateDistance, params string[] targetColumns)
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

        int distColIndex = -1;
        int srcLatIndex = -1;
        int srcLonIndex = -1;
        int trgLatIndex = -1;
        int trgLonIndex = -1;
        bool isDistanceCalcRequired = false;
        string[] processedDistance = null;

        if (calculateDistance)
        {
            distColIndex = FindColumnIndex(dt, "DistanceInKm", "Session Update Distance from School", "Distance", "CalculatedDistance");

            srcLatIndex = FindColumnIndex(dt, "SourceLatitude", "AttendanceLatitude", "Latitude", "AppLatitude");
            srcLonIndex = FindColumnIndex(dt, "SourceLongitude", "AttendanceLongitude", "Longitude", "AppLongitude");
            trgLatIndex = FindColumnIndex(dt, "TargetLatitude", "SchoolLatitude", "sLatitude", "VillageLatitude", "MasterLatitude");
            trgLonIndex = FindColumnIndex(dt, "TargetLongitude", "SchoolLongitude", "sLongitute", "sLongitude", "VillageLongitude", "MasterLongitude");

            isDistanceCalcRequired = (distColIndex != -1 && srcLatIndex != -1 && srcLonIndex != -1 && trgLatIndex != -1 && trgLonIndex != -1);
            if (isDistanceCalcRequired)
            {
                processedDistance = new string[rowCount];
            }
        }

        Dictionary<string, FieldMaskConfig> sessionMaskConfig = RoleMaskingConfig.Instance.GetConfigFromSession();

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
                        string plain = sqlInjection.DecryptMatchingWithSessionMasking(enc, col.Name, true, sessionMaskConfig);

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

            if (isDistanceCalcRequired)
            {
                string rawSrcLat = row[srcLatIndex] != DBNull.Value ? row[srcLatIndex].ToString() : "";
                string rawSrcLon = row[srcLonIndex] != DBNull.Value ? row[srcLonIndex].ToString() : "";
                string rawTrgLat = row[trgLatIndex] != DBNull.Value ? row[trgLatIndex].ToString() : "";
                string rawTrgLon = row[trgLonIndex] != DBNull.Value ? row[trgLonIndex].ToString() : "";

                string decSrcLat = !string.IsNullOrEmpty(rawSrcLat) ? sqlInjection.DecryptMatchingWithSessionMasking(rawSrcLat, "", false, null) : "";
                string decSrcLon = !string.IsNullOrEmpty(rawSrcLon) ? sqlInjection.DecryptMatchingWithSessionMasking(rawSrcLon, "", false, null) : "";
                string decTrgLat = !string.IsNullOrEmpty(rawTrgLat) ? sqlInjection.DecryptMatchingWithSessionMasking(rawTrgLat, "", false, null) : "";
                string decTrgLon = !string.IsNullOrEmpty(rawTrgLon) ? sqlInjection.DecryptMatchingWithSessionMasking(rawTrgLon, "", false, null) : "";

                processedDistance[i] = CalculateDistanceInKm(decSrcLat, decSrcLon, decTrgLat, decTrgLon);
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

            if (isDistanceCalcRequired && processedDistance != null && processedDistance[i] != null)
            {
                dt.Rows[i][distColIndex] = processedDistance[i];
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

    private static int FindColumnIndex(DataTable dt, params string[] columnNames)
    {
        foreach (string name in columnNames)
        {
            int idx = dt.Columns.IndexOf(name);
            if (idx != -1) return idx;
        }
        return -1;
    }

    private static string CalculateDistanceInKm(string lat1Str, string lon1Str, string lat2Str, string lon2Str)
    {
        if (string.IsNullOrWhiteSpace(lat1Str) || string.IsNullOrWhiteSpace(lon1Str) ||
            string.IsNullOrWhiteSpace(lat2Str) || string.IsNullOrWhiteSpace(lon2Str))
        {
            return "";
        }

        double lat1, lon1, lat2, lon2;
        if (!double.TryParse(lat1Str.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out lat1) ||
            !double.TryParse(lon1Str.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out lon1) ||
            !double.TryParse(lat2Str.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out lat2) ||
            !double.TryParse(lon2Str.Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out lon2))
        {
            return "";
        }

        if ((lat1 == 0 && lon1 == 0) || (lat2 == 0 && lon2 == 0))
        {
            return "";
        }

        try
        {
            double rLat1 = lat1 * (Math.PI / 180.0);
            double rLon1 = lon1 * (Math.PI / 180.0);
            double rLat2 = lat2 * (Math.PI / 180.0);
            double rLon2 = lon2 * (Math.PI / 180.0);

            double cosValue = (Math.Cos(rLat1) * Math.Cos(rLat2) * Math.Cos(rLon1 - rLon2)) +
                              (Math.Sin(rLat1) * Math.Sin(rLat2));

            cosValue = Math.Min(1.0, Math.Max(-1.0, cosValue));

            double distance = 6371.0 * Math.Acos(cosValue);
            return distance.ToString("0.00", CultureInfo.InvariantCulture);
        }
        catch
        {
            return "";
        }
    }

    private class ColumnInfo
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public bool IsDobColumn { get; set; }
    }
}