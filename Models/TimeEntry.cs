using System.Globalization;

namespace BlazorTimeEntryStatusModule.Models;

public class TimeEntryResponse
{
    public List<TimeEntry> TimeEntries { get; set; }

    public string PrevPageURI { get; set; }
    public string NextPageURI { get; set; }
}


// Only grabbing the information that's needed for this app (rest of the fields can be found here: https://www.dovico.com/developer/API_doc/index.htm#t=API_Calls_v7%2FTime_Entries_v7.htm)
public class TimeEntry
{
    public string ID { get; set; } // TempTrans (unapproved time) is a Guid with a 'T' prefix. Trans (approved time) is an Int64 with an 'M' prefix
    public Sheet Sheet { get; set; }

    protected string _DateString = "";
    public string Date // Name coming from the API's return data
    {
        get { return _DateString; }
        set { SetDate(value); }
    }


    // Needed so that we can sort and compare based on the date value
    protected DateTime _Date = DateTime.MinValue;
    public DateTime TheDate { get { return _Date; } }

    protected void SetDate(string Date)
    {
        // Remember the original string
        _DateString = Date;

        _Date = DateTime.ParseExact(Date, "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}

public class Sheet
{
    public string ID { get; set; }
    public string Status { get; set; }
}

