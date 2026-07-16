using System.Linq;
using System.Web.UI.WebControls;


public class LocationHelper
{
    public static string GetSelectedValues(ListControl list)
    {
        return string.Join(",",
            list.Items.Cast<ListItem>()
                .Where(x => x.Selected)
                .Select(x => x.Value));
    }

    public static LocationFilter GetLocationFilter(
        CheckBoxList chkState,
        CheckBoxList chkDistrict,
        CheckBoxList chkBlock,
        CheckBoxList chkVillage,
        DropDownList ddlYear)
    {
        return new LocationFilter
        {
            StateCodes = GetSelectedValues(chkState),
            DistrictCodes = GetSelectedValues(chkDistrict),
            BlockCodes = GetSelectedValues(chkBlock),
            VillageCodes = GetSelectedValues(chkVillage),
            Year = ddlYear.SelectedValue
        };
    }
}
public class LocationFilter
{
    public string StateCodes { get; set; }

    public string DistrictCodes { get; set; }

    public string BlockCodes { get; set; }

    public string VillageCodes { get; set; }

    public string Year { get; set; }
}


